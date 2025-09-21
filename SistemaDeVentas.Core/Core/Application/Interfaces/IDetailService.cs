using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface IDetailService
{
    Task<IEnumerable<Detail>> GetAllDetailsAsync();
    Task<Detail?> GetDetailByIdAsync(Guid id);
    Task<IEnumerable<Detail>> GetDetailsBySaleAsync(Guid saleId);
    Task<IEnumerable<Detail>> GetDetailsByProductAsync(Guid productId);
    Task<Detail> CreateDetailAsync(Detail detail);
    Task<Detail> UpdateDetailAsync(Detail detail);
    Task<bool> DeleteDetailAsync(Guid id);
    Task<bool> DeleteDetailsBySaleAsync(Guid saleId);
    Task<decimal> CalculateSaleTotalAsync(Guid saleId);
    Task<int> GetTotalQuantityBySaleAsync(Guid saleId);
    Task<IEnumerable<Product>> GetMostSoldProductsAsync(int count = 10);
    Task<IEnumerable<Detail>> GetDetailsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalSalesAmountByProductAsync(Guid productId, DateTime? startDate = null, DateTime? endDate = null);
    Task<int> GetTotalQuantitySoldByProductAsync(Guid productId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<Detail>> GetTopSellingDetailsAsync(int count = 10);
    Task<bool> ExistsAsync(Guid id);
    Task<decimal> GetAverageDetailValueAsync();
    Task<IEnumerable<Detail>> GetDetailsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<Detail>> GetDetailsByQuantityRangeAsync(int minQuantity, int maxQuantity);
}