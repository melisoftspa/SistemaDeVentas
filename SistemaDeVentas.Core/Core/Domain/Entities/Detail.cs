using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Core.Domain.Entities;

public class Detail : IDetail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime Date { get; set; } = DateTime.Now;
    
    public Guid? IdSale { get; set; }
    
    public Guid? IdProduct { get; set; }
    
    [Required(ErrorMessage = "El nombre del producto es requerido")]
    [StringLength(500, ErrorMessage = "El nombre del producto no puede exceder 500 caracteres")]
    public string ProductName { get; set; } = string.Empty;
    
    [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public double Amount { get; set; }

    [NotMapped]
    [Range(0.01, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public double Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public double Price { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "El impuesto debe ser mayor o igual a 0")]
    public double Tax { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public double Total { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "El total del impuesto debe ser mayor o igual a 0")]
    public double TotalTax { get; set; }
    
    [NotMapped]
    [Range(0, double.MaxValue, ErrorMessage = "El descuento debe ser mayor o igual a 0")]
    public double Discount { get; set; }
    
    public bool State { get; set; } = true;

    // Navigation properties
    public Sale? Sale { get; set; }
    public Product? Product { get; set; }

    // Computed properties
    public double Subtotal => Amount * Price;
    public double DiscountAmount => Subtotal * (Discount / 100);
    public double NetAmount => Subtotal - DiscountAmount;
    public double TaxAmount => NetAmount * (Tax / 100);
    public double FinalTotal => NetAmount + TaxAmount;
}
