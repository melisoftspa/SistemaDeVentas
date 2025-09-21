using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Interfaz para servicios de generación de PDFs de documentos DTE.
/// Implementa las especificaciones del manual de muestras impresas del SII.
/// </summary>
public interface IPdfGeneratorService
{
    /// <summary>
    /// Genera un PDF para un documento DTE según las especificaciones del SII.
    /// Incluye timbre electrónico PDF417, layout específico, y manejo de caracteres especiales.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a convertir en PDF.</param>
    /// <param name="isCedible">Indica si el documento es cedible (incluye acuse de recibo).</param>
    /// <returns>Un arreglo de bytes con el contenido del PDF generado.</returns>
    /// <exception cref="ArgumentNullException">Si dteDocument es null.</exception>
    /// <exception cref="InvalidOperationException">Si falla la generación del PDF.</exception>
    byte[] GeneratePdf(DteDocument dteDocument, bool isCedible = false);

    /// <summary>
    /// Genera un PDF para una boleta electrónica (TipoDTE 39 o 41) con template específico.
    /// </summary>
    /// <param name="dteDocument">El documento DTE de tipo boleta.</param>
    /// <param name="isCedible">Indica si la boleta es cedible.</param>
    /// <returns>Un arreglo de bytes con el contenido del PDF generado.</returns>
    /// <exception cref="ArgumentException">Si el TipoDTE no es 39 o 41.</exception>
    byte[] GenerateBoletaPdf(DteDocument dteDocument, bool isCedible = false);
}