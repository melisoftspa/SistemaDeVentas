using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Detail
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public Guid? IdSale { get; set; }

    public Guid? IdProduct { get; set; }

    public string? ProductName { get; set; }

    public double? Amount { get; set; }

    public double? Price { get; set; }

    public double? Tax { get; set; }

    public double? Total { get; set; }

    public double? TotalTax { get; set; }

    public bool? State { get; set; }
}
