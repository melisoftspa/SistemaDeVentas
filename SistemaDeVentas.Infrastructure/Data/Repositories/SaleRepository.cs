using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly SalesSystemDbContext _context;

    public SaleRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sale>> GetByUserAsync(Guid userId)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.IdUser == userId)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetByStateAsync(bool state)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.State == state)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<Sale> CreateAsync(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<Sale> UpdateAsync(Sale sale)
    {
        _context.Entry(sale).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale == null) return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Sales.AnyAsync(s => s.Id == id);
    }

    public async Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Sales.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(s => s.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(s => s.Date <= endDate.Value);

        return await query
            .Where(s => s.State == true)
            .SumAsync(s => (decimal)s.Total);
    }

    public async Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Sales.AsQueryable();

        if (startDate.HasValue)
            query = query.Where(s => s.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(s => s.Date <= endDate.Value);

        return await query
            .Where(s => s.State == true)
            .CountAsync();
    }

    public async Task<IEnumerable<Sale>> GetTopSalesAsync(int count = 10)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.State == true)
            .OrderByDescending(s => s.Total)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> SearchAsync(string searchTerm)
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.Name!.Contains(searchTerm) || 
                       s.User!.Name!.Contains(searchTerm) ||
                       s.Ticket.ToString().Contains(searchTerm))
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<bool> CancelSaleAsync(Guid saleId, string reason)
    {
        var sale = await _context.Sales.FindAsync(saleId);
        if (sale == null) return false;

        sale.State = false;
        sale.Note = $"{sale.Note} - Cancelada: {reason}";
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Sale>> GetPendingSalesAsync()
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.State == false)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Sale>> GetCompletedSalesAsync()
    {
        return await _context.Sales
            .Include(s => s.User)
            .Include(s => s.Details)
            .Where(s => s.State == true)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }
}