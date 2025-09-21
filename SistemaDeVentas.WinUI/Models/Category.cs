using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.WinUI.Models
{
    public class Category : ICategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public int Value { get; set; }

        [Required(ErrorMessage = "El texto de la categoría es requerido")]
        [StringLength(500, ErrorMessage = "El texto no puede exceder 500 caracteres")]
        public string Text { get; set; } = string.Empty;

        public bool State { get; set; } = true;

        // Navigation properties
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

        // Computed properties
        public string DisplayText => Text;
        public int ProductCount => Products?.Count ?? 0;
        public string StatusText => State ? "Activa" : "Inactiva";
    }
}