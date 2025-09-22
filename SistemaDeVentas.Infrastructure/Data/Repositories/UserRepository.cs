using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SalesSystemDbContext _context;

    public UserRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Sales)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Sales)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<IEnumerable<User>> GetByRoleAsync(string role)
    {
        return await _context.Users
            .Include(u => u.Sales)
            .Where(u => u.Role == role)
            .ToListAsync();
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Password = newPassword;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActivateUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsActive = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Sales)
            .Where(u => u.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetInactiveUsersAsync()
    {
        return await _context.Users
            .Include(u => u.Sales)
            .Where(u => !u.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> SearchAsync(string searchTerm)
    {
        return await _context.Users
            .Include(u => u.Sales)
            .Where(u => u.Name.Contains(searchTerm) ||
                       u.Username.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<string> HashPasswordAsync(string password)
    {
        using (var connection = _context.Database.GetDbConnection())
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT PWDENCRYPT(@password)";
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@password";
                parameter.Value = password;
                command.Parameters.Add(parameter);

                var result = await command.ExecuteScalarAsync();
                return result?.ToString() ?? string.Empty;
            }
        }
    }

    public async Task<User?> AuthenticateWithPwdCompareAsync(string username, string password)
    {
        try
        {
            using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT TOP 1 id, username, password, role, name, in_invoice, date
                        FROM users
                        WHERE username = @username AND PWDCOMPARE(@password, password) = 1";
                    command.Parameters.Add(new SqlParameter("@username", username));
                    command.Parameters.Add(new SqlParameter("@password", password));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Username = reader.GetString(1),
                                Password = reader.GetString(2),
                                Role = reader.GetString(3),
                                Name = reader.GetString(4),
                                InInvoice = reader.GetBoolean(5),
                                CreatedAt = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                                Email = string.Empty,
                                IsActive = true,
                                UpdatedAt = null
                            };
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Log the exception or handle it appropriately
            // For now, rethrow or return null
            throw new InvalidOperationException("Error authenticating user", ex);
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            throw new InvalidOperationException("Unexpected error during authentication", ex);
        }
        return null;
    }
}