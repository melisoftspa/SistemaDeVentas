using System;
using System.Collections.Generic;
using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.WinUI.Models
{
    public static class ProductExtensions
    {
        public static Product ToWinUIProduct(this Core.Domain.Entities.Product coreProduct)
        {
            if (coreProduct == null)
                throw new ArgumentNullException(nameof(coreProduct));

            return new Product
            {
                Id = coreProduct.Id,
                Date = (DateTime)coreProduct.CreatedAt,
                State = coreProduct.IsActive,
                Name = coreProduct.Name,
                Amount = coreProduct.Amount,
                SalePrice = coreProduct.SalePrice,
                Minimum = coreProduct.Minimum,
                BarCode = coreProduct.Barcode,
                Photo = coreProduct.Photo,
                Stock = coreProduct.Stock,
                Price = coreProduct.Price,
                Exenta = coreProduct.Exenta,
                IdTax = coreProduct.IdTax,
                Expiration = coreProduct.Expiration,
                IdCategory = coreProduct.IdCategory,
                IsPack = coreProduct.IsPack,
                IdPack = coreProduct.IdPack,
                Line = coreProduct.Line,
                IdSubcategory = coreProduct.IdSubcategory,
                Category = coreProduct.Category?.ToWinUICategory(),
                Subcategory = coreProduct.Subcategory?.ToWinUISubcategory(),
                Tax = coreProduct.Tax?.ToWinUITax()
            };
        }

        public static Category ToWinUICategory(this Core.Domain.Entities.Category coreCategory)
        {
            if (coreCategory == null)
                throw new ArgumentNullException(nameof(coreCategory));

            return new Category
            {
                Id = coreCategory.Id,
                Value = 0, // No hay equivalente directo en Core
                Text = coreCategory.Name,
                State = coreCategory.IsActive,
                Products = new List<Product>(), // Se mapeará después si es necesario
                Subcategories = new List<Subcategory>() // Se mapeará después si es necesario
            };
        }

        public static Subcategory ToWinUISubcategory(this Core.Domain.Entities.Subcategory coreSubcategory)
        {
            if (coreSubcategory == null)
                throw new ArgumentNullException(nameof(coreSubcategory));

            return new Subcategory
            {
                Id = coreSubcategory.Id,
                Value = string.IsNullOrEmpty(coreSubcategory.Value) ? 0 : int.TryParse(coreSubcategory.Value, out int val) ? val : 0,
                Text = coreSubcategory.Text ?? string.Empty,
                State = coreSubcategory.State == 1,
                IdCategory = coreSubcategory.IdCategory,
                Category = null, // Se mapeará después si es necesario
                Products = new List<Product>() // Se mapeará después si es necesario
            };
        }

        public static Tax ToWinUITax(this Core.Domain.Entities.Tax coreTax)
        {
            if (coreTax == null)
                throw new ArgumentNullException(nameof(coreTax));

            return new Tax
            {
                Id = coreTax.Id,
                Name = coreTax.Name,
                Percentage = (double)coreTax.Percentage,
                State = coreTax.IsActive,
                Date = coreTax.CreatedAt,
                Description = coreTax.DisplayText,
                Products = new List<Product>() // Se mapeará después si es necesario
            };
        }
    }
}