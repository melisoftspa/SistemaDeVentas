using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SistemaDeVentas.WinUI.Models
{
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

        public int? IdUser { get; set; }

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

        // Navigation properties
        public User? User { get; set; }
        public List<Detail> Details { get; set; } = new List<Detail>();

        // Computed properties
        public double Subtotal => Details.Sum(d => d.Total);
        public double TotalTax => Details.Sum(d => d.TotalTax);
        public double TotalPayment => PaymentCash + PaymentOther;
        public bool IsCompleted => State && Total > 0;
        public int ItemCount => Details.Count;
        public string FormattedDate => Date.ToString("dd/MM/yyyy HH:mm");
        public string PaymentStatus => TotalPayment >= Total ? "Pagado" : "Pendiente";
    }
}