using System;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Subcategory
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public Guid? IdCategory { get; set; }
    public int? State { get; set; }
    public string? Text { get; set; }
    public string? Value { get; set; }
}
