using System;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Invoice
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public bool? State { get; set; }
    public string? Name { get; set; }
    public double? Amount { get; set; }
    public double? Total { get; set; }
    public string? NameProvider { get; set; }
    public string? Notes { get; set; }
    public bool? StatePayment { get; set; }
    public bool? PaymentCheck { get; set; }
    public string? PaymentCheckNumber { get; set; }
    public DateTime? PaymentCheckDate { get; set; }
    public double? PaymentCheckTotal { get; set; }
    public bool? PaymentCash { get; set; }
    public double? PaymentCashTotal { get; set; }
}
