using SistemaDeVentas.Core.Domain.Entities;

namespace SistemaDeVentas.Core.Application.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<Sale>> GetAllSalesAsync();
    Task<Sale?> GetSaleByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetSalesByUserAsync(Guid userId);
    Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<Sale> CreateSaleAsync(Sale sale, IEnumerable<Detail> details);
    Task<Sale> UpdateSaleAsync(Sale sale);
    Task<bool> CancelSaleAsync(Guid saleId, string reason);
    Task<bool> CompleteSaleAsync(Guid saleId);
    Task<bool> CompleteSaleWithDteAsync(Guid saleId, int tipoDocumento);
    Task<bool> UpdateSaleDteInfoAsync(Guid saleId, int dteFolio, string dteType, Guid? cafId, string dteXml);
    Task<bool> ValidateSaleAsync(Sale sale, IEnumerable<Detail> details);
    Task<decimal> CalculateSaleTotalAsync(IEnumerable<Detail> details);
    Task<decimal> CalculateTaxAmountAsync(IEnumerable<Detail> details);
    Task<bool> ProcessPaymentAsync(Guid saleId, decimal cashAmount, decimal otherAmount);
    Task<IEnumerable<Sale>> GetPendingSalesAsync();
    Task<IEnumerable<Sale>> GetCompletedSalesAsync();
    Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null);
    Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null);
}