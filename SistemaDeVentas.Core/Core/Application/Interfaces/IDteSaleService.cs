using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using System.Xml.Linq;

namespace SistemaDeVentas.Core.Application.Interfaces;

/// <summary>
/// Interfaz para servicios de integración DTE en ventas.
/// </summary>
public interface IDteSaleService
{
    /// <summary>
    /// Genera un DTE para una venta completada.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <param name="tipoDocumento">Tipo de documento DTE (33=Factura, 34=Factura exenta, etc.).</param>
    /// <returns>Documento XML DTE generado, firmado y timbrado.</returns>
    Task<XDocument> GenerateDteForSaleAsync(Guid saleId, int tipoDocumento);

    /// <summary>
    /// Obtiene el DTE asociado a una venta.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <returns>Documento XML DTE o null si no existe.</returns>
    Task<XDocument?> GetDteForSaleAsync(Guid saleId);

    /// <summary>
    /// Valida si una venta puede generar un DTE.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <returns>True si puede generar DTE.</returns>
    Task<bool> CanGenerateDteForSaleAsync(Guid saleId);

    /// <summary>
    /// Obtiene el folio asignado a una venta.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <returns>Folio asignado o null.</returns>
    Task<int?> GetFolioForSaleAsync(Guid saleId);

    /// <summary>
    /// Actualiza el estado de DTE de una venta.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <param name="dteGenerated">True si DTE fue generado.</param>
    /// <param name="folio">Folio asignado.</param>
    Task UpdateSaleDteStatusAsync(Guid saleId, bool dteGenerated, int? folio = null);

    /// <summary>
    /// Obtiene el CAF usado para una venta.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <returns>ID del CAF usado.</returns>
    Task<Guid?> GetCafIdForSaleAsync(Guid saleId);

    /// <summary>
    /// Obtiene el XML DTE generado para una venta.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <returns>XML DTE como string.</returns>
    Task<string?> GetDteXmlForSaleAsync(Guid saleId);
}