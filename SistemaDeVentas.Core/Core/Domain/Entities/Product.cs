using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Core.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    [Required(ErrorMessage = "El nombre del producto es requerido")]
    [StringLength(500, ErrorMessage = "El nombre no puede exceder 500 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
    public int Amount { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor a 0")]
    public double SalePrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El mínimo debe ser mayor o igual a 0")]
    public int Minimum { get; set; }

    [StringLength(500, ErrorMessage = "El código de barras no puede exceder 500 caracteres")]
    public string? Barcode { get; set; }

    public byte[]? Photo { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser mayor o igual a 0")]
    public int Stock { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Price { get; set; }

    public bool Exenta { get; set; }

    public Guid? IdTax { get; set; }

    public DateTime? Expiration { get; set; }

    public Guid? IdCategory { get; set; }

    public bool IsPack { get; set; }

    public Guid? IdPack { get; set; }

    [StringLength(1500, ErrorMessage = "La línea no puede exceder 1500 caracteres")]
    public string? Line { get; set; }

    public Guid? IdSubcategory { get; set; }

    // Navigation properties
    public Tax? Tax { get; set; }
    public Category? Category { get; set; }
    public Subcategory? Subcategory { get; set; }

    // Computed properties
    public double PriceWithTax => Tax != null ? Price * (1 + (double)Tax.Percentage / 100) : Price;
    public bool IsLowStock => Stock <= Minimum;
    public bool IsExpired => Expiration.HasValue && Expiration.Value < DateTime.Now;
    public bool IsExpiringSoon => Expiration.HasValue && Expiration.Value <= DateTime.Now.AddDays(30);
    public string StatusText => IsActive ? "Activo" : "Inactivo";
    public string StockStatusText => IsLowStock ? "Stock Bajo" : "Stock Normal";
}
