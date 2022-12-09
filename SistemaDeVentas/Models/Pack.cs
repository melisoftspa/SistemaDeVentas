using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Pack
{
    public Guid? Id { get; set; }

    public DateTime? Date { get; set; }

    public Guid? IdPack { get; set; }

    public Guid? IdProduct { get; set; }

    public string? BarCode { get; set; }

    public string? Name { get; set; }

    public double? Amount { get; set; }

    public string? Line { get; set; }

    public bool? State { get; set; }
}
