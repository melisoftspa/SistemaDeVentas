using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDetailRepository _detailRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDteSaleService _dteSaleService;

    public SaleService(
        ISaleRepository saleRepository,
        IDetailRepository detailRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IDteSaleService dteSaleService)
    {
        _saleRepository = saleRepository;
        _detailRepository = detailRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _dteSaleService = dteSaleService;
    }

    public async Task<IEnumerable<Sale>> GetAllSalesAsync()
    {
        return await _saleRepository.GetAllAsync();
    }

    public async Task<Sale?> GetSaleByIdAsync(Guid id)
    {
        return await _saleRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Sale>> GetSalesByUserAsync(Guid userId)
    {
        return await _saleRepository.GetByUserAsync(userId);
    }

    public async Task<IEnumerable<Sale>> GetSalesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _saleRepository.GetByDateRangeAsync(startDate, endDate);
    }

    public async Task<Sale> CreateSaleAsync(Sale sale, IEnumerable<Detail> details)
    {
        // Validar que el usuario existe
        if (sale.IdUser.HasValue && !await _userRepository.ExistsAsync(sale.IdUser.Value))
        {
            throw new ArgumentException("El usuario especificado no existe.");
        }

        // Validar stock de productos
        foreach (var detail in details)
        {
            if (!detail.IdProduct.HasValue)
            {
                throw new ArgumentException("El detalle de venta no tiene un producto válido.");
            }

            var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
            if (product == null)
            {
                throw new ArgumentException($"El producto con ID {detail.IdProduct.Value} no existe.");
            }

            if (product.Stock < ((int?)detail.Quantity ?? 0))
            {
                throw new ArgumentException($"Stock insuficiente para el producto {product.Name}. Stock disponible: {product.Stock}, requerido: {((int?)detail.Quantity ?? 0)}");
            }
        }

        // Crear la venta
        var createdSale = await _saleRepository.CreateAsync(sale);

        // Crear los detalles y reducir stock
        foreach (var detail in details)
        {
            detail.IdSale = createdSale.Id;
            await _detailRepository.CreateAsync(detail);
            await _productRepository.ReduceStockAsync(detail.IdProduct.Value, (int?)detail.Quantity ?? 0);
        }

        return createdSale;
    }

    public async Task<bool> CancelSaleAsync(Guid saleId, string reason)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        // Restaurar stock de productos
        var details = await _detailRepository.GetBySaleAsync(saleId);
        foreach (var detail in details)
        {
            if (detail.IdProduct.HasValue)
            {
                await _productRepository.IncreaseStockAsync(detail.IdProduct.Value, (int?)detail.Amount ?? 0);
            }
        }

        return await _saleRepository.CancelSaleAsync(saleId, reason);
    }

    public Task<decimal> CalculateSaleTotalAsync(IEnumerable<Detail> details)
    {
        decimal total = 0;
        foreach (var detail in details)
        {
            total += (decimal)detail.Total;
        }
        return Task.FromResult(total);
    }

    public async Task<bool> ProcessPaymentAsync(Guid saleId, decimal amount, string paymentMethod)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        if (amount < (decimal)sale.Total)
        {
            throw new ArgumentException("El monto del pago es insuficiente.");
        }

        sale.State = true;
        // PaymentMethod is computed property, no need to set it directly
        await _saleRepository.UpdateAsync(sale);

        return true;
    }

    public async Task<IEnumerable<Sale>> GetPendingSalesAsync()
    {
        return await _saleRepository.GetPendingSalesAsync();
    }

    public async Task<IEnumerable<Sale>> GetCompletedSalesAsync()
    {
        return await _saleRepository.GetCompletedSalesAsync();
    }

    public async Task<decimal> GetTotalSalesAmountAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        return await _saleRepository.GetTotalSalesAmountAsync(startDate, endDate);
    }

    public async Task<int> GetTotalSalesCountAsync(DateTime? startDate = null, DateTime? endDate = null)
    {
        return await _saleRepository.GetTotalSalesCountAsync(startDate, endDate);
    }

    public async Task<IEnumerable<Sale>> GetTopSalesAsync(int count = 10)
    {
        return await _saleRepository.GetTopSalesAsync(count);
    }

    public async Task<IEnumerable<Sale>> SearchSalesAsync(string searchTerm)
    {
        return await _saleRepository.SearchAsync(searchTerm);
    }

    public async Task<Sale> UpdateSaleAsync(Sale sale)
    {
        if (!await _saleRepository.ExistsAsync(sale.Id))
        {
            throw new ArgumentException("La venta especificada no existe.");
        }

        return await _saleRepository.UpdateAsync(sale);
    }

    public async Task<bool> CompleteSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        try
        {
            // Generar DTE automáticamente
            int tipoDocumento = await DetermineDteTypeForSaleAsync(saleId);
            await _dteSaleService.GenerateDteForSaleAsync(saleId, tipoDocumento);

            sale.State = true;
            await _saleRepository.UpdateAsync(sale);
            return true;
        }
        catch
        {
            // Si falla la generación de DTE, no completar la venta
            return false;
        }
    }

    private async Task<int> DetermineDteTypeForSaleAsync(Guid saleId)
    {
        var details = await _detailRepository.GetBySaleAsync(saleId);
        bool hasNonExempt = false;

        foreach (var detail in details)
        {
            if (detail.IdProduct.HasValue)
            {
                var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
                if (product != null && !product.Exenta)
                {
                    hasNonExempt = true;
                    break;
                }
            }
        }

        return hasNonExempt ? 39 : 41;
    }

    public async Task<bool> CompleteSaleWithDteAsync(Guid saleId, int tipoDocumento)
    {
        // Primero completar la venta normalmente
        var saleCompleted = await CompleteSaleAsync(saleId);
        if (!saleCompleted) return false;

        try
        {
            // Generar DTE para la venta completada
            await _dteSaleService.GenerateDteForSaleAsync(saleId, tipoDocumento);
            return true;
        }
        catch (Exception)
        {
            // Si falla la generación de DTE, la venta ya está completada
            // Podríamos marcar la venta como completada pero sin DTE
            // Por ahora, devolver false para indicar que algo falló
            return false;
        }
    }

    public async Task<bool> UpdateSaleDteInfoAsync(Guid saleId, int dteFolio, string dteType, Guid? cafId, string dteXml)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        sale.DteFolio = dteFolio;
        sale.DteType = dteType;
        sale.CafId = cafId;
        sale.DteXml = dteXml;
        sale.DteStatus = "Generado";
        sale.DteSentDate = DateTime.Now;

        await _saleRepository.UpdateAsync(sale);
        return true;
    }

    public async Task<bool> UpdateSalePaymentInfoAsync(Guid saleId, string paymentMethod, string? transactionId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        sale.PaymentMethod = paymentMethod;
        sale.PaymentTransactionId = transactionId;
        sale.PaymentDate = DateTime.Now;

        await _saleRepository.UpdateAsync(sale);
        return true;
    }

    public async Task<bool> ValidateSaleAsync(Sale sale, IEnumerable<Detail> details)
    {
        // Validar que el usuario existe
        if (sale.IdUser.HasValue && !await _userRepository.ExistsAsync(sale.IdUser.Value))
            return false;

        // Validar stock de productos
        foreach (var detail in details)
        {
            if (!detail.IdProduct.HasValue)
                return false;

            var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
            if (product == null || product.Stock < ((int?)detail.Amount ?? 0))
                return false;
        }

        return true;
    }

    public async Task<decimal> CalculateTaxAmountAsync(IEnumerable<Detail> details)
    {
        decimal taxAmount = 0;
        foreach (var detail in details)
        {
            if (!detail.IdProduct.HasValue)
                continue;

            var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
            if (product != null)
            {
                taxAmount += (decimal)(detail.Amount * detail.Price * ((double?)product.Tax?.Percentage ?? 0.0) / 100);
            }
        }
        return taxAmount;
    }

    public async Task<bool> ProcessPaymentAsync(Guid saleId, decimal cashAmount, decimal otherAmount)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null) return false;

        var totalRequired = sale.Total;
        var totalPaid = cashAmount + otherAmount;

        if ((decimal)totalPaid >= (decimal)totalRequired)
        {
            sale.State = true;
            await _saleRepository.UpdateAsync(sale);
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteSaleAsync(Guid id)
    {
        // Restaurar stock antes de eliminar
        var details = await _detailRepository.GetBySaleAsync(id);
        foreach (var detail in details)
        {
            if (detail.IdProduct.HasValue)
            {
                await _productRepository.IncreaseStockAsync(detail.IdProduct.Value, (int?)detail.Amount ?? 0);
            }
        }

        // Eliminar detalles
        await _detailRepository.DeleteBySaleAsync(id);

        // Eliminar venta
        return await _saleRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Detail>> GetSaleDetailsAsync(Guid saleId)
    {
        return await _detailRepository.GetBySaleAsync(saleId);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _saleRepository.ExistsAsync(id);
    }
}