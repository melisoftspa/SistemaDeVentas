using System.Threading.Tasks;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.WinUI.Models;

namespace SistemaDeVentas.WinUI.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string username, string password);
        Task<bool> LogoutAsync();
        Task<SistemaDeVentas.Core.Domain.Entities.User?> GetCurrentUserAsync();
        bool IsAuthenticated { get; }
        SistemaDeVentas.Core.Domain.Entities.User? CurrentUser { get; }
    }

    public class AuthenticationResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public SistemaDeVentas.Core.Domain.Entities.User? User { get; set; }

        public static AuthenticationResult Success(SistemaDeVentas.Core.Domain.Entities.User user) => new()
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