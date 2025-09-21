using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<User?> AuthenticateUserAsync(string username, string password);
    Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task<bool> ValidateUserAsync(User user);
    Task<bool> ActivateUserAsync(Guid userId);
    Task<bool> DeactivateUserAsync(Guid userId);
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<IEnumerable<User>> SearchUsersAsync(string searchTerm);
    Task<bool> IsUsernameAvailableAsync(string username);
}