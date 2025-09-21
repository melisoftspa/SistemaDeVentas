using System;
using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.WinUI.Models
{
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

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public double Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El impuesto debe ser mayor o igual a 0")]
        public double Tax { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public double Total { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total de impuesto debe ser mayor o igual a 0")]
        public double TotalTax { get; set; }

        public bool State { get; set; } = true;

        // Navigation properties
        public ISale? Sale { get; set; }
        public IProduct? Product { get; set; }

        // Computed properties
        public double Subtotal => Amount * Price;
        public double TaxAmount => Subtotal * (Tax / 100);
        public double LineTotal => Subtotal + TaxAmount;
        public string FormattedAmount => Amount.ToString("N2");
        public string FormattedPrice => Price.ToString("C");
        public string FormattedTotal => Total.ToString("C");
        public string DisplayLine => $"{ProductName} x {FormattedAmount} = {FormattedTotal}";
    }
}