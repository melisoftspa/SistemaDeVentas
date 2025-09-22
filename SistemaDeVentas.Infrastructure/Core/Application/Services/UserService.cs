using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

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

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        return await _userRepository.AuthenticateWithPwdCompareAsync(username, password);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        // Validar que el email no esté en uso
        if (await _userRepository.ExistsByEmailAsync(user.Email))
        {
            throw new ArgumentException("El email ya está en uso por otro usuario.");
        }

        // Hash de la contraseña
        user.Password = await _userRepository.HashPasswordAsync(user.Password);
        user.IsActive = true;

        return await _userRepository.CreateAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        // Verificar contraseña actual usando autenticación
        var authenticatedUser = await AuthenticateAsync(user.Username, currentPassword);
        if (authenticatedUser == null || authenticatedUser.Id != userId)
        {
            return false;
        }

        user.Password = await _userRepository.HashPasswordAsync(newPassword);
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

        user.Password = await _userRepository.HashPasswordAsync(newPassword);
        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeactivateUserAsync(int userId)
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

    public async Task<bool> ActivateUserAsync(int userId)
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

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
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

}