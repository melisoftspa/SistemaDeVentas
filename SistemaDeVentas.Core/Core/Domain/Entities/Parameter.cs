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

    // Propiedades adicionales para compatibilidad con configuración
    public string Key => Name ?? string.Empty;
    public string Description => $"{Module ?? "General"} - {Name ?? "Sin nombre"}";
}
