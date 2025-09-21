using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Interfaces
{
    public interface IAuthService
    {
        bool Login(string username, string password);
        bool CreateUser(string username, string password, string name, bool inInvoice);
        bool ChangePassword(string newPassword, string name, bool inInvoice);
        User? GetCurrentUser();
        int? CurrentUserId { get; }
    }
}
