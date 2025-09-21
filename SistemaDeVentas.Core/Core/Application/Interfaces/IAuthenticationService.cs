using System.Threading.Tasks;
using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<User?> GetCurrentUserAsync();
        bool IsAuthenticated { get; }
        User? CurrentUser { get; }
    }

    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public User? User { get; set; }

        public static AuthenticationResult Success(User user) => new()
        {
            IsSuccess = true,
            User = user
        };

        public static AuthenticationResult Failure(string errorMessage) => new()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}