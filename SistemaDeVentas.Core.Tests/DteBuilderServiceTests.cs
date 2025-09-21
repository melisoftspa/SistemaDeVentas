using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

/// <summary>
/// Clase de prueba concreta para DteBuilderService.
/// </summary>
public class TestDteBuilderService : DteBuilderService
{
    public TestDteBuilderService(ILogger<DteBuilderService> logger) : base(logger) { }
}

public class DteBuilderServiceTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly TestDteBuilderService _builder;

    public DteBuilderServiceTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _builder = new TestDteBuilderService(_loggerMock.Object);
    }

    [Fact]
    public void Constructor_Throws_ArgumentNullException_When_Logger_Is_Null()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => new TestDteBuilderService(null));
        Assert.Equal("logger", exception.ParamName);
    }

    [Fact]
    public void BuildXml_Throws_DteValidationException_When_DteDocument_Is_Null()
    {
        // Act & Assert
        var exception = Assert.Throws<DteValidationException>(() => _builder.BuildXml(null));
        Assert.Contains("no puede ser nulo", exception.Message);
    }

    [Fact]
    public void BuildXml_Throws_DteValidationException_When_DteDocument_Is_Invalid()
    {
        // Arrange
        var invalidDteDocument = new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Empresa", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Cliente" },
            Totales = new TotalesDte { MontoTotal = 1000m },
            Detalles = new List<DetalleDte>() // Empty list - invalid
        };

        // Act & Assert
        var exception = Assert.Throws<DteValidationException>(() => _builder.BuildXml(invalidDteDocument));
        Assert.Contains("Errores de validación", exception.Message);
    }

    [Fact]
    public void BuildXml_Returns_Valid_XmlDocument_When_DteDocument_Is_Valid()
    {
        // Arrange
        var validDteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(validDteDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1.0", result.Declaration?.Version);
        Assert.Equal("ISO-8859-1", result.Declaration?.Encoding);
        Assert.Equal("Documento", result.Root?.Name.LocalName);

        var encabezado = result.Root?.Element("Encabezado");
        Assert.NotNull(encabezado);

        Assert.NotNull(encabezado.Element("IdDoc"));
        Assert.NotNull(encabezado.Element("Emisor"));
        Assert.NotNull(encabezado.Element("Receptor"));
        Assert.NotNull(encabezado.Element("Totales"));

        var detalles = result.Root?.Elements("Detalle");
        Assert.Single(detalles);
    }

    [Fact]
    public void BuildXml_Includes_Correct_IdDoc_Elements()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var idDoc = result.Root?.Element("Encabezado")?.Element("IdDoc");
        Assert.NotNull(idDoc);
        Assert.Equal("33", idDoc.Element("TipoDTE")?.Value);
        Assert.Equal("12345", idDoc.Element("Folio")?.Value);
        Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd"), idDoc.Element("FchEmis")?.Value);
    }

    [Fact]
    public void BuildXml_Includes_Correct_Emisor_Elements()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var emisor = result.Root?.Element("Encabezado")?.Element("Emisor");
        Assert.NotNull(emisor);
        Assert.Equal("11111111-1", emisor.Element("RUTEmisor")?.Value);
        Assert.Equal("Empresa Emisora S.A.", emisor.Element("RznSoc")?.Value);
        Assert.Equal("Venta de productos", emisor.Element("GiroEmis")?.Value);
        Assert.Equal("620100", emisor.Element("Acteco")?.Value);
    }

    [Fact]
    public void BuildXml_Includes_Correct_Receptor_Elements()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var receptor = result.Root?.Element("Encabezado")?.Element("Receptor");
        Assert.NotNull(receptor);
        Assert.Equal("22222222-2", receptor.Element("RUTRecep")?.Value);
        Assert.Equal("Cliente S.A.", receptor.Element("RznSocRecep")?.Value);
    }

    [Fact]
    public void BuildXml_Includes_Correct_Totales_Elements()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var totales = result.Root?.Element("Encabezado")?.Element("Totales");
        Assert.NotNull(totales);
        Assert.Equal("100000", totales.Element("MntNeto")?.Value);
        Assert.Equal("19", totales.Element("TasaIVA")?.Value);
        Assert.Equal("19000", totales.Element("IVA")?.Value);
        Assert.Equal("119000", totales.Element("MntTotal")?.Value);
    }

    [Fact]
    public void BuildXml_Includes_Correct_Detalle_Elements()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var detalle = result.Root?.Element("Detalle");
        Assert.NotNull(detalle);
        Assert.Equal("1", detalle.Element("NroLinDet")?.Value);
        Assert.Equal("Producto Afecto", detalle.Element("NmbItem")?.Value);
        Assert.Equal("1", detalle.Element("QtyItem")?.Value);
        Assert.Equal("119000", detalle.Element("PrcItem")?.Value);
        Assert.Equal("119000", detalle.Element("MontoItem")?.Value);
    }

    [Fact]
    public void BuildXml_Includes_CodigoItem_When_Present()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();
        dteDocument.Detalles[0].CodigoItem = new CodigoItem { TipoCodigo = "INT", ValorCodigo = "PROD001" };

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var cdgItem = result.Root?.Element("Detalle")?.Element("CdgItem");
        Assert.NotNull(cdgItem);
        Assert.Equal("INT", cdgItem.Element("TpoCodigo")?.Value);
        Assert.Equal("PROD001", cdgItem.Element("VlrCodigo")?.Value);
    }

    [Fact]
    public void BuildXml_Excludes_Optional_Elements_When_Null()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();
        dteDocument.IdDoc.FechaVencimiento = null;
        dteDocument.IdDoc.FormaPago = null;
        dteDocument.IdDoc.IndicadorTraslado = null;

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        var idDoc = result.Root?.Element("Encabezado")?.Element("IdDoc");
        Assert.NotNull(idDoc);
        Assert.Null(idDoc.Element("FchVenc"));
        Assert.Null(idDoc.Element("FmaPago"));
        Assert.Null(idDoc.Element("IndTraslado"));
    }

    [Fact]
    public void ValidateXml_Returns_False_When_XmlDocument_Is_Null()
    {
        // Act
        var result = _builder.ValidateXml(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_Returns_False_When_XmlDocument_Has_No_Root()
    {
        // Arrange
        var xmlDocument = new XDocument();

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_Returns_False_When_Root_Is_Not_Documento()
    {
        // Arrange
        var xmlDocument = new XDocument(new XElement("InvalidRoot"));

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_Returns_False_When_Missing_Required_Elements()
    {
        // Arrange
        var xmlDocument = new XDocument(
            new XElement("Documento",
                new XElement("Encabezado",
                    new XElement("IdDoc"),
                    // Missing Emisor, Receptor, Totales
                    new XElement("Totales")
                )
            )
        );

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_Returns_True_When_Valid_Structure()
    {
        // Arrange
        var xmlDocument = new XDocument(
            new XElement("Documento",
                new XElement("Encabezado",
                    new XElement("IdDoc"),
                    new XElement("Emisor"),
                    new XElement("Receptor"),
                    new XElement("Totales")
                )
            )
        );

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.True(result);
    }

    private DteDocument CreateValidDteDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaAfecta,
                Folio = 12345,
                FechaEmision = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddDays(30),
                FormaPago = 1,
                IndicadorTraslado = 1
            },
            Emisor = new Emisor
            {
                RutEmisor = "11111111-1",
                RazonSocial = "Empresa Emisora S.A.",
                GiroEmisor = "Venta de productos",
                ActividadEconomica = 620100,
                CodigoSucursalSII = 1,
                DireccionOrigen = "Dirección 123",
                ComunaOrigen = "Santiago",
                CiudadOrigen = "Santiago",
                CorreoEmisor = "emisor@example.com",
                Telefono = "+56912345678"
            },
            Receptor = new Receptor
            {
                RutReceptor = "22222222-2",
                RazonSocialReceptor = "Cliente S.A.",
                GiroReceptor = "Compra de productos",
                DireccionReceptor = "Dirección Cliente 456",
                ComunaReceptor = "Providencia",
                CiudadReceptor = "Santiago",
                CorreoReceptor = "cliente@example.com",
                Contacto = "Juan Pérez",
                CodigoInternoReceptor = "CLI001"
            },
            Totales = new TotalesDte
            {
                MontoNeto = 100000m,
                MontoExento = 0m,
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
                    IndicadorExencion = 1
                }
            }
        };
    }
}