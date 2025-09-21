using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class DteXmlGenerationIntegrationTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly TestDteBuilderService _builder;

    public DteXmlGenerationIntegrationTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _builder = new TestDteBuilderService(_loggerMock.Object);
    }

    [Fact]
    public void GenerateFacturaAfectaXml_ValidDocument_ShouldBeValidAgainstXsd()
    {
        // Arrange
        var dteDocument = CreateFacturaAfectaDocument();

        // Act
        var xmlDocument = _builder.BuildXml(dteDocument);
        var isValid = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.True(isValid);
        Assert.Equal("Documento", xmlDocument.Root?.Name.LocalName);
        var tipoDte = xmlDocument.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value;
        Assert.NotNull(tipoDte);
        Assert.Equal("33", tipoDte);
    }

    [Fact]
    public void GenerateFacturaExentaXml_ValidDocument_ShouldBeValidAgainstXsd()
    {
        // Arrange
        var dteDocument = CreateFacturaExentaDocument();

        // Act
        var xmlDocument = _builder.BuildXml(dteDocument);
        var isValid = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.True(isValid);
        Assert.Equal("Documento", xmlDocument.Root?.Name.LocalName);
        var tipoDte = xmlDocument.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value;
        Assert.NotNull(tipoDte);
        Assert.Equal("34", tipoDte);
    }

    [Fact]
    public void GenerateBoletaAfectaXml_ValidDocument_ShouldBeValidAgainstXsd()
    {
        // Arrange
        var dteDocument = CreateBoletaAfectaDocument();

        // Act
        var xmlDocument = _builder.BuildXml(dteDocument);
        var isValid = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.True(isValid);
        Assert.Equal("Documento", xmlDocument.Root?.Name.LocalName);
        var tipoDte = xmlDocument.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value;
        Assert.NotNull(tipoDte);
        Assert.Equal("39", tipoDte);
    }

    [Fact]
    public void GenerateBoletaExentaXml_ValidDocument_ShouldBeValidAgainstXsd()
    {
        // Arrange
        var dteDocument = CreateBoletaExentaDocument();

        // Act
        var xmlDocument = _builder.BuildXml(dteDocument);
        var isValid = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.True(isValid);
        Assert.Equal("Documento", xmlDocument.Root?.Name.LocalName);
        var tipoDte = xmlDocument.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value;
        Assert.NotNull(tipoDte);
        Assert.Equal("41", tipoDte);
    }


    [Fact]
    public void GenerateXml_InvalidDocument_ShouldThrowException()
    {
        // Arrange
        var invalidDocument = new DteDocument(); // Documento vacío

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => _builder.BuildXml(invalidDocument));
    }

    private DteDocument CreateFacturaAfectaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaAfecta,
                Folio = 12345,
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
                    CantidadItem = 1m,
                    PrecioItem = 119000m,
                    MontoItem = 119000m
                }
            }
        };
    }

    private DteDocument CreateFacturaExentaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaExenta,
                Folio = 12346,
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
                    IndicadorExencion = 1
                }
            }
        };
    }

    private DteDocument CreateBoletaAfectaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.BoletaAfecta,
                Folio = 12347,
                FechaEmision = DateTime.Now,
                IndicadorServicio = 3 // Boleta de Ventas y Servicios
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
                RutReceptor = "66666666-6", // RUT genérico para consumidor final
                RazonSocialReceptor = "Consumidor Final"
            },
            Totales = new TotalesDte
            {
                MontoNeto = 50000m,
                TasaIVA = 19m,
                IVA = 9500m,
                MontoTotal = 59500m
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Boleta",
                    CantidadItem = 1m,
                    PrecioItem = 59500m,
                    MontoItem = 59500m
                }
            }
        };
    }

    private DteDocument CreateBoletaExentaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.BoletaExenta,
                Folio = 12348,
                FechaEmision = DateTime.Now,
                IndicadorServicio = 3
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
                RutReceptor = "66666666-6",
                RazonSocialReceptor = "Consumidor Final"
            },
            Totales = new TotalesDte
            {
                MontoExento = 50000m,
                MontoTotal = 50000m
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Exento Boleta",
                    CantidadItem = 1m,
                    PrecioItem = 50000m,
                    MontoItem = 50000m,
                    IndicadorExencion = 1
                }
            }
        };
    }

    // Clase de prueba para acceder al constructor protegido
    private class TestDteBuilderService : DteBuilderService
    {
        public TestDteBuilderService(ILogger<DteBuilderService> logger) : base(logger) { }

        public override XDocument BuildXml(DteDocument dteDocument)
        {
            return base.BuildXml(dteDocument);
        }

        public override bool ValidateXml(XDocument xmlDocument)
        {
            return base.ValidateXml(xmlDocument);
        }
    }
}