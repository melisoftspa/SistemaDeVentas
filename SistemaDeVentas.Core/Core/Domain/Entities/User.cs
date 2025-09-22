using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Core.Domain.Entities;

public class User
{
    public int Id { get; set; }

    [NotMapped]
    public DateTime? CreatedAt { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
    [StringLength(100, ErrorMessage = "El email no puede exceder 100 caracteres")]
    public string Email { get; set; } = string.Empty;

    [NotMapped]
    public DateTime? UpdatedAt { get; set; }

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

    public bool InInvoice { get; set; }

    [NotMapped]
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public List<Sale> Sales { get; set; } = new List<Sale>();

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
