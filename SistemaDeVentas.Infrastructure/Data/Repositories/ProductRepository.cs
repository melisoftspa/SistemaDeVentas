using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SalesSystemDbContext _context;

    public ProductRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .FirstOrDefaultAsync(p => p.Barcode == barcode && p.IsActive);
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.IdCategory == categoryId && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetBySubcategoryAsync(Guid subcategoryId)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.IdSubcategory == subcategoryId && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockAsync(int minimumStock = 5)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.Stock <= minimumStock && p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => (p.Name.Contains(searchTerm) ||
                         p.Barcode.Contains(searchTerm)) && p.IsActive)
            .ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.IsActive = false; // Soft delete
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByBarcodeAsync(string barcode)
    {
        return await _context.Products.AnyAsync(p => p.Barcode == barcode);
    }

    public async Task<int> GetStockAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        return product?.Stock ?? 0;
    }

    public async Task<bool> UpdateStockAsync(Guid productId, int newStock)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        product.Stock = newStock;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReduceStockAsync(Guid productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null || product.Stock < quantity) return false;

        product.Stock -= quantity;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IncreaseStockAsync(Guid productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        product.Stock += quantity;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Product>> GetActiveProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Tax)
            .Where(p => p.Stock <= threshold && p.IsActive)
            .ToListAsync();
    }

    public async Task<bool> ActivateProductAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        product.IsActive = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateProductAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) return false;

        product.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}