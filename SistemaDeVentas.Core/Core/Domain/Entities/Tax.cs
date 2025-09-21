using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Core.Domain.Entities;

public class Tax
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Required(ErrorMessage = "El nombre del impuesto es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El porcentaje del impuesto es requerido")]
    [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
    public decimal Percentage { get; set; }

    public bool IsExempt { get; set; }

    public bool IsActive { get; set; } = true;

    // Computed property
    public string DisplayText => $"{Name} ({Percentage}%)";
}
