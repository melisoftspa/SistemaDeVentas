using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Builder específico para Facturas Afectas (TipoDTE 33).
/// </summary>
public class FacturaAfectaBuilder : DteBuilderService
{
    /// <summary>
    /// Inicializa una nueva instancia de FacturaAfectaBuilder.
    /// </summary>
    /// <param name="logger">Logger para registrar eventos.</param>
    public FacturaAfectaBuilder(ILogger<DteBuilderService> logger)
        : base(logger)
    {
    }
    /// <summary>
    /// Construye un documento XML DTE para Factura Afecta.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a convertir.</param>
    /// <returns>El documento XML generado.</returns>
    public override XDocument BuildXml(DteDocument dteDocument)
    {
        // Validar que sea una factura afecta
        if (dteDocument.IdDoc.TipoDTE != TipoDte.FacturaAfecta)
        {
            throw new ArgumentException("El documento debe ser de tipo Factura Afecta (33).", nameof(dteDocument));
        }

        // Usar la implementación base
        return base.BuildXml(dteDocument);
    }

    /// <summary>
    /// Valida el XML generado contra esquemas específicos para Factura Afecta.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a validar.</param>
    /// <returns>True si es válido, false en caso contrario.</returns>
    public override bool ValidateXml(XDocument xmlDocument)
    {
        // Validación base
        if (!base.ValidateXml(xmlDocument))
        {
            return false;
        }

        // Validaciones específicas para Factura Afecta
        // - Verificar que tenga IVA
        // - Verificar que no tenga MontoExento si es afecta
        // Implementar según reglas del SII

        var totales = xmlDocument.Root?.Element("Totales");
        if (totales == null)
        {
            return false;
        }

        // Para factura afecta, debe tener IVA
        var ivaElement = totales.Element("IVA");
        if (ivaElement == null || string.IsNullOrEmpty(ivaElement.Value))
        {
            return false;
        }

        return true;
    }
}