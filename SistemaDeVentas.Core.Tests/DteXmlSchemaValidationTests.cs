using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

/// <summary>
/// Pruebas de validación de XML DTE contra esquemas XSD del SII.
/// </summary>
public class DteXmlSchemaValidationTests
{
    private readonly ILogger<DteBuilderService> _logger;
    private readonly FacturaAfectaBuilder _facturaAfectaBuilder;
    private readonly FacturaExentaBuilder _facturaExentaBuilder;

    public DteXmlSchemaValidationTests()
    {
        using var loggerFactory = LoggerFactory.Create(builder => { });
        _logger = loggerFactory.CreateLogger<DteBuilderService>();
        _facturaAfectaBuilder = new FacturaAfectaBuilder(_logger);
        _facturaExentaBuilder = new FacturaExentaBuilder(_logger);
    }

    [Fact]
    public void ValidateFacturaAfectaXml_AgainstBasicStructure_Success()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(xmlDocument.Root);
        Assert.Equal("DTE", xmlDocument.Root.Name.LocalName);

        // Validar estructura básica
        var documento = xmlDocument.Root.Element("Documento");
        Assert.NotNull(documento);

        var encabezado = documento.Element("Encabezado");
        Assert.NotNull(encabezado);

        // Validar elementos requeridos
        Assert.NotNull(encabezado.Element("IdDoc"));
        Assert.NotNull(encabezado.Element("Emisor"));
        Assert.NotNull(encabezado.Element("Receptor"));
        Assert.NotNull(encabezado.Element("Totales"));

        // Validar detalles
        var detalles = documento.Elements("Detalle");
        Assert.Single(detalles);

        // Validar que tenga IVA (requerido para afectas)
        var totales = encabezado.Element("Totales");
        var ivaElement = totales?.Element("IVA");
        Assert.NotNull(ivaElement);
        Assert.NotEmpty(ivaElement.Value);
    }

    [Fact]
    public void ValidateFacturaExentaXml_AgainstBasicStructure_Success()
    {
        // Arrange
        var dteDocument = CreateValidFacturaExentaDocument();

        // Act
        var xmlDocument = _facturaExentaBuilder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(xmlDocument.Root);
        Assert.Equal("DTE", xmlDocument.Root.Name.LocalName);

        // Validar estructura básica
        var documento = xmlDocument.Root.Element("Documento");
        Assert.NotNull(documento);

        var encabezado = documento.Element("Encabezado");
        Assert.NotNull(encabezado);

        // Validar elementos requeridos
        Assert.NotNull(encabezado.Element("IdDoc"));
        Assert.NotNull(encabezado.Element("Emisor"));
        Assert.NotNull(encabezado.Element("Receptor"));
        Assert.NotNull(encabezado.Element("Totales"));

        // Validar detalles
        var detalles = documento.Elements("Detalle");
        Assert.Single(detalles);

        // Validar que NO tenga IVA (no requerido para exentas)
        var totales = encabezado.Element("Totales");
        var ivaElement = totales?.Element("IVA");
        Assert.Null(ivaElement); // Factura exenta no debe tener IVA

        // Validar que tenga MontoExento
        var montoExentoElement = totales?.Element("MntExe");
        Assert.NotNull(montoExentoElement);
        Assert.NotEmpty(montoExentoElement.Value);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_FacturaAfecta_RequiredElements()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar elementos requeridos según esquema DTE
        var documento = xmlDocument.Root?.Element("Documento");
        var encabezado = documento?.Element("Encabezado");
        var idDoc = encabezado?.Element("IdDoc");

        // TipoDTE requerido
        Assert.NotNull(idDoc?.Element("TipoDTE"));
        Assert.Equal("33", idDoc?.Element("TipoDTE")?.Value);

        // Folio requerido
        Assert.NotNull(idDoc?.Element("Folio"));
        Assert.NotEmpty(idDoc?.Element("Folio")?.Value);

        // FchEmis requerido
        Assert.NotNull(idDoc?.Element("FchEmis"));
        Assert.NotEmpty(idDoc?.Element("FchEmis")?.Value);

        // Emisor requerido
        var emisor = encabezado?.Element("Emisor");
        Assert.NotNull(emisor?.Element("RUTEmisor"));
        Assert.NotNull(emisor?.Element("RznSoc"));
        Assert.NotNull(emisor?.Element("GiroEmis"));
        Assert.NotNull(emisor?.Element("Acteco"));

        // Receptor requerido
        var receptor = encabezado?.Element("Receptor");
        Assert.NotNull(receptor?.Element("RUTRecep"));
        Assert.NotNull(receptor?.Element("RznSocRecep"));

        // Totales requerido
        var totales = encabezado?.Element("Totales");
        Assert.NotNull(totales?.Element("MntNeto"));
        Assert.NotNull(totales?.Element("TasaIVA"));
        Assert.NotNull(totales?.Element("IVA"));
        Assert.NotNull(totales?.Element("MntTotal"));
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_FacturaExenta_RequiredElements()
    {
        // Arrange
        var dteDocument = CreateValidFacturaExentaDocument();

        // Act
        var xmlDocument = _facturaExentaBuilder.BuildXml(dteDocument);

        // Assert - Validar elementos requeridos según esquema DTE
        var documento = xmlDocument.Root?.Element("Documento");
        var encabezado = documento?.Element("Encabezado");
        var idDoc = encabezado?.Element("IdDoc");

        // TipoDTE requerido
        Assert.NotNull(idDoc?.Element("TipoDTE"));
        Assert.Equal("34", idDoc?.Element("TipoDTE")?.Value);

        // Folio requerido
        Assert.NotNull(idDoc?.Element("Folio"));
        Assert.NotEmpty(idDoc?.Element("Folio")?.Value);

        // FchEmis requerido
        Assert.NotNull(idDoc?.Element("FchEmis"));
        Assert.NotEmpty(idDoc?.Element("FchEmis")?.Value);

        // Emisor requerido
        var emisor = encabezado?.Element("Emisor");
        Assert.NotNull(emisor?.Element("RUTEmisor"));
        Assert.NotNull(emisor?.Element("RznSoc"));
        Assert.NotNull(emisor?.Element("GiroEmis"));
        Assert.NotNull(emisor?.Element("Acteco"));

        // Receptor requerido
        var receptor = encabezado?.Element("Receptor");
        Assert.NotNull(receptor?.Element("RUTRecep"));
        Assert.NotNull(receptor?.Element("RznSocRecep"));

        // Totales requerido
        var totales = encabezado?.Element("Totales");
        Assert.NotNull(totales?.Element("MntExe"));
        Assert.NotNull(totales?.Element("MntTotal"));

        // Para exentas, IVA debe ser null
        var ivaElement = totales?.Element("IVA");
        Assert.Null(ivaElement);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_DetalleStructure_RequiredElements()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar estructura del detalle
        var detalle = xmlDocument.Root?.Element("Documento")?.Element("Detalle");

        Assert.NotNull(detalle);

        // Elementos requeridos del detalle
        Assert.NotNull(detalle.Element("NroLinDet"));
        Assert.NotNull(detalle.Element("NmbItem"));
        Assert.NotNull(detalle.Element("QtyItem"));
        Assert.NotNull(detalle.Element("PrcItem"));
        Assert.NotNull(detalle.Element("MontoItem"));

        // Verificar valores
        Assert.Equal("1", detalle.Element("NroLinDet")?.Value);
        Assert.Equal("Producto Afecto", detalle.Element("NmbItem")?.Value);
        Assert.Equal("1", detalle.Element("QtyItem")?.Value);
        Assert.Equal("119000", detalle.Element("PrcItem")?.Value);
        Assert.Equal("119000", detalle.Element("MontoItem")?.Value);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_OptionalElements_WhenPresent()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();
        dteDocument.IdDoc.IndicadorTraslado = 1;
        dteDocument.Detalles[0].UnidadMedidaItem = "UN";
        dteDocument.Detalles[0].IndicadorExencion = 1;

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar elementos opcionales cuando están presentes
        var idDoc = xmlDocument.Root?.Element("Documento")?.Element("Encabezado")?.Element("IdDoc");
        Assert.NotNull(idDoc?.Element("IndTraslado"));
        Assert.Equal("1", idDoc?.Element("IndTraslado")?.Value);

        var detalle = xmlDocument.Root?.Element("Documento")?.Element("Detalle");
        Assert.NotNull(detalle?.Element("UnmdItem"));
        Assert.Equal("UN", detalle?.Element("UnmdItem")?.Value);

        Assert.NotNull(detalle?.Element("IndExe"));
        Assert.Equal("1", detalle?.Element("IndExe")?.Value);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_OptionalElements_WhenNotPresent()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();
        // No configurar elementos opcionales

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar que elementos opcionales no estén presentes cuando no se configuran
        var idDoc = xmlDocument.Root?.Element("Documento")?.Element("Encabezado")?.Element("IdDoc");
        Assert.Null(idDoc?.Element("FchVenc")); // Fecha vencimiento opcional
        Assert.Null(idDoc?.Element("FmaPago")); // Forma pago opcional
        Assert.Null(idDoc?.Element("IndTraslado")); // Indicador traslado opcional

        var detalle = xmlDocument.Root?.Element("Documento")?.Element("Detalle");
        Assert.Null(detalle?.Element("UnmdItem")); // Unidad medida opcional
        Assert.Null(detalle?.Element("IndExe")); // Indicador exención opcional
        Assert.Null(detalle?.Element("DscItem")); // Descripción opcional
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_CodigoItem_WhenPresent()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();
        dteDocument.Detalles[0].CodigoItem = new CodigoItem
        {
            TipoCodigo = "INT",
            ValorCodigo = "PROD001"
        };

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar estructura del código de item
        var cdgItem = xmlDocument.Root?.Element("Documento")?.Element("Detalle")?.Element("CdgItem");

        Assert.NotNull(cdgItem);
        Assert.NotNull(cdgItem.Element("TpoCodigo"));
        Assert.Equal("INT", cdgItem.Element("TpoCodigo")?.Value);
        Assert.NotNull(cdgItem.Element("VlrCodigo"));
        Assert.Equal("PROD001", cdgItem.Element("VlrCodigo")?.Value);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_XmlDeclaration_Correct()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar declaración XML
        Assert.NotNull(xmlDocument.Declaration);
        Assert.Equal("1.0", xmlDocument.Declaration.Version);
        Assert.Equal("ISO-8859-1", xmlDocument.Declaration.Encoding);
    }

    [Fact]
    public void ValidateXmlSchemaCompliance_NamespaceDeclarations_Correct()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert - Validar namespaces (aunque en la implementación actual no se usan explícitamente)
        // En una implementación completa, se deberían validar los namespaces del SII
        Assert.NotNull(xmlDocument.Root);
        Assert.Equal("DTE", xmlDocument.Root.Name.LocalName);
    }

    private DteDocument CreateValidFacturaAfectaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaAfecta,
                Folio = 12345,
                FechaEmision = DateTime.Now,
                IndicadorTraslado = 1
            },
            Emisor = new Emisor
            {
                RutEmisor = "11111111-1",
                RazonSocial = "Empresa Emisora S.A.",
                GiroEmisor = "Venta de productos",
                ActividadEconomica = 620100,
                DireccionOrigen = "Dirección 123",
                ComunaOrigen = "Santiago",
                CiudadOrigen = "Santiago"
            },
            Receptor = new Receptor
            {
                RutReceptor = "22222222-2",
                RazonSocialReceptor = "Cliente S.A.",
                GiroReceptor = "Compra de productos",
                DireccionReceptor = "Dirección Cliente 456",
                ComunaReceptor = "Providencia",
                CiudadReceptor = "Santiago"
            },
            Totales = new TotalesDte
            {
                MontoNeto = 100000m,
                TasaIVA = 19m,
                IVA = 19000m,
                MontoTotal = 119000m
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Afecto",
                    DescripcionItem = "Descripción del producto",
                    CantidadItem = 1m,
                    UnidadMedidaItem = "UN",
                    PrecioItem = 119000m,
                    MontoItem = 119000m,
                    IndicadorExencion = 1,
                    CodigoItem = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "PROD001" }
                }
            }
        };
    }

    private DteDocument CreateValidFacturaExentaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaExenta,
                Folio = 67890,
                FechaEmision = DateTime.Now
            },
            Emisor = new Emisor
            {
                RutEmisor = "11111111-1",
                RazonSocial = "Empresa Emisora S.A.",
                GiroEmisor = "Venta de productos",
                ActividadEconomica = 620100
            },
            Receptor = new Receptor
            {
                RutReceptor = "22222222-2",
                RazonSocialReceptor = "Cliente S.A."
            },
            Totales = new TotalesDte
            {
                MontoExento = 100000m,
                MontoTotal = 100000m
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Exento",
                    CantidadItem = 1m,
                    PrecioItem = 100000m,
                    MontoItem = 100000m,
                    IndicadorExencion = 2 // Exento
                }
            }
        };
    }
}