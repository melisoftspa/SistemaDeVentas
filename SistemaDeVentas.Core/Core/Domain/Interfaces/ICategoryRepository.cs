using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetActiveAsync();
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByTextAsync(string text);
    Task<IEnumerable<Category>> SearchAsync(string searchTerm);
    Task<bool> ActivateAsync(Guid id);
    Task<bool> DeactivateAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
    Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
    Task<int> GetProductsCountAsync(Guid categoryId);
}