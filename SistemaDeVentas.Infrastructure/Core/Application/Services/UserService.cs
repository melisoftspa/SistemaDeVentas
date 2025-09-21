using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class UserService : IUserService, IAuthService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !user.IsActive)
        {
            return null;
        }

        var hashedPassword = HashPassword(password);
        if (user.Password != hashedPassword)
        {
            return null;
        }

        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Validar que el email no esté en uso
        if (await _userRepository.ExistsByEmailAsync(user.Email))
        {
            throw new ArgumentException("El email ya está en uso por otro usuario.");
        }

        // Hash de la contraseña
        user.Password = HashPassword(user.Password);
        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = true;

        return await _userRepository.CreateAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var hashedCurrentPassword = HashPassword(currentPassword);
        if (user.Password != hashedCurrentPassword)
        {
            return false;
        }

        user.Password = HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> ResetPasswordAsync(string email, string newPassword)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        user.Password = HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeactivateUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> ActivateUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.IsActive = true;
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<IEnumerable<User>> GetActiveUsersAsync()
    {
        return await _userRepository.GetActiveUsersAsync();
    }

    public async Task<IEnumerable<User>> GetInactiveUsersAsync()
    {
        return await _userRepository.GetInactiveUsersAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        return await _userRepository.GetByRoleAsync(role);
    }

    public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm)
    {
        return await _userRepository.SearchAsync(searchTerm);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _userRepository.ExistsAsync(id);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _userRepository.ExistsByEmailAsync(email);
    }

    public async Task<int> GetTotalUsersCountAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Count();
    }

    public async Task<int> GetActiveUsersCountAsync()
    {
        var activeUsers = await _userRepository.GetActiveUsersAsync();
        return activeUsers.Count();
    }

    // Implementación de IAuthService
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetByUsernameAsync(username);
    }

    public async Task<User?> AuthenticateUserAsync(string username, string password)
    {
        return await AuthenticateAsync(username, password);
    }

    public async Task<bool> IsUsernameAvailableAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user == null;
    }

    public bool Login(string username, string password)
    {
        var user = AuthenticateAsync(username, password).Result;
        return user != null;
    }

    public Task<bool> ValidateUserAsync(User user)
    {
        return Task.FromResult(user != null && user.IsActive);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}