using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Subcategory
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public bool? State { get; set; }

    public Guid? IdCategory { get; set; }

    public string? Value { get; set; }

    public string? Text { get; set; }
}
