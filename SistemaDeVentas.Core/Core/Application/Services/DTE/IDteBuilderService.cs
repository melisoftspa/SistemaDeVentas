using System.Xml.Linq;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Interfaz para servicios de construcción de XML DTE.
/// </summary>
public interface IDteBuilderService
{
    /// <summary>
    /// Construye un documento XML DTE a partir de un objeto DteDocument.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a convertir.</param>
    /// <returns>El documento XML generado.</returns>
    XDocument BuildXml(DteDocument dteDocument);

    /// <summary>
    /// Valida el XML generado contra los esquemas del SII.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a validar.</param>
    /// <returns>True si es válido, false en caso contrario.</returns>
    bool ValidateXml(XDocument xmlDocument);
}