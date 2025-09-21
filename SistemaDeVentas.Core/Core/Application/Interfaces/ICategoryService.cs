using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetActiveCategoriesAsync();
    Task<IEnumerable<Category>> GetInactiveCategoriesAsync();
    Task<IEnumerable<Category>> GetCategoriesWithProductsAsync();
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<bool> ActivateCategoryAsync(Guid id);
    Task<bool> DeactivateCategoryAsync(Guid id);
    Task<IEnumerable<Category>> SearchCategoriesAsync(string searchTerm);
    Task<int> GetProductsCountAsync(Guid categoryId);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameAsync(string name);
    Task<int> GetTotalCategoriesCountAsync();
    Task<int> GetActiveCategoriesCountAsync();
    Task<int> GetInactiveCategoriesCountAsync();
}