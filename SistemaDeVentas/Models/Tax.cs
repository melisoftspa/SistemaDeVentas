using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Tax
{
    public Guid Id { get; set; }

    public string? Value { get; set; }

    public string? Text { get; set; }

    public bool? Exenta { get; set; }
}
