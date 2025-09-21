using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface ITaxService
{
    Task<IEnumerable<Tax>> GetAllTaxesAsync();
    Task<Tax?> GetTaxByIdAsync(Guid id);
    Task<IEnumerable<Tax>> GetActiveTaxesAsync();
    Task<IEnumerable<Tax>> GetInactiveTaxesAsync();
    Task<IEnumerable<Tax>> GetExemptTaxesAsync();
    Task<IEnumerable<Tax>> GetNonExemptTaxesAsync();
    Task<Tax> CreateTaxAsync(Tax tax);
    Task<Tax> UpdateTaxAsync(Tax tax);
    Task<bool> DeleteTaxAsync(Guid id);
    Task<bool> ActivateTaxAsync(Guid id);
    Task<bool> DeactivateTaxAsync(Guid id);
    Task<IEnumerable<Tax>> SearchTaxesAsync(string searchTerm);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByValueAsync(string value);
    Task<decimal> CalculateTaxAmountAsync(decimal baseAmount, Guid taxId);
    Task<int> GetTotalTaxesCountAsync();
    Task<int> GetActiveTaxesCountAsync();
    Task<int> GetInactiveTaxesCountAsync();
}