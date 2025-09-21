using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

public class DetailService : IDetailService
{
    private readonly IDetailRepository _detailRepository;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;

    public DetailService(
        IDetailRepository detailRepository,
        IProductRepository productRepository,
        ISaleRepository saleRepository)
    {
        _detailRepository = detailRepository;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<Detail>> GetAllDetailsAsync()
    {
        return await _detailRepository.GetAllAsync();
    }

    public async Task<Detail?> GetDetailByIdAsync(Guid id)
    {
        return await _detailRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Detail>> GetDetailsBySaleAsync(Guid saleId)
    {
        return await _detailRepository.GetBySaleAsync(saleId);
    }

    public async Task<IEnumerable<Detail>> GetDetailsByProductAsync(Guid productId)
    {
        return await _detailRepository.GetByProductAsync(productId);
    }

    public async Task<Detail> CreateDetailAsync(Detail detail)
    {
        // Validar que la venta existe
        if (detail.IdSale == null || !await _saleRepository.ExistsAsync(detail.IdSale.Value))
        {
            throw new ArgumentException("La venta especificada no existe.");
        }

        // Validar que el producto existe
        if (detail.IdProduct == null || !await _productRepository.ExistsAsync(detail.IdProduct.Value))
        {
            throw new ArgumentException("El producto especificado no existe.");
        }

        // Obtener el producto para calcular el total
        var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
        if (product == null)
        {
            throw new ArgumentException("No se pudo obtener la información del producto.");
        }

        // Calcular el total del detalle
        detail.Total = detail.Amount * (double)product.SalePrice;
        detail.Date = DateTime.UtcNow;

        return await _detailRepository.CreateAsync(detail);
    }

    public async Task<Detail> UpdateDetailAsync(Detail detail)
    {
        if (!await _detailRepository.ExistsAsync(detail.Id))
        {
            throw new ArgumentException("El detalle especificado no existe.");
        }

        // Recalcular el total si cambió la cantidad
        var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);
        if (product != null)
        {
            detail.Total = detail.Amount * (double)product.SalePrice;
        }

        return await _detailRepository.UpdateAsync(detail);
    }

    public async Task<bool> DeleteDetailAsync(Guid id)
    {
        return await _detailRepository.DeleteAsync(id);
    }

    public async Task<bool> DeleteDetailsBySaleAsync(Guid saleId)
    {
        return await _detailRepository.DeleteBySaleAsync(saleId);
    }

    public async Task<decimal> CalculateSaleTotalAsync(Guid saleId)
    {
        return await _detailRepository.GetSaleTotalAsync(saleId);
    }

    public async Task<int> GetTotalQuantityBySaleAsync(Guid saleId)
    {
        return (int)await _detailRepository.GetTotalQuantityBySaleAsync(saleId);
    }

    public async Task<IEnumerable<Product>> GetMostSoldProductsAsync(int count = 10)
    {
        return await _detailRepository.GetTopSellingProductsAsync(count);
    }

    public async Task<IEnumerable<Detail>> GetDetailsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var allDetails = await _detailRepository.GetAllAsync();
        return allDetails.Where(d => d.Date >= startDate && d.Date <= endDate);
    }

    public async Task<decimal> GetTotalSalesAmountByProductAsync(Guid productId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var details = await _detailRepository.GetByProductAsync(productId);
        
        if (startDate.HasValue || endDate.HasValue)
        {
           details = details.Where(d =>
            (!startDate.HasValue || d.Date >= startDate.Value) &&
            (!endDate.HasValue || d.Date <= endDate.Value));
        }

        return (decimal)details.Sum(d => d.Total);
    }

    public async Task<int> GetTotalQuantitySoldByProductAsync(Guid productId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var details = await _detailRepository.GetByProductAsync(productId);
        
        if (startDate.HasValue || endDate.HasValue)
        {
            details = details.Where(d => 
                (!startDate.HasValue || d.Date >= startDate.Value) &&
                (!endDate.HasValue || d.Date <= endDate.Value));
        }

        return details.Sum(d => (int)d.Amount);
    }

    public async Task<IEnumerable<Detail>> GetTopSellingDetailsAsync(int count = 10)
    {
        var allDetails = await _detailRepository.GetAllAsync();
        return allDetails
            .OrderByDescending(d => d.Total)
            .Take(count);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _detailRepository.ExistsAsync(id);
    }

    public async Task<int> GetTotalDetailsCountAsync()
    {
        var details = await _detailRepository.GetAllAsync();
        return details.Count();
    }

    public async Task<decimal> GetAverageDetailValueAsync()
    {
        var details = await _detailRepository.GetAllAsync();
        if (!details.Any())
        {
            return 0;
        }

        return (decimal)details.Average(d => d.Total);
    }

    public async Task<IEnumerable<Detail>> GetDetailsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var allDetails = await _detailRepository.GetAllAsync();
        return allDetails.Where(d => (decimal)d.Total >= minPrice && (decimal)d.Total <= maxPrice);
    }

    public async Task<IEnumerable<Detail>> GetDetailsByQuantityRangeAsync(int minQuantity, int maxQuantity)
    {
        var allDetails = await _detailRepository.GetAllAsync();
        return allDetails.Where(d => d.Amount >= minQuantity && d.Amount <= maxQuantity);
    }

    public async Task<bool> ValidateDetailAsync(Detail detail)
    {
        // Validar que la venta existe
        if (detail.IdSale == null || !await _saleRepository.ExistsAsync(detail.IdSale.Value))
        {
            return false;
        }

        // Validar que el producto existe
        if (detail.IdProduct == null || !await _productRepository.ExistsAsync(detail.IdProduct.Value))
        {
            return false;
        }

        // Validar que la cantidad es positiva
        if (detail.Amount <= 0)
        {
            return false;
        }

        return true;
    }
}