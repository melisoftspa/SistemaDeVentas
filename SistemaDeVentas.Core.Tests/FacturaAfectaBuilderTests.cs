using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using Xunit;

namespace SistemaDeVentas.Core.Tests;

public class FacturaAfectaBuilderTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly FacturaAfectaBuilder _builder;

    public FacturaAfectaBuilderTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _builder = new FacturaAfectaBuilder(_loggerMock.Object);
    }

    [Fact]
    public void BuildXml_ValidFacturaAfectaDocument_ReturnsValidXml()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();

        // Act
        var result = _builder.BuildXml(dteDocument);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Documento", result.Root?.Name.LocalName);
        Assert.Equal("33", result.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value);
    }

    [Fact]
    public void BuildXml_InvalidTipoDte_ThrowsArgumentException()
    {
        // Arrange
        var dteDocument = CreateValidFacturaAfectaDocument();
        dteDocument.IdDoc.TipoDTE = TipoDte.FacturaExenta; // Tipo incorrecto

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _builder.BuildXml(dteDocument));
        Assert.Contains("Factura Afecta (33)", exception.Message);
    }

    [Fact]
    public void ValidateXml_ValidFacturaAfectaXml_ReturnsTrue()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaAfectaXml();

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateXml_XmlWithoutIva_ReturnsFalse()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaAfectaXml();
        var totales = xmlDocument.Root?.Element("Encabezado")?.Element("Totales");
        totales?.Element("IVA")?.Remove(); // Remover IVA, que debe estar en afecta

        // Act
        var result = _builder.ValidateXml(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateXml_XmlWithEmptyIva_ReturnsFalse()
    {
        // Arrange
        var xmlDocument = CreateValidFacturaAfectaXml();
        var ivaElement = xmlDocument.Root?.Element("Encabezado")?.Element("Totales")?.Element("IVA");
        if (ivaElement != null)
        {
            ivaElement.Value = ""; // IVA vacío
        }

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

    private DteDocument CreateValidFacturaAfectaDocument()
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

    private XDocument CreateValidFacturaAfectaXml()
    {
        return XDocument.Parse(@"
            <Documento>
                <Encabezado>
                    <IdDoc>
                        <TipoDTE>33</TipoDTE>
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
                        <MntNeto>100000</MntNeto>
                        <TasaIVA>19</TasaIVA>
                        <IVA>19000</IVA>
                        <MntTotal>119000</MntTotal>
                    </Totales>
                </Encabezado>
                <Detalle>
                    <NroLinDet>1</NroLinDet>
                    <NmbItem>Producto Afecto</NmbItem>
                    <QtyItem>1</QtyItem>
                    <PrcItem>119000</PrcItem>
                    <MontoItem>119000</MontoItem>
                </Detalle>
            </Documento>");
    }
}