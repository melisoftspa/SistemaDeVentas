using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;

    public TaxService(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public async Task<IEnumerable<Tax>> GetAllTaxesAsync()
    {
        return await _taxRepository.GetAllAsync();
    }

    public async Task<Tax?> GetTaxByIdAsync(Guid id)
    {
        return await _taxRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Tax>> GetExemptTaxesAsync()
    {
        return await _taxRepository.GetExemptAsync();
    }

    public async Task<IEnumerable<Tax>> GetNonExemptTaxesAsync()
    {
        return await _taxRepository.GetNonExemptAsync();
    }

    public async Task<Tax> CreateTaxAsync(Tax tax)
    {
        // Validar que no exista otro impuesto con el mismo valor
        if (await _taxRepository.ExistsByValueAsync(tax.Percentage.ToString()))
        {
            throw new ArgumentException("Ya existe un impuesto con ese valor.");
        }

        tax.CreatedAt = DateTime.UtcNow;
        tax.IsActive = true;

        return await _taxRepository.CreateAsync(tax);
    }

    public async Task<Tax> UpdateTaxAsync(Tax tax)
    {
        if (!await _taxRepository.ExistsAsync(tax.Id))
        {
            throw new ArgumentException("El impuesto especificado no existe.");
        }

        // Verificar que el valor no esté en uso por otro impuesto
        var existingTax = await _taxRepository.GetByIdAsync(tax.Id);
        if (existingTax != null && existingTax.Percentage != tax.Percentage)
        {
            if (await _taxRepository.ExistsByValueAsync(tax.Percentage.ToString()))
            {
                throw new ArgumentException("Ya existe un impuesto con ese valor.");
            }
        }

        return await _taxRepository.UpdateAsync(tax);
    }

    public async Task<bool> DeleteTaxAsync(Guid id)
    {
        return await _taxRepository.DeleteAsync(id);
    }

    public async Task<bool> DeactivateTaxAsync(Guid id)
    {
        var tax = await _taxRepository.GetByIdAsync(id);
        if (tax == null)
        {
            return false;
        }

        tax.IsActive = false;
        await _taxRepository.UpdateAsync(tax);
        return true;
    }

    public async Task<bool> ActivateTaxAsync(Guid id)
    {
        var tax = await _taxRepository.GetByIdAsync(id);
        if (tax == null)
        {
            return false;
        }

        tax.IsActive = true;
        await _taxRepository.UpdateAsync(tax);
        return true;
    }

    public async Task<IEnumerable<Tax>> GetActiveTaxesAsync()
    {
        var allTaxes = await _taxRepository.GetAllAsync();
        return allTaxes.Where(t => t.IsActive);
    }

    public async Task<IEnumerable<Tax>> GetInactiveTaxesAsync()
    {
        var allTaxes = await _taxRepository.GetAllAsync();
        return allTaxes.Where(t => !t.IsActive);
    }

    public async Task<IEnumerable<Tax>> SearchTaxesAsync(string searchTerm)
    {
        return await _taxRepository.SearchAsync(searchTerm);
    }

    public async Task<decimal> CalculateTaxAmount(decimal baseAmount, Guid taxId)
    {
        var tax = await _taxRepository.GetByIdAsync(taxId);
        if (tax == null)
        {
            throw new ArgumentException("El impuesto especificado no existe.");
        }

        if (tax.IsExempt)
        {
            return 0;
        }

        return baseAmount * (tax.Percentage / 100);
    }

    public async Task<decimal> CalculateTotalWithTax(decimal baseAmount, Guid taxId)
    {
        var taxAmount = await CalculateTaxAmount(baseAmount, taxId);
        return baseAmount + taxAmount;
    }

    public async Task<decimal> CalculateBaseAmountFromTotal(decimal totalAmount, Guid taxId)
    {
        var tax = await _taxRepository.GetByIdAsync(taxId);
        if (tax == null)
        {
            throw new ArgumentException("El impuesto especificado no existe.");
        }

        if (tax.IsExempt)
        {
            return totalAmount;
        }

        var taxRate = (decimal)tax.Percentage / 100;
        return totalAmount / (1 + taxRate);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _taxRepository.ExistsAsync(id);
    }

    public async Task<bool> ExistsByValueAsync(double value)
    {
        return await _taxRepository.ExistsByValueAsync(value.ToString());
    }

    public async Task<bool> ExistsByValueAsync(string value)
    {
        if (double.TryParse(value, out double numericValue))
        {
            return await _taxRepository.ExistsByValueAsync(numericValue.ToString());
        }
        return false;
    }

    public async Task<decimal> CalculateTaxAmountAsync(decimal baseAmount, Guid taxId)
    {
        var tax = await _taxRepository.GetByIdAsync(taxId);
        if (tax == null)
        {
            throw new ArgumentException("El impuesto especificado no existe.");
        }

        if (tax.IsExempt)
        {
            return 0;
        }

        return baseAmount * ((decimal)tax.Percentage / 100);
    }

    public async Task<int> GetTotalTaxesCountAsync()
    {
        var taxes = await _taxRepository.GetAllAsync();
        return taxes.Count();
    }

    public async Task<int> GetActiveTaxesCountAsync()
    {
        var activeTaxes = await GetActiveTaxesAsync();
        return activeTaxes.Count();
    }

    public async Task<Tax?> GetDefaultTaxAsync()
    {
        // Buscar el impuesto más común (por ejemplo, IVA 19% en Chile)
        var taxes = await _taxRepository.GetAllAsync();
        return taxes.FirstOrDefault(t => !t.IsExempt && t.Percentage == 19.0m);
    }

    public async Task<IEnumerable<Tax>> GetTaxesByRangeAsync(double minValue, double maxValue)
    {
        var allTaxes = await _taxRepository.GetAllAsync();
        return allTaxes.Where(t => (double)t.Percentage >= minValue && (double)t.Percentage <= maxValue);
    }

    public async Task<int> GetInactiveTaxesCountAsync()
    {
        var inactiveTaxes = await GetInactiveTaxesAsync();
        return inactiveTaxes.Count();
    }
}