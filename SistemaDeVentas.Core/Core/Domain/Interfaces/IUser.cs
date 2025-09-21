using System;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string FullName { get; set; }
        bool IsActive { get; set; }
        bool IsAdmin { get; }
        bool IsEmployee { get; }
        string DisplayName { get; }
        string StatusText { get; }
    }
}