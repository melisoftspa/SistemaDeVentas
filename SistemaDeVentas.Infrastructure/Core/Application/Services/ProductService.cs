using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITaxRepository _taxRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ITaxRepository taxRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _taxRepository = taxRepository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product?> GetProductByBarcodeAsync(string barcode)
    {
        return await _productRepository.GetByBarcodeAsync(barcode);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await _productRepository.GetByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _productRepository.GetActiveProductsAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold = 10)
    {
        return await _productRepository.GetLowStockProductsAsync(threshold);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        // Validar que la categoría existe
        if (product.IdCategory.HasValue && !await _categoryRepository.ExistsAsync(product.IdCategory.Value))
        {
            throw new ArgumentException("La categoría especificada no existe.");
        }

        // Validar que el código de barras no existe
        if (await _productRepository.ExistsByBarcodeAsync(product.Barcode))
        {
            throw new ArgumentException("Ya existe un producto con este código de barras.");
        }

        return await _productRepository.CreateAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        // Validar que el producto existe
        if (!await _productRepository.ExistsAsync(product.Id))
        {
            throw new ArgumentException("El producto especificado no existe.");
        }

        // Validar que la categoría existe
        if (product.IdCategory.HasValue && !await _categoryRepository.ExistsAsync(product.IdCategory.Value))
        {
            throw new ArgumentException("La categoría especificada no existe.");
        }

        return await _productRepository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        return await _productRepository.DeleteAsync(id);
    }

    public async Task<bool> UpdateStockAsync(Guid productId, int newStock)
    {
        return await _productRepository.UpdateStockAsync(productId, newStock);
    }

    public async Task<bool> ReduceStockAsync(Guid productId, int quantity)
    {
        return await _productRepository.ReduceStockAsync(productId, quantity);
    }

    public async Task<bool> IncreaseStockAsync(Guid productId, int quantity)
    {
        return await _productRepository.IncreaseStockAsync(productId, quantity);
    }

    public async Task<decimal> CalculateProductTotalAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            throw new ArgumentException("El producto especificado no existe.");

        return (decimal)product.SalePrice * quantity;
    }

    public async Task<decimal> CalculateProductTotalWithTaxAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            throw new ArgumentException("El producto especificado no existe.");

        var baseTotal = (decimal)product.SalePrice * quantity;
        
        // Si el producto tiene impuesto, calcularlo
        if (product.IdTax.HasValue)
        {
            var tax = await _taxRepository.GetByIdAsync(product.IdTax.Value);
            if (tax != null)
            {
                var taxAmount = baseTotal * ((decimal)tax.Percentage / 100);
                return baseTotal + taxAmount;
            }
        }

        return baseTotal;
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await _productRepository.SearchAsync(searchTerm);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _productRepository.ExistsAsync(id);
    }

    public async Task<bool> ExistsByBarcodeAsync(string barcode)
    {
        return await _productRepository.ExistsByBarcodeAsync(barcode);
    }

    public async Task<bool> HasSufficientStockAsync(Guid productId, int requiredQuantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return product != null && product.Stock >= requiredQuantity;
    }

    public async Task<bool> ActivateProductAsync(Guid productId)
    {
        return await _productRepository.ActivateProductAsync(productId);
    }

    public async Task<bool> DeactivateProductAsync(Guid productId)
    {
        return await _productRepository.DeactivateProductAsync(productId);
    }

    public async Task<bool> ValidateProductAsync(Product product)
    {
        // Validar que el producto no sea nulo
        if (product == null)
            return false;

        // Validar que tenga nombre
        if (string.IsNullOrWhiteSpace(product.Name))
            return false;

        // Validar que tenga precio válido
        if (product.SalePrice <= 0)
            return false;

        // Validar que tenga stock válido
        if (product.Stock < 0)
            return false;

        // Validar que tenga categoría válida
        if (!product.IdCategory.HasValue || product.IdCategory.Value == Guid.Empty)
            return false;

        // Verificar que la categoría existe
        var categoryExists = await _categoryRepository.ExistsAsync(product.IdCategory.Value);
        if (!categoryExists)
            return false;

        // Validar código de barras único si se proporciona
        if (!string.IsNullOrWhiteSpace(product.Barcode))
        {
            var existingProduct = await _productRepository.GetByBarcodeAsync(product.Barcode);
            if (existingProduct != null && existingProduct.Id != product.Id)
                return false;
        }

        return true;
    }

    public async Task<bool> IsProductAvailableAsync(Guid productId, int requestedQuantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            return false;

        return product.Stock >= requestedQuantity && product.IsActive;
    }

    public async Task<decimal> CalculateProductTotalAsync(Guid productId, int quantity, decimal? discount = null)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
            return 0;

        decimal subtotal = (decimal)product.SalePrice * quantity;
        
        if (discount.HasValue && discount.Value > 0)
        {
            subtotal -= discount.Value;
        }

        return Math.Max(0, subtotal); // Asegurar que no sea negativo
    }
}
