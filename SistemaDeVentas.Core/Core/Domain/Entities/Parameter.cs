using System;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Parameter
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Module { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }
}
