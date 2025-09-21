using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

/// <summary>
/// Pruebas de integración para el flujo completo de construcción DTE.
/// </summary>
public class DteCompleteWorkflowIntegrationTests
{
    private readonly ILogger<DteBuilderService> _builderLogger;
    private readonly FacturaAfectaBuilder _facturaAfectaBuilder;
    private readonly FacturaExentaBuilder _facturaExentaBuilder;

    public DteCompleteWorkflowIntegrationTests()
    {
        using var loggerFactory = LoggerFactory.Create(builder => { });
        _builderLogger = loggerFactory.CreateLogger<DteBuilderService>();
        _facturaAfectaBuilder = new FacturaAfectaBuilder(_builderLogger);
        _facturaExentaBuilder = new FacturaExentaBuilder(_builderLogger);
    }

    [Fact]
    public void BuildFacturaAfectaXml_EndToEnd_Success()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(xmlDocument.Root);
        Assert.Equal("DTE", xmlDocument.Root.Name.LocalName);

        // Validar estructura completa
        var documento = xmlDocument.Root.Element("Documento");
        Assert.NotNull(documento);

        var encabezado = documento.Element("Encabezado");
        Assert.NotNull(encabezado);

        // Validar IdDoc
        var idDoc = encabezado.Element("IdDoc");
        Assert.NotNull(idDoc);
        Assert.Equal("33", idDoc.Element("TipoDTE")?.Value);
        Assert.Equal("12345", idDoc.Element("Folio")?.Value);

        // Validar Emisor
        var emisor = encabezado.Element("Emisor");
        Assert.NotNull(emisor);
        Assert.Equal("11111111-1", emisor.Element("RUTEmisor")?.Value);
        Assert.Equal("Empresa Emisora S.A.", emisor.Element("RznSoc")?.Value);

        // Validar Receptor
        var receptor = encabezado.Element("Receptor");
        Assert.NotNull(receptor);
        Assert.Equal("22222222-2", receptor.Element("RUTRecep")?.Value);

        // Validar Totales
        var totales = encabezado.Element("Totales");
        Assert.NotNull(totales);
        Assert.Equal("100000", totales.Element("MntNeto")?.Value);
        Assert.Equal("19", totales.Element("TasaIVA")?.Value);
        Assert.Equal("19000", totales.Element("IVA")?.Value);
        Assert.Equal("119000", totales.Element("MntTotal")?.Value);

        // Validar Detalles
        var detalles = documento.Elements("Detalle");
        Assert.Single(detalles);
        var detalle = detalles.First();
        Assert.Equal("1", detalle.Element("NroLinDet")?.Value);
        Assert.Equal("Producto Afecto", detalle.Element("NmbItem")?.Value);
        Assert.Equal("119000", detalle.Element("MontoItem")?.Value);
    }

    [Fact]
    public void BuildFacturaExentaXml_EndToEnd_Success()
    {
        // Arrange
        var dteDocument = CreateValidFacturaExentaDocument();

        // Act
        var xmlDocument = _facturaExentaBuilder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(xmlDocument.Root);
        Assert.Equal("DTE", xmlDocument.Root.Name.LocalName);

        // Validar estructura completa
        var documento = xmlDocument.Root.Element("Documento");
        Assert.NotNull(documento);

        var encabezado = documento.Element("Encabezado");
        Assert.NotNull(encabezado);

        // Validar IdDoc
        var idDoc = encabezado.Element("IdDoc");
        Assert.NotNull(idDoc);
        Assert.Equal("34", idDoc.Element("TipoDTE")?.Value);

        // Validar Totales - sin IVA
        var totales = encabezado.Element("Totales");
        Assert.NotNull(totales);
        Assert.Equal("100000", totales.Element("MntExe")?.Value);
        Assert.Equal("100000", totales.Element("MntTotal")?.Value);

        // Verificar que NO tenga IVA
        var ivaElement = totales.Element("IVA");
        Assert.Null(ivaElement);
    }

    [Fact]
    public void BuildXml_WithOptionalElements_Included()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();
        dteDocument.IdDoc.IndicadorTraslado = 1;
        dteDocument.Detalles[0].UnidadMedidaItem = "UN";
        dteDocument.Detalles[0].IndicadorExencion = 1;

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);

        // Assert
        var documento = xmlDocument.Root?.Element("Documento");
        var idDoc = documento?.Element("Encabezado")?.Element("IdDoc");
        var detalle = documento?.Element("Detalle");

        // Verificar elementos opcionales incluidos
        Assert.NotNull(idDoc?.Element("IndTraslado"));
        Assert.Equal("1", idDoc?.Element("IndTraslado")?.Value);

        Assert.NotNull(detalle?.Element("UnmdItem"));
        Assert.Equal("UN", detalle?.Element("UnmdItem")?.Value);

        Assert.NotNull(detalle?.Element("IndExe"));
        Assert.Equal("1", detalle?.Element("IndExe")?.Value);
    }

    [Fact]
    public void BuildXml_ValidationPasses_ForValidDocuments()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var xmlDocument = _facturaAfectaBuilder.BuildXml(dteDocument);
        var isValid = _facturaAfectaBuilder.ValidateXml(xmlDocument);

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void BuildXml_ValidationFails_ForInvalidStructure()
    {
        // Arrange - Crear XML inválido manualmente
        var invalidXml = new XDocument(
            new XElement("DTE",
                new XElement("InvalidStructure")
            )
        );

        // Act
        var isValid = _facturaAfectaBuilder.ValidateXml(invalidXml);

        // Assert
        Assert.False(isValid);
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