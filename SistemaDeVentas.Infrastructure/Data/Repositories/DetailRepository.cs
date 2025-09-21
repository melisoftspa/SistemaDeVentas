using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

public class DetailRepository : IDetailRepository
{
    private readonly SalesSystemDbContext _context;

    public DetailRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Detail>> GetAllAsync()
    {
        return await _context.Details
            .Include(d => d.Sale)
            .Include(d => d.Product)
            .ToListAsync();
    }

    public async Task<Detail?> GetByIdAsync(Guid id)
    {
        return await _context.Details
            .Include(d => d.Sale)
            .Include(d => d.Product)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Detail>> GetBySaleAsync(Guid saleId)
    {
        return await _context.Details
            .Include(d => d.Sale)
            .Include(d => d.Product)
            .Where(d => d.IdSale == saleId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Detail>> GetByProductAsync(Guid productId)
    {
        return await _context.Details
            .Include(d => d.Sale)
            .Include(d => d.Product)
            .Where(d => d.IdProduct == productId)
            .ToListAsync();
    }

    public async Task<Detail> CreateAsync(Detail detail)
    {
        _context.Details.Add(detail);
        await _context.SaveChangesAsync();
        return detail;
    }

    public async Task<Detail> UpdateAsync(Detail detail)
    {
        _context.Entry(detail).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return detail;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var detail = await _context.Details.FindAsync(id);
        if (detail == null) return false;

        _context.Details.Remove(detail);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Details.AnyAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Detail>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Details
            .Include(d => d.Sale)
            .Include(d => d.Product)
            .Where(d => d.Sale != null && d.Sale.Date >= startDate && d.Sale.Date <= endDate)
            .ToListAsync();
    }

    public async Task<decimal> GetTotalAmountBySaleAsync(Guid saleId)
    {
        return await _context.Details
            .Where(d => d.IdSale == saleId)
            .SumAsync(d => (decimal)d.Total);
    }

    public async Task<int> GetItemCountBySaleAsync(Guid saleId)
    {
        return await _context.Details
            .Where(d => d.IdSale == saleId)
            .SumAsync(d => (int)d.Amount);
    }

    public async Task<decimal> GetTotalBySaleAsync(Guid saleId)
    {
        return await _context.Details
            .Where(d => d.IdSale == saleId)
            .SumAsync(d => (decimal)d.Total);
    }

    public async Task<double> GetTotalQuantityBySaleAsync(Guid saleId)
    {
        return await _context.Details
            .Where(d => d.IdSale == saleId)
            .SumAsync(d => d.Amount);
    }

    public async Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count = 10)
    {
        return await _context.Details
            .Include(d => d.Product)
            .GroupBy(d => d.IdProduct)
            .Select(g => new
            {
                ProductId = g.Key,
                TotalQuantity = g.Sum(d => d.Amount),
                Product = g.First().Product
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(count)
            .Where(x => x.Product != null)
            .Select(x => x.Product!)
            .ToListAsync();
    }

    public async Task<bool> DeleteBySaleAsync(Guid saleId)
    {
        var details = await _context.Details
            .Where(d => d.IdSale == saleId)
            .ToListAsync();

        if (!details.Any()) return false;

        _context.Details.RemoveRange(details);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetSaleTotalAsync(Guid saleId)
    {
        return await GetTotalBySaleAsync(saleId);
    }

    public async Task<IEnumerable<Product>> GetMostSoldProductsAsync(int count = 10)
    {
        return await GetTopSellingProductsAsync(count);
    }
}