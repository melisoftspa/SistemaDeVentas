using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Entry
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public Guid? IdProduct { get; set; }

    public string? NameProduct { get; set; }

    public int? Amount { get; set; }

    public double? Total { get; set; }

    public bool? State { get; set; }

    public int? IdUser { get; set; }
}
