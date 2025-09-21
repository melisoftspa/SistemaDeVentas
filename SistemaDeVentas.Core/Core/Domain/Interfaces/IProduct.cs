using System;

namespace SistemaDeVentas.Core.Domain.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; set; }
        string Name { get; set; }
        double SalePrice { get; set; }
        string? Barcode { get; set; }
        int Stock { get; set; }
        double Price { get; set; }
        bool Exenta { get; set; }
        Guid? IdCategory { get; set; }
        bool IsActive { get; set; }
        bool IsLowStock { get; }
    }
}