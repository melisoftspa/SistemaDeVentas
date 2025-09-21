using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.WinUI.Models
{
    public class User : IUser
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 50 caracteres")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es requerido")]
        public string Role { get; set; } = "Empleado";

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres")]
        public string FullName { get; set; } = string.Empty;

        public bool InInvoice { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public List<ISale> Sales { get; set; } = new List<ISale>();

        // Computed properties
        public string DisplayName => $"{Name} ({Username})";
        public bool IsAdmin => Role.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        public bool IsEmployee => Role.Equals("Empleado", StringComparison.OrdinalIgnoreCase);
        public string StatusText => IsActive ? "Activo" : "Inactivo";
        public int TotalSales => Sales?.Count ?? 0;
    }

    public static class UserRoles
    {
        public const string Administrator = "Administrador";
        public const string Employee = "Empleado";
        public const string Manager = "Gerente";
        
        public static readonly string[] AllRoles = { Administrator, Employee, Manager };
    }
}