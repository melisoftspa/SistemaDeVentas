using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SistemaDeVentas.Core.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; } = DateTime.Now;

    [StringLength(500, ErrorMessage = "El nombre no puede exceder 500 caracteres")]
    public string? Name { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
    public double Amount { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El impuesto debe ser mayor o igual a 0")]
    public double Tax { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public double Total { get; set; }

    public Guid? IdUser { get; set; }

    [StringLength(1000, ErrorMessage = "La nota no puede exceder 1000 caracteres")]
    public string? Note { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El cambio debe ser mayor o igual a 0")]
    public double Change { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El pago en efectivo debe ser mayor o igual a 0")]
    public double PaymentCash { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Otro pago debe ser mayor o igual a 0")]
    public double PaymentOther { get; set; }

    public int? Ticket { get; set; }

    public bool State { get; set; } = true;

    public bool DteGenerated { get; set; } = false;
    public int? Folio { get; set; }
    public string? DteXml { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public List<Detail> Details { get; set; } = new List<Detail>();

    // Computed properties
    public double Subtotal => Details.Sum(d => d.Total);
    public double TotalTax => Details.Sum(d => d.TotalTax);
    public int TotalItems => ((int)Details.Sum(d => d.Amount));
    public string StatusText => State ? "Completada" : "Cancelada";
    public string PaymentMethodText => GetPaymentMethodText();
    public bool HasDiscount => Details.Any(d => d.Discount > 0);
    public double TotalDiscount => Details.Sum(d => d.Discount);

    private string GetPaymentMethodText()
    {
        if (PaymentCash > 0 && PaymentOther > 0)
            return "Mixto";
        else if (PaymentCash > 0)
            return "Efectivo";
        else if (PaymentOther > 0)
            return "Otro";
        else
            return "No especificado";
    }
}
