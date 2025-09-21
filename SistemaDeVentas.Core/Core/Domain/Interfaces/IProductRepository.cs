using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetBySubcategoryAsync(Guid subcategoryId);
    Task<IEnumerable<Product>> GetLowStockAsync(int minimumStock = 5);
    Task<IEnumerable<Product>> GetActiveProductsAsync();
    Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByBarcodeAsync(string barcode);
    Task<int> GetStockAsync(Guid productId);
    Task<bool> UpdateStockAsync(Guid productId, int newStock);
    Task<bool> ReduceStockAsync(Guid productId, int quantity);
    Task<bool> IncreaseStockAsync(Guid productId, int quantity);
    Task<bool> ActivateProductAsync(Guid productId);
    Task<bool> DeactivateProductAsync(Guid productId);
}