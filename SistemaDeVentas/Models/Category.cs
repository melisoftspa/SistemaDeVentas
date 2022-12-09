using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public int? Value { get; set; }

    public string? Text { get; set; }

    public bool? State { get; set; }
}
