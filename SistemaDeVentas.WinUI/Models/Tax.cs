using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.WinUI.Models
{
    public class Tax
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "El nombre del impuesto es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public double Percentage { get; set; }

        public bool State { get; set; } = true;

        public DateTime Date { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Description { get; set; }

        // Navigation properties
        public List<Product> Products { get; set; } = new List<Product>();

        // Computed properties
        public string DisplayName => $"{Name} ({Percentage}%)";
        public string StatusText => State ? "Activo" : "Inactivo";
        public int ProductCount => Products?.Count ?? 0;
        public decimal DecimalPercentage => (decimal)(Percentage / 100);
    }

    public static class CommonTaxes
    {
        public static readonly Tax IVA = new Tax
        {
            Name = "IVA",
            Percentage = 19,
            Description = "Impuesto al Valor Agregado"
        };

        public static readonly Tax Exento = new Tax
        {
            Name = "Exento",
            Percentage = 0,
            Description = "Producto exento de impuestos"
        };
    }
}