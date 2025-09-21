using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Domain.Interfaces;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale?> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetByUserAsync(Guid userId);
    Task<IEnumerable<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Sale>> GetByStateAsync(bool state);
    Task<Sale> CreateAsync(Sale sale);
    Task<Sale> UpdateAsync(Sale sale);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<Sale>> GetTopSalesAsync(int count = 10);
    Task<IEnumerable<Sale>> SearchAsync(string searchTerm);
    Task<bool> CancelSaleAsync(Guid saleId, string reason);
    Task<IEnumerable<Sale>> GetPendingSalesAsync();
    Task<IEnumerable<Sale>> GetCompletedSalesAsync();
}