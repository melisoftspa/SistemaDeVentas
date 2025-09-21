using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

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
    public void BuildXml_ValidDocument_ReturnsXml()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Documento", result.Root?.Name.LocalName);
    }

    [Fact]
    public void BuildXml_NullDocument_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<NullReferenceException>(() => _builder.BuildXml(null));
    }

    [Fact]
    public void ValidateXml_ValidXml_ReturnsTrue()
    {
        // Arrange
        var xml = XDocument.Parse("<Documento><IdDoc></IdDoc><Emisor></Emisor><Receptor></Receptor><Totales></Totales></Documento>");

        // Act
        var result = _builder.ValidateXml(xml);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateXml_NullXml_ReturnsFalse()
    {
        // Act
        var result = _builder.ValidateXml(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_InvalidStructure_ReturnsFalse()
    {
        // Arrange
        var xml = XDocument.Parse("<Invalid></Invalid>");

        // Act
        var result = _builder.ValidateXml(xml);

        // Assert
        Assert.False(result);
    }

    private DteDocument CreateValidDteDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc { TipoDTE = TipoDte.FacturaAfecta, Folio = 1, FechaEmision = DateTime.Now },
            Emisor = new Emisor { RutEmisor = "11111111-1", RazonSocial = "Emisor", GiroEmisor = "Giro", ActividadEconomica = 620100 },
            Receptor = new Receptor { RutReceptor = "22222222-2", RazonSocialReceptor = "Receptor" },
            Totales = new TotalesDte { MontoNeto = 100m, IVA = 19m, MontoTotal = 119m },
            Detalles = new List<DetalleDte> { new DetalleDte { NumeroLineaDetalle = 1, NombreItem = "Item", MontoItem = 119m } }
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