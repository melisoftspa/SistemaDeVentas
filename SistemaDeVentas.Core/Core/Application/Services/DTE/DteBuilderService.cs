using System.Xml.Linq;
using System.Xml.Schema;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;
using SistemaDeVentas.Core.Domain.Validators.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Servicio base para construcción de XML DTE.
/// </summary>
public abstract class DteBuilderService : IDteBuilderService
{
    protected readonly ILogger<DteBuilderService> _logger;

    /// <summary>
    /// Inicializa una nueva instancia de DteBuilderService.
    /// </summary>
    /// <param name="logger">Logger para registrar eventos.</param>
    protected DteBuilderService(ILogger<DteBuilderService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    /// <summary>
    /// Construye un documento XML DTE a partir de un objeto DteDocument.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a convertir.</param>
    /// <returns>El documento XML generado.</returns>
    /// <exception cref="DteValidationException">Se lanza cuando la validación del DTE falla.</exception>
    public virtual XDocument BuildXml(DteDocument dteDocument)
    {
        _logger.LogInformation("Iniciando construcción de XML DTE para folio {Folio}", dteDocument.IdDoc.Folio);

        // Validar el documento DTE
        ValidateDteDocument(dteDocument);

        _logger.LogInformation("Validación del DTE exitosa, procediendo con la construcción del XML");

        var encabezado = new XElement("Encabezado",
            BuildIdDoc(dteDocument.IdDoc),
            BuildEmisor(dteDocument.Emisor),
            BuildReceptor(dteDocument.Receptor),
            BuildTotales(dteDocument.Totales)
        );

        var documento = new XElement("Documento",
            encabezado,
            dteDocument.Detalles.Select(BuildDetalle)
        );

        var xmlDocument = new XDocument(
            new XDeclaration("1.0", "ISO-8859-1", null),
            documento
        );

        _logger.LogInformation("XML DTE construido exitosamente para folio {Folio}", dteDocument.IdDoc.Folio);

        return xmlDocument;
    }

    /// <summary>
    /// Valida un documento DTE usando FluentValidation.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a validar.</param>
    /// <exception cref="DteValidationException">Se lanza cuando la validación falla.</exception>
    protected virtual void ValidateDteDocument(DteDocument dteDocument)
    {
        if (dteDocument == null)
        {
            throw new DteValidationException("El documento DTE no puede ser nulo.");
        }

        var validator = new DteDocumentValidator();
        var validationResult = validator.Validate(dteDocument);

        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            _logger.LogWarning("Validación del DTE fallida para folio {Folio}: {Errors}",
                dteDocument.IdDoc?.Folio ?? 0, errors);

            throw new DteValidationException($"Errores de validación en el DTE: {errors}");
        }

        _logger.LogDebug("Validación del DTE exitosa para folio {Folio}", dteDocument.IdDoc?.Folio ?? 0);
    }

    /// <summary>
    /// Valida el XML generado contra los esquemas del SII.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a validar.</param>
    /// <returns>True si es válido, false en caso contrario.</returns>
    public virtual bool ValidateXml(XDocument xmlDocument)
    {
        // Validación básica
        if (xmlDocument == null || xmlDocument.Root == null)
        {
            return false;
        }

        // Intentar validación contra esquemas XSD del SII si están disponibles
        try
        {
            var schemas = GetXmlSchemas();
            if (schemas != null)
            {
                bool isValid = true;
                xmlDocument.Validate(schemas, (sender, e) =>
                {
                    isValid = false;
                });
                return isValid;
            }
        }
        catch
        {
            // Si falla la validación con esquemas, continuar con validación básica
        }

        // Validación básica de estructura
        return ValidateBasicStructure(xmlDocument);
    }

    /// <summary>
    /// Obtiene los esquemas XML del SII para validación.
    /// En una implementación real, cargar desde archivos XSD del SII.
    /// </summary>
    /// <returns>Conjunto de esquemas XML.</returns>
    protected virtual XmlSchemaSet? GetXmlSchemas()
    {
        try
        {
            var schemas = new XmlSchemaSet();

            // Cargar esquema DTE_v10.xsd
            var dteSchemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "docs", "xml", "dte", "DTE_v10.xsd");
            if (File.Exists(dteSchemaPath))
            {
                schemas.Add("http://www.sii.cl/SiiDte", dteSchemaPath);
            }

            // Cargar esquema SiiTypes_v10.xsd
            var siiTypesSchemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "docs", "xml", "dte", "SiiTypes_v10.xsd");
            if (File.Exists(siiTypesSchemaPath))
            {
                schemas.Add("http://www.sii.cl/SiiDte", siiTypesSchemaPath);
            }

            // Cargar esquema xmldsignature_v10.xsd
            var signatureSchemaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "docs", "xml", "dte", "xmldsignature_v10.xsd");
            if (File.Exists(signatureSchemaPath))
            {
                schemas.Add("http://www.w3.org/2000/09/xmldsig#", signatureSchemaPath);
            }

            return schemas.Count > 0 ? schemas : null;
        }
        catch
        {
            // Si falla la carga, continuar sin validación de esquemas
            return null;
        }
    }

    /// <summary>
    /// Valida la estructura básica del XML DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a validar.</param>
    /// <returns>True si la estructura es válida.</returns>
    protected virtual bool ValidateBasicStructure(XDocument xmlDocument)
    {
        var root = xmlDocument.Root;
        if (root == null || root.Name != "Documento")
        {
            return false;
        }

        // Verificar elementos requeridos en Encabezado
        var encabezado = root.Element("Encabezado");
        return encabezado != null &&
               encabezado.Element("IdDoc") != null &&
               encabezado.Element("Emisor") != null &&
               encabezado.Element("Receptor") != null &&
               encabezado.Element("Totales") != null;
    }

    /// <summary>
    /// Construye el elemento IdDoc.
    /// </summary>
    protected virtual XElement BuildIdDoc(IdDoc idDoc)
    {
        return new XElement("IdDoc",
            new XElement("TipoDTE", (int)idDoc.TipoDTE),
            new XElement("Folio", idDoc.Folio),
            new XElement("FchEmis", idDoc.FechaEmision.ToString("yyyy-MM-dd")),
            idDoc.FechaVencimiento.HasValue ? new XElement("FchVenc", idDoc.FechaVencimiento.Value.ToString("yyyy-MM-dd")) : null,
            idDoc.FormaPago.HasValue ? new XElement("FmaPago", idDoc.FormaPago.Value) : null,
            idDoc.IndicadorTraslado.HasValue ? new XElement("IndTraslado", idDoc.IndicadorTraslado.Value) : null
        );
    }

    /// <summary>
    /// Construye el elemento Emisor.
    /// </summary>
    protected virtual XElement BuildEmisor(Emisor emisor)
    {
        return new XElement("Emisor",
            new XElement("RUTEmisor", emisor.RutEmisor),
            new XElement("RznSoc", emisor.RazonSocial),
            new XElement("GiroEmis", emisor.GiroEmisor),
            new XElement("Acteco", emisor.ActividadEconomica),
            emisor.CodigoSucursalSII.HasValue ? new XElement("CdgSIISucur", emisor.CodigoSucursalSII.Value) : null,
            !string.IsNullOrEmpty(emisor.DireccionOrigen) ? new XElement("DirOrigen", emisor.DireccionOrigen) : null,
            !string.IsNullOrEmpty(emisor.ComunaOrigen) ? new XElement("CmnaOrigen", emisor.ComunaOrigen) : null,
            !string.IsNullOrEmpty(emisor.CiudadOrigen) ? new XElement("CiudadOrigen", emisor.CiudadOrigen) : null,
            !string.IsNullOrEmpty(emisor.CorreoEmisor) ? new XElement("CorreoEmisor", emisor.CorreoEmisor) : null,
            !string.IsNullOrEmpty(emisor.Telefono) ? new XElement("Telefono", emisor.Telefono) : null
        );
    }

    /// <summary>
    /// Construye el elemento Receptor.
    /// </summary>
    protected virtual XElement BuildReceptor(Receptor receptor)
    {
        return new XElement("Receptor",
            new XElement("RUTRecep", receptor.RutReceptor),
            new XElement("RznSocRecep", receptor.RazonSocialReceptor),
            !string.IsNullOrEmpty(receptor.GiroReceptor) ? new XElement("GiroRecep", receptor.GiroReceptor) : null,
            !string.IsNullOrEmpty(receptor.DireccionReceptor) ? new XElement("DirRecep", receptor.DireccionReceptor) : null,
            !string.IsNullOrEmpty(receptor.ComunaReceptor) ? new XElement("CmnaRecep", receptor.ComunaReceptor) : null,
            !string.IsNullOrEmpty(receptor.CiudadReceptor) ? new XElement("CiudadRecep", receptor.CiudadReceptor) : null,
            !string.IsNullOrEmpty(receptor.CorreoReceptor) ? new XElement("CorreoRecep", receptor.CorreoReceptor) : null,
            !string.IsNullOrEmpty(receptor.Contacto) ? new XElement("Contacto", receptor.Contacto) : null,
            !string.IsNullOrEmpty(receptor.CodigoInternoReceptor) ? new XElement("CdgIntRecep", receptor.CodigoInternoReceptor) : null
        );
    }

    /// <summary>
    /// Construye el elemento Totales.
    /// </summary>
    protected virtual XElement BuildTotales(TotalesDte totales)
    {
        return new XElement("Totales",
            totales.MontoNeto.HasValue ? new XElement("MntNeto", totales.MontoNeto.Value) : null,
            totales.MontoExento.HasValue ? new XElement("MntExe", totales.MontoExento.Value) : null,
            totales.TasaIVA.HasValue ? new XElement("TasaIVA", totales.TasaIVA.Value) : null,
            totales.IVA.HasValue ? new XElement("IVA", totales.IVA.Value) : null,
            new XElement("MntTotal", totales.MontoTotal)
        );
    }

    /// <summary>
    /// Construye el elemento Detalle.
    /// </summary>
    protected virtual XElement BuildDetalle(DetalleDte detalle)
    {
        return new XElement("Detalle",
            new XElement("NroLinDet", detalle.NumeroLineaDetalle),
            detalle.CodigoItem != null ? BuildCodigoItem(detalle.CodigoItem) : null,
            new XElement("NmbItem", detalle.NombreItem),
            !string.IsNullOrEmpty(detalle.DescripcionItem) ? new XElement("DscItem", detalle.DescripcionItem) : null,
            detalle.CantidadItem.HasValue ? new XElement("QtyItem", detalle.CantidadItem.Value) : null,
            !string.IsNullOrEmpty(detalle.UnidadMedidaItem) ? new XElement("UnmdItem", detalle.UnidadMedidaItem) : null,
            detalle.PrecioItem.HasValue ? new XElement("PrcItem", detalle.PrecioItem.Value) : null,
            detalle.MontoItem.HasValue ? new XElement("MontoItem", detalle.MontoItem.Value) : null,
            detalle.IndicadorExencion.HasValue ? new XElement("IndExe", detalle.IndicadorExencion.Value) : null
        );
    }

    /// <summary>
    /// Construye el elemento CdgItem.
    /// </summary>
    protected virtual XElement BuildCodigoItem(CodigoItem codigoItem)
    {
        return new XElement("CdgItem",
            new XElement("TpoCodigo", codigoItem.TipoCodigo),
            new XElement("VlrCodigo", codigoItem.ValorCodigo)
        );
    }
}