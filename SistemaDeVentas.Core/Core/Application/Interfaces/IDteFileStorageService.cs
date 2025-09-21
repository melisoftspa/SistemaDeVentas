using System.Xml.Linq;

namespace SistemaDeVentas.Core.Application.Interfaces;

/// <summary>
/// Interfaz para servicios de almacenamiento de archivos DTE.
/// </summary>
public interface IDteFileStorageService
{
    /// <summary>
    /// Guarda el XML del DTE en el sistema de archivos.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <param name="folio">Folio del DTE.</param>
    /// <param name="dteXml">Documento XML DTE.</param>
    /// <returns>Ruta del archivo guardado.</returns>
    Task<string> SaveDteXmlAsync(Guid saleId, int folio, XDocument dteXml);

    /// <summary>
    /// Guarda el PDF del DTE en el sistema de archivos.
    /// </summary>
    /// <param name="saleId">ID de la venta.</param>
    /// <param name="folio">Folio del DTE.</param>
    /// <param name="pdfBytes">Contenido del PDF en bytes.</param>
    /// <returns>Ruta del archivo guardado.</returns>
    Task<string> SaveDtePdfAsync(Guid saleId, int folio, byte[] pdfBytes);

    /// <summary>
    /// Obtiene la ruta del directorio de almacenamiento.
    /// </summary>
    string GetStorageDirectory();
}