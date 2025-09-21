using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SalesSystemDbContext _context;

    public CategoryRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByTextAsync(string text)
    {
        return await _context.Categories.AnyAsync(c => c.Name == text);
    }

    public async Task<IEnumerable<Category>> GetActiveAsync()
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .Include(c => c.Products)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> SearchAsync(string searchTerm)
    {
        return await _context.Categories
            .Where(c => c.Name.Contains(searchTerm))
            .ToListAsync();
    }

    public async Task<bool> ActivateAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        category.IsActive = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeactivateAsync(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        category.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Categories.AnyAsync(c => c.Name == name);
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
    {
        return await _context.Categories
            .Where(c => _context.Products.Any(p => p.IdCategory == c.Id))
            .ToListAsync();
    }

    public async Task<int> GetProductCountAsync(Guid categoryId)
    {
        return await _context.Products
            .CountAsync(p => p.IdCategory == categoryId);
    }

    public async Task<int> GetProductsCountAsync(Guid categoryId)
    {
        return await GetProductCountAsync(categoryId);
    }
}