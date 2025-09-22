using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Core.Domain.Entities;

public partial class Category : ICategory
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int? Value { get; set; }

    public string? Text { get; set; }

    public bool State { get; set; } = true;

    [NotMapped]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public DateTime? UpdatedAt { get; set; }

    // Interface implementation
    public string Name
    {
        get => Text ?? string.Empty;
        set => Text = value;
    }

    public bool IsActive
    {
        get => State;
        set => State = value;
    }

    // Navigation properties
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
