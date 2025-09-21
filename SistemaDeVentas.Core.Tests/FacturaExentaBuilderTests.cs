using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class FacturaExentaBuilderTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly FacturaExentaBuilder _builder;

    public FacturaExentaBuilderTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _builder = new FacturaExentaBuilder(_loggerMock.Object);
    }

    [Fact]
    public void BuildXml_ValidFacturaExentaDocument_ReturnsValidXml()
    {
        // Arrange
        var dteDocument = CreateValidFacturaExentaDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Documento", result.Root?.Name.LocalName);
        Assert.Equal("34", result.Root?.Element("IdDoc")?.Element("TipoDTE")?.Value);
    }

    [Fact]
    public void BuildXml_InvalidTipoDte_ThrowsArgumentException()
    {
        // Arrange
        var dteDocument = CreateValidFacturaExentaDocument();
        dteDocument.IdDoc.TipoDTE = TipoDte.FacturaAfecta; // Tipo incorrecto

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _builder.BuildXml(dteDocument));
        Assert.Contains("Factura Exenta (34)", exception.Message);
    }

    [Fact]
    public void ValidateXml_ValidFacturaExentaXml_ReturnsTrue()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaExentaXml();

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateXml_XmlWithIva_ReturnsFalse()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaExentaXml();
        var totales = xmlDocument.Root?.Element("Totales");
        totales?.Add(new XElement("IVA", "1000")); // Agregar IVA, que no debe estar en exenta

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_XmlWithoutMontoExento_ReturnsFalse()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaExentaXml();
        var totales = xmlDocument.Root?.Element("Totales");
        totales?.Element("MntExe")?.Remove(); // Remover MontoExento

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_NullXml_ReturnsFalse()
    {
        // Act
        var result = _builder.ValidateXml(null);

        // Assert
        Assert.False(result);
    }

    private DteDocument CreateValidFacturaExentaDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaExenta,
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
                    MontoItem = 100000m
                }
            }
        };
    }

    private XDocument CreateValidFacturaExentaXml()
    {
        return XDocument.Parse(@"
            <Documento>
                <IdDoc>
                    <TipoDTE>34</TipoDTE>
                    <Folio>12345</Folio>
                    <FchEmis>2023-01-01</FchEmis>
                </IdDoc>
                <Emisor>
                    <RUTEmisor>11111111-1</RUTEmisor>
                    <RznSoc>Empresa Emisora S.A.</RznSoc>
                    <GiroEmis>Venta de productos</GiroEmis>
                    <Acteco>620100</Acteco>
                </Emisor>
                <Receptor>
                    <RUTRecep>22222222-2</RUTRecep>
                    <RznSocRecep>Cliente S.A.</RznSocRecep>
                </Receptor>
                <Totales>
                    <MntExe>100000</MntExe>
                    <MntTotal>100000</MntTotal>
                </Totales>
                <Detalle>
                    <NroLinDet>1</NroLinDet>
                    <NmbItem>Producto Exento</NmbItem>
                    <QtyItem>1</QtyItem>
                    <PrcItem>100000</PrcItem>
                    <MontoItem>100000</MontoItem>
                </Detalle>
            </Documento>");
    }
}