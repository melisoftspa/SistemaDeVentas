using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Builder específico para Facturas Exentas (TipoDTE 34).
/// </summary>
public class FacturaExentaBuilder : DteBuilderService
{
    /// <summary>
    /// Inicializa una nueva instancia de FacturaExentaBuilder.
    /// </summary>
    /// <param name="logger">Logger para registrar eventos.</param>
    public FacturaExentaBuilder(ILogger<DteBuilderService> logger)
        : base(logger)
    {
    }
    /// <summary>
    /// Construye un documento XML DTE para Factura Exenta.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a convertir.</param>
    /// <returns>El documento XML generado.</returns>
    public override XDocument BuildXml(DteDocument dteDocument)
    {
        // Validar que sea una factura exenta
        if (dteDocument.IdDoc.TipoDTE != TipoDte.FacturaExenta)
        {
            throw new ArgumentException("El documento debe ser de tipo Factura Exenta (34).", nameof(dteDocument));
        }

        // Usar la implementación base
        return base.BuildXml(dteDocument);
    }

    /// <summary>
    /// Valida el XML generado contra esquemas específicos para Factura Exenta.
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

        // Validaciones específicas para Factura Exenta
        // - Verificar que no tenga IVA
        // - Verificar que tenga MontoExento

        var totales = xmlDocument.Root?.Element("Totales");
        if (totales == null)
        {
            return false;
        }

        // Para factura exenta, no debe tener IVA
        var ivaElement = totales.Element("IVA");
        if (ivaElement != null && !string.IsNullOrEmpty(ivaElement.Value))
        {
            return false;
        }

        // Debe tener MontoExento
        var montoExentoElement = totales.Element("MntExe");
        if (montoExentoElement == null || string.IsNullOrEmpty(montoExentoElement.Value))
        {
            return false;
        }

        return true;
    }
}