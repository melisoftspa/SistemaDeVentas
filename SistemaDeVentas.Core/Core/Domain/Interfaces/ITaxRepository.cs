using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces;

public interface ITaxRepository
{
    Task<IEnumerable<Tax>> GetAllAsync();
    Task<Tax?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tax>> GetExemptAsync();
    Task<IEnumerable<Tax>> GetNonExemptAsync();
    Task<Tax> CreateAsync(Tax tax);
    Task<Tax> UpdateAsync(Tax tax);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByValueAsync(string value);
    Task<IEnumerable<Tax>> SearchAsync(string searchTerm);
}