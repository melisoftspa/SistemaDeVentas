using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.WinUI.Models
{
    public class Subcategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public int Value { get; set; }

        [Required(ErrorMessage = "El texto de la subcategoría es requerido")]
        [StringLength(500, ErrorMessage = "El texto no puede exceder 500 caracteres")]
        public string Text { get; set; } = string.Empty;

        public bool State { get; set; } = true;

        public Guid? IdCategory { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        // Computed properties
        public string DisplayText => Text;
        public int ProductCount => Products?.Count ?? 0;
        public string StatusText => State ? "Activa" : "Inactiva";
        public string FullPath => Category != null ? $"{Category.Text} > {Text}" : Text;
    }
}