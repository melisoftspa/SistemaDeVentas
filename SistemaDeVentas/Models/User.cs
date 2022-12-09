using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Models;

public partial class User
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? Name { get; set; }

    public bool? InInvoice { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
}
