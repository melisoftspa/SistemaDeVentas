using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class TaxRepository : ITaxRepository
{
    private readonly SalesSystemDbContext _context;

    public TaxRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tax>> GetAllAsync()
    {
        return await _context.Taxes.ToListAsync();
    }

    public async Task<Tax?> GetByIdAsync(Guid id)
    {
        return await _context.Taxes.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tax>> GetExemptAsync()
    {
        return await _context.Taxes
            .Where(t => t.Percentage == 0)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tax>> GetNonExemptAsync()
    {
        return await _context.Taxes
            .Where(t => t.Percentage > 0)
            .ToListAsync();
    }

    public async Task<Tax> CreateAsync(Tax tax)
    {
        _context.Taxes.Add(tax);
        await _context.SaveChangesAsync();
        return tax;
    }

    public async Task<Tax> UpdateAsync(Tax tax)
    {
        _context.Entry(tax).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return tax;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tax = await _context.Taxes.FindAsync(id);
        if (tax == null) return false;

        _context.Taxes.Remove(tax);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Taxes.AnyAsync(t => t.Id == id);
    }

    public async Task<bool> ExistsByValueAsync(string value)
    {
        if (decimal.TryParse(value, out decimal numericValue))
        {
            return await _context.Taxes.AnyAsync(t => t.Percentage == numericValue);
        }
        return false;
    }

    public async Task<IEnumerable<Tax>> SearchAsync(string searchTerm)
    {
        return await _context.Taxes
            .Where(t => t.Percentage.ToString().Contains(searchTerm) ||
                        t.Name.Contains(searchTerm))
            .ToListAsync();
    }
}