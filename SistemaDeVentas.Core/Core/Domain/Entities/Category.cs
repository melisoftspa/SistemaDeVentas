using System;
using System.Collections.Generic;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Category : ICategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Interface implementation
    public string Text
    {
        get => Name;
        set => Name = value;
    }

    public bool State
    {
        get => IsActive;
        set => IsActive = value;
    }

    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
