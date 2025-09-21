using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Servicio para validación de DTE contra estándares del SII.
/// </summary>
public class SiiValidationService : ISiiValidationService
{
    private readonly XmlSchemaSet _schemaSet;

    public SiiValidationService()
    {
        _schemaSet = new XmlSchemaSet();
        // TODO: Cargar esquemas XSD del SII (DTE_v10.xsd, etc.)
        // Por ahora, validación básica de estructura
    }

    /// <summary>
    /// Valida un DTE contra los esquemas XML del SII.
    /// </summary>
    public async Task<SiiValidationResult> ValidateDteAsync(string dteXml)
    {
        try
        {
            var doc = XDocument.Parse(dteXml);
            return await ValidateDteAsync(doc);
        }
        catch (XmlException ex)
        {
            return new SiiValidationResult
            {
                IsValid = false,
                Errors = new[] { $"XML mal formado: {ex.Message}" }
            };
        }
    }

    /// <summary>
    /// Valida un DTE contra los esquemas XML del SII.
    /// </summary>
    public async Task<SiiValidationResult> ValidateDteAsync(XDocument dteDocument)
    {
        var errors = new List<string>();
        var warnings = new List<string>();

        // Validación básica de estructura
        var root = dteDocument.Root;
        if (root == null || root.Name.LocalName != "DTE")
        {
            errors.Add("El documento debe tener un elemento raíz 'DTE'");
            return new SiiValidationResult { IsValid = false, Errors = errors.ToArray() };
        }

        // Validar elementos requeridos
        ValidateRequiredElements(root, errors, warnings);

        // Validar tipos de documento
        ValidateDocumentType(root, errors, warnings);

        // Validar montos
        ValidateAmounts(root, errors, warnings);

        return new SiiValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors.ToArray(),
            Warnings = warnings.ToArray()
        };
    }

    /// <summary>
    /// Valida reglas de negocio específicas del SII.
    /// </summary>
    public async Task<SiiValidationResult> ValidateBusinessRulesAsync(string dteXml)
    {
        var doc = XDocument.Parse(dteXml);
        return await ValidateDteAsync(doc); // Por ahora, misma validación
    }

    private void ValidateRequiredElements(XElement root, List<string> errors, List<string> warnings)
    {
        var documento = root.Element("Documento");
        if (documento == null)
        {
            errors.Add("Elemento 'Documento' requerido");
            return;
        }

        // Validar IdDoc
        var idDoc = documento.Element("Encabezado")?.Element("IdDoc");
        if (idDoc == null)
        {
            errors.Add("Elemento 'IdDoc' requerido en Encabezado");
        }

        // Validar Emisor
        var emisor = documento.Element("Encabezado")?.Element("Emisor");
        if (emisor == null)
        {
            errors.Add("Elemento 'Emisor' requerido en Encabezado");
        }

        // Validar Receptor
        var receptor = documento.Element("Encabezado")?.Element("Receptor");
        if (receptor == null)
        {
            errors.Add("Elemento 'Receptor' requerido en Encabezado");
        }

        // Validar Detalles
        var detalles = documento.Elements("Detalle");
        if (!detalles.Any())
        {
            errors.Add("Al menos un elemento 'Detalle' requerido");
        }
    }

    private void ValidateDocumentType(XElement root, List<string> errors, List<string> warnings)
    {
        var tipoDte = root.Element("Documento")?
                         .Element("Encabezado")?
                         .Element("IdDoc")?
                         .Element("TipoDTE")?.Value;

        if (string.IsNullOrEmpty(tipoDte))
        {
            errors.Add("TipoDTE requerido");
            return;
        }

        // Validar que sea un tipo válido (33=Factura, 61=Nota de Crédito, etc.)
        var validTypes = new[] { "33", "34", "61", "56", "52" }; // Ejemplos
        if (!validTypes.Contains(tipoDte))
        {
            warnings.Add($"TipoDTE '{tipoDte}' puede no ser válido");
        }
    }

    private void ValidateAmounts(XElement root, List<string> errors, List<string> warnings)
    {
        var documento = root.Element("Documento");
        if (documento == null) return;

        // Validar que los montos sean consistentes
        var totales = documento.Element("Encabezado")?.Element("Totales");
        if (totales != null)
        {
            var mntNeto = ParseDecimal(totales.Element("MntNeto")?.Value);
            var iva = ParseDecimal(totales.Element("IVA")?.Value);
            var mntTotal = ParseDecimal(totales.Element("MntTotal")?.Value);

            if (mntNeto.HasValue && iva.HasValue && mntTotal.HasValue)
            {
                var calculatedTotal = mntNeto.Value + iva.Value;
                if (Math.Abs(calculatedTotal - mntTotal.Value) > 0.01m)
                {
                    errors.Add("Inconsistencia en los montos totales");
                }
            }
        }
    }

    private decimal? ParseDecimal(string value)
    {
        return decimal.TryParse(value, out var result) ? result : null;
    }
}