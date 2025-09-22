using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Core.Domain.Entities;

public class Tax
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Value { get; set; }

    public string? Text { get; set; }

    public bool Exenta { get; set; }

    [NotMapped]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "El nombre del impuesto es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [NotMapped]
    [Required(ErrorMessage = "El porcentaje del impuesto es requerido")]
    [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
    public decimal Percentage { get; set; }

    [NotMapped]
    public bool IsExempt { get; set; }

    [NotMapped]
    public bool IsActive { get; set; } = true;

    // Computed property
    public string DisplayText => $"{Text} ({Value}%)";
}
