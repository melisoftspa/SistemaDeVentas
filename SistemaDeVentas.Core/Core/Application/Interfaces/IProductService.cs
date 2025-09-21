using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product?> GetProductByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int minimumStock = 5);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(Guid id);
    Task<bool> ValidateProductAsync(Product product);
    Task<bool> UpdateStockAsync(Guid productId, int newStock);
    Task<bool> ReduceStockAsync(Guid productId, int quantity);
    Task<bool> IncreaseStockAsync(Guid productId, int quantity);
    Task<bool> IsProductAvailableAsync(Guid productId, int requestedQuantity);
    Task<decimal> CalculateProductTotalAsync(Guid productId, int quantity, decimal? discount = null);
}
