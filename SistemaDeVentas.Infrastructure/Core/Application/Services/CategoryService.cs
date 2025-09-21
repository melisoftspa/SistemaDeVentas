using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithProductsAsync()
    {
        return await _categoryRepository.GetCategoriesWithProductsAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        // Validar que el nombre no esté en uso
        if (await _categoryRepository.ExistsByNameAsync(category.Name))
        {
            throw new ArgumentException("Ya existe una categoría con ese nombre.");
        }

        category.CreatedAt = DateTime.UtcNow;
        category.IsActive = true;

        return await _categoryRepository.CreateAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        if (!await _categoryRepository.ExistsAsync(category.Id))
        {
            throw new ArgumentException("La categoría especificada no existe.");
        }

        // Verificar que el nombre no esté en uso por otra categoría
        var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
        if (existingCategory != null && existingCategory.Name != category.Name)
        {
            if (await _categoryRepository.ExistsByNameAsync(category.Name))
            {
                throw new ArgumentException("Ya existe una categoría con ese nombre.");
            }
        }

        return await _categoryRepository.UpdateAsync(category);
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        // Verificar si la categoría tiene productos asociados
        var productsCount = await _categoryRepository.GetProductsCountAsync(id);
        if (productsCount > 0)
        {
            throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos asociados.");
        }

        return await _categoryRepository.DeleteAsync(id);
    }

    public async Task<bool> DeactivateCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }

        category.IsActive = false;
        await _categoryRepository.UpdateAsync(category);
        return true;
    }

    public async Task<bool> ActivateCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return false;
        }

        category.IsActive = true;
        await _categoryRepository.UpdateAsync(category);
        return true;
    }

    public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
    {
        var allCategories = await _categoryRepository.GetAllAsync();
        return allCategories.Where(c => c.IsActive);
    }

    public async Task<IEnumerable<Category>> GetInactiveCategoriesAsync()
    {
        var allCategories = await _categoryRepository.GetAllAsync();
        return allCategories.Where(c => !c.IsActive);
    }

    public async Task<IEnumerable<Category>> SearchCategoriesAsync(string searchTerm)
    {
        return await _categoryRepository.SearchAsync(searchTerm);
    }

    public async Task<int> GetProductsCountAsync(Guid categoryId)
    {
        return await _categoryRepository.GetProductsCountAsync(categoryId);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _categoryRepository.ExistsAsync(id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _categoryRepository.ExistsByNameAsync(name);
    }

    public async Task<int> GetTotalCategoriesCountAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Count();
    }

    public async Task<int> GetActiveCategoriesCountAsync()
    {
        var activeCategories = await GetActiveCategoriesAsync();
        return activeCategories.Count();
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithMostProductsAsync(int count = 10)
    {
        var categoriesWithProducts = await _categoryRepository.GetCategoriesWithProductsAsync();
        return categoriesWithProducts
            .OrderByDescending(c => c.Products?.Count() ?? 0)
            .Take(count);
    }

    public async Task<int> GetInactiveCategoriesCountAsync()
    {
        var inactiveCategories = await GetInactiveCategoriesAsync();
        return inactiveCategories.Count();
    }
}