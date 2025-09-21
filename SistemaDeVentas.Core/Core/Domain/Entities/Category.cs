using System;
using System.Collections.Generic;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
