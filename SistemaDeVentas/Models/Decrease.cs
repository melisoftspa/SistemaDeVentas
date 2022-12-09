using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Decrease
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public Guid? IdProduct { get; set; }

    public double? Amount { get; set; }

    public double? Tax { get; set; }

    public double? Total { get; set; }

    public string? Note { get; set; }

    public string? Line { get; set; }
}
