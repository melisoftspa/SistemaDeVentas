using System;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface IDetail
    {
        Guid Id { get; set; }
        string ProductName { get; set; }
        double Amount { get; set; }
        double Price { get; set; }
        double Tax { get; set; }
        double Total { get; set; }
        double Subtotal { get; }
        double TaxAmount { get; }
        double TotalTax { get; }
    }
}