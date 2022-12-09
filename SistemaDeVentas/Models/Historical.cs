using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class Historical
{
    public Guid Id { get; set; }

    public DateTime? Date { get; set; }

    public int? IdUser { get; set; }

    public string? Action { get; set; }

    public string? Before { get; set; }

    public string? After { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public Guid? IdProduct { get; set; }
}
