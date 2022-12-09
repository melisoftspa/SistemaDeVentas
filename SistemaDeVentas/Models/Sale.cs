using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Sale
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Name { get; set; }

    public double? Amount { get; set; }

    public double? Tax { get; set; }

    public double? Total { get; set; }

    public int? IdUser { get; set; }

    public string? Note { get; set; }

    public double? Change { get; set; }

    public double? PaymentCash { get; set; }

    public double? PaymentOther { get; set; }

    public int? Ticket { get; set; }

    public bool? State { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
