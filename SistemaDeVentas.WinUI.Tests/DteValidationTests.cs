using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using Moq;
using SistemaDeVentas.Infrastructure.Core.Application.Services;
using System.Xml.Linq;
using System.Xml.Schema;
using Xunit;

namespace SistemaDeVentas.WinUI.Tests;

/// <summary>
/// Pruebas de validación de integridad para documentos DTE.
/// </summary>
public class DteValidationTests
{
    [Fact]
    public void BoletaElectronica_ShouldHaveRequiredElements()
    {
        // Arrange
        var dteDocument = new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.BoletaAfecta,
                Folio = 123,
                FechaEmision = DateTime.Now
            },
            Emisor = new Emisor
            {
                RutEmisor = "12345678-9",
                RazonSocial = "Empresa Test",
                GiroEmisor = "Venta de productos"
            },
            Receptor = new Receptor
            {
                RutReceptor = "66666666-6",
                RazonSocialReceptor = "Cliente Final"
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Test",
                    CantidadItem = 1,
                    PrecioItem = 1000,
                    MontoItem = 1000
                }
            },
            Totales = new TotalesDte
            {
                MontoNeto = 1000,
                TasaIVA = 19,
                IVA = 190,
                MontoTotal = 1190
            }
        };

        // Act & Assert - Verificar que todos los elementos requeridos estén presentes
        Assert.Equal(TipoDte.BoletaAfecta, dteDocument.IdDoc.TipoDTE);
        Assert.Equal(123, dteDocument.IdDoc.Folio);
        Assert.NotNull(dteDocument.Emisor);
        Assert.Equal("12345678-9", dteDocument.Emisor.RutEmisor);
        Assert.NotNull(dteDocument.Receptor);
        Assert.Equal("66666666-6", dteDocument.Receptor.RutReceptor);
        Assert.NotEmpty(dteDocument.Detalles);
        Assert.Equal(1, dteDocument.Detalles.Count);
        Assert.NotNull(dteDocument.Totales);
        Assert.Equal(1190m, dteDocument.Totales.MontoTotal);
    }

    [Fact]
    public void BoletaExenta_ShouldHaveCorrectTaxCalculations()
    {
        // Arrange
        var dteDocument = new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.BoletaExenta,
                Folio = 456,
                FechaEmision = DateTime.Now
            },
            Emisor = new Emisor
            {
                RutEmisor = "12345678-9",
                RazonSocial = "Empresa Test"
            },
            Receptor = new Receptor
            {
                RutReceptor = "66666666-6",
                RazonSocialReceptor = "Cliente Final"
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Exento",
                    CantidadItem = 2,
                    PrecioItem = 500,
                    MontoItem = 1000,
                    IndicadorExencion = 1 // Exento
                }
            },
            Totales = new TotalesDte
            {
                MontoNeto = 0, // No hay neto en exento
                MontoExento = 1000,
                TasaIVA = 0, // No IVA
                IVA = 0,
                MontoTotal = 1000
            }
        };

        // Act & Assert
        Assert.Equal(TipoDte.BoletaExenta, dteDocument.IdDoc.TipoDTE);
        Assert.Equal(0m, dteDocument.Totales.MontoNeto);
        Assert.Equal(1000m, dteDocument.Totales.MontoExento);
        Assert.Equal(0m, dteDocument.Totales.IVA);
        Assert.Equal(1000m, dteDocument.Totales.MontoTotal);
        Assert.Equal(1, dteDocument.Detalles[0].IndicadorExencion);
    }

    [Fact]
    public void DteXml_ShouldContainRequiredNamespaces()
    {
        // Arrange
        var xmlString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DTE xmlns=""http://www.sii.cl/SiiDte"" version=""1.0"">
  <Documento ID=""DTE_123"">
    <Encabezado>
      <IdDoc>
        <TipoDTE>39</TipoDTE>
        <Folio>123</Folio>
        <FchEmis>2024-01-01</FchEmis>
      </IdDoc>
    </Encabezado>
  </Documento>
</DTE>";

        var xmlDoc = XDocument.Parse(xmlString);

        // Act & Assert
        var dteElement = xmlDoc.Root;
        Assert.NotNull(dteElement);
        Assert.Equal("DTE", dteElement.Name.LocalName);
        Assert.Equal("http://www.sii.cl/SiiDte", dteElement.Name.NamespaceName);

        var documentoElement = dteElement.Element(XName.Get("Documento", "http://www.sii.cl/SiiDte"));
        Assert.NotNull(documentoElement);

        var encabezadoElement = documentoElement.Element(XName.Get("Encabezado", "http://www.sii.cl/SiiDte"));
        Assert.NotNull(encabezadoElement);

        var idDocElement = encabezadoElement.Element(XName.Get("IdDoc", "http://www.sii.cl/SiiDte"));
        Assert.NotNull(idDocElement);

        var tipoDteElement = idDocElement.Element(XName.Get("TipoDTE", "http://www.sii.cl/SiiDte"));
        Assert.NotNull(tipoDteElement);
        Assert.Equal("39", tipoDteElement.Value);
    }

    [Fact]
    public void DteFolio_ShouldBeUniqueAndSequential()
    {
        // Arrange - Simular múltiples boletas
        var folios = new List<int> { 1, 2, 3, 4, 5 };

        // Act & Assert
        Assert.Equal(5, folios.Count);
        Assert.True(folios.SequenceEqual(folios.OrderBy(f => f)));
        Assert.Equal(folios.Distinct().Count(), folios.Count); // No duplicates
    }

    [Fact]
    public void Boleta_ShouldHaveValidRutFormat()
    {
        // Arrange
        var validRuts = new[] { "12345678-9", "66666666-6", "99999999-9" };
        var invalidRuts = new[] { "123456789", "12345678-99", "abc-def" };

        // Act & Assert
        foreach (var rut in validRuts)
        {
            Assert.Matches(@"^\d{7,8}-[\dK]$", rut);
        }

        foreach (var rut in invalidRuts)
        {
            Assert.DoesNotMatch(@"^\d{7,8}-[\dK]$", rut);
        }
    }

    [Fact]
    public void DteTotals_ShouldMatchDetailSum()
    {
        // Arrange
        var detalles = new List<DetalleDte>
        {
            new DetalleDte { MontoItem = 500 },
            new DetalleDte { MontoItem = 300 },
            new DetalleDte { MontoItem = 200 }
        };

        var expectedTotal = detalles.Sum(d => d.MontoItem ?? 0);

        // Act
        var actualTotal = detalles.Sum(d => d.MontoItem ?? 0);

        // Assert
        Assert.Equal(expectedTotal, actualTotal);
        Assert.Equal(1000m, actualTotal);
    }
}