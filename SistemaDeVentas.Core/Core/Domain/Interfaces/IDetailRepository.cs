using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces;

public interface IDetailRepository
{
    Task<IEnumerable<Detail>> GetAllAsync();
    Task<Detail?> GetByIdAsync(Guid id);
    Task<IEnumerable<Detail>> GetBySaleAsync(Guid saleId);
    Task<IEnumerable<Detail>> GetByProductAsync(Guid productId);
    Task<Detail> CreateAsync(Detail detail);
    Task<Detail> UpdateAsync(Detail detail);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<IEnumerable<Detail>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalAmountBySaleAsync(Guid saleId);
    Task<int> GetItemCountBySaleAsync(Guid saleId);
    Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count = 10);
    Task<bool> DeleteBySaleAsync(Guid saleId);
    Task<decimal> GetSaleTotalAsync(Guid saleId);
    Task<double> GetTotalQuantityBySaleAsync(Guid saleId);
    Task<IEnumerable<Product>> GetMostSoldProductsAsync(int count = 10);
}