using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services;

/// <summary>
/// Servicio para integración de DTE en ventas.
/// </summary>
public class DteSaleService : IDteSaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDetailRepository _detailRepository;
    private readonly IProductRepository _productRepository;
    private readonly IDteProcessingService _dteProcessingService;
    private readonly ICafRepository _cafRepository;
    private readonly IDteFileStorageService _dteFileStorageService;
    private readonly IPdfGeneratorService _pdfGeneratorService;

    public DteSaleService(
        ISaleRepository saleRepository,
        IDetailRepository detailRepository,
        IProductRepository productRepository,
        IDteProcessingService dteProcessingService,
        ICafRepository cafRepository,
        IDteFileStorageService dteFileStorageService,
        IPdfGeneratorService pdfGeneratorService)
    {
        _saleRepository = saleRepository;
        _detailRepository = detailRepository;
        _productRepository = productRepository;
        _dteProcessingService = dteProcessingService;
        _cafRepository = cafRepository;
        _dteFileStorageService = dteFileStorageService;
        _pdfGeneratorService = pdfGeneratorService;
    }

    /// <inheritdoc/>
    public async Task<XDocument> GenerateDteForSaleAsync(Guid saleId, int tipoDocumento)
    {
        // Obtener venta
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
        {
            throw new ArgumentException($"Venta con ID {saleId} no encontrada.");
        }

        // Validar que la venta esté completada
        if (!sale.State)
        {
            throw new InvalidOperationException("No se puede generar DTE para una venta no completada.");
        }

        // Obtener detalles de la venta
        var details = await _detailRepository.GetBySaleAsync(saleId);
        if (!details.Any())
        {
            throw new InvalidOperationException("La venta no tiene detalles.");
        }

        // Convertir a DteDocument
        var dteDocument = await ConvertSaleToDteDocumentAsync(sale, details, tipoDocumento);

        // Generar DTE completo (construir, firmar y timbrar)
        var dteXml = await _dteProcessingService.BuildSignAndStampDteWithSettings(dteDocument, tipoDocumento);

        // Guardar XML físicamente
        await _dteFileStorageService.SaveDteXmlAsync(saleId, dteDocument.IdDoc.Folio, dteXml);

        // Generar y guardar PDF
        var pdfBytes = _pdfGeneratorService.GenerateBoletaPdf(dteDocument, false);
        await _dteFileStorageService.SaveDtePdfAsync(saleId, dteDocument.IdDoc.Folio, pdfBytes);

        // Actualizar estado de la venta
        await UpdateSaleDteStatusAsync(saleId, true, dteDocument.IdDoc.Folio, dteXml.ToString());

        return dteXml;
    }

    /// <inheritdoc/>
    public async Task<XDocument?> GetDteForSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale != null && !string.IsNullOrEmpty(sale.DteXml))
        {
            return XDocument.Parse(sale.DteXml);
        }
        return null;
    }

    /// <inheritdoc/>
    public async Task<bool> CanGenerateDteForSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        return sale != null && sale.State == true;
    }

    /// <inheritdoc/>
    public async Task<int?> GetFolioForSaleAsync(Guid saleId)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        return sale?.Folio;
    }

    /// <inheritdoc/>
    public async Task UpdateSaleDteStatusAsync(Guid saleId, bool dteGenerated, int? folio = null)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
        {
            throw new ArgumentException($"Venta con ID {saleId} no encontrada.");
        }

        sale.DteGenerated = dteGenerated;
        sale.Folio = folio;
        await _saleRepository.UpdateAsync(sale);
    }

    /// <summary>
    /// Actualiza el estado de DTE de una venta incluyendo el XML.
    /// </summary>
    private async Task UpdateSaleDteStatusAsync(Guid saleId, bool dteGenerated, int? folio, string dteXml)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId);
        if (sale == null)
        {
            throw new ArgumentException($"Venta con ID {saleId} no encontrada.");
        }

        sale.DteGenerated = dteGenerated;
        sale.Folio = folio;
        sale.DteXml = dteXml;
        await _saleRepository.UpdateAsync(sale);
    }

    /// <summary>
    /// Convierte una venta y sus detalles a un documento DTE.
    /// </summary>
    private async Task<DteDocument> ConvertSaleToDteDocumentAsync(Sale sale, IEnumerable<Detail> details, int tipoDocumento)
    {
        // Crear IdDoc
        var idDoc = new IdDoc
        {
            TipoDTE = (TipoDte)tipoDocumento,
            FechaEmision = DateTime.Now,
            // Folio se asignará automáticamente por el CAF
        };

        // Crear Emisor (datos hardcodeados por ahora, deberían venir de configuración)
        var emisor = new Emisor
        {
            RutEmisor = "12345678-9", // TODO: Obtener de configuración
            RazonSocial = "Empresa Ejemplo", // TODO: Obtener de configuración
            GiroEmisor = "Venta de productos", // TODO: Obtener de configuración
            ActividadEconomica = 123456 // TODO: Obtener de configuración
        };

        // Crear Receptor (cliente final por defecto)
        var receptor = new Receptor
        {
            RutReceptor = "66666666-6", // Cliente genérico
            RazonSocialReceptor = "Cliente Final"
        };

        // Crear detalles DTE
        var detallesDte = new List<DetalleDte>();
        int numeroLinea = 1;

        foreach (var detail in details)
        {
            var product = await _productRepository.GetByIdAsync(detail.IdProduct.Value);

            var detalleDte = new DetalleDte
            {
                NumeroLineaDetalle = numeroLinea++,
                NombreItem = product?.Name ?? "Producto",
                CantidadItem = (decimal?)detail.Amount,
                PrecioItem = (decimal?)detail.Price,
                MontoItem = (decimal?)detail.Total,
                IndicadorExencion = product?.Exenta == true ? 1 : null // 1 = Exento de IVA
            };

            detallesDte.Add(detalleDte);
        }

        // Calcular totales
        var montoNeto = detallesDte.Where(d => d.IndicadorExencion != 1).Sum(d => d.MontoItem ?? 0);
        var montoExento = detallesDte.Where(d => d.IndicadorExencion == 1).Sum(d => d.MontoItem ?? 0);
        var montoTotal = detallesDte.Sum(d => d.MontoItem ?? 0);

        var totales = new TotalesDte
        {
            MontoNeto = montoNeto,
            MontoExento = montoExento,
            TasaIVA = 19, // IVA Chile
            IVA = montoNeto * 0.19m,
            MontoTotal = montoTotal
        };

        // Crear documento DTE
        var dteDocument = new DteDocument
        {
            IdDoc = idDoc,
            Emisor = emisor,
            Receptor = receptor,
            Detalles = detallesDte,
            Totales = totales
        };

        return dteDocument;
    }
}