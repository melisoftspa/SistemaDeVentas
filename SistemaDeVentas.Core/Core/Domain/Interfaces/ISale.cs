using System;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface ISale
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        double Amount { get; set; }
        double Tax { get; set; }
        double Total { get; set; }
        int? IdUser { get; set; }
        string? Note { get; set; }
        double Change { get; set; }
        double PaymentCash { get; set; }
        double PaymentOther { get; set; }
        int? Ticket { get; set; }
        bool State { get; set; }
        double Subtotal { get; }
        double TotalTax { get; }
        double TotalPayment { get; }
        bool IsCompleted { get; }
        int ItemCount { get; }
        string FormattedDate { get; }
        string PaymentStatus { get; }
    }
}