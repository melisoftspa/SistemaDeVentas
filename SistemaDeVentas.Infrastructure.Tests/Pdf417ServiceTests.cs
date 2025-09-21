using System.Drawing;
using Xunit;
using SistemaDeVentas.Infrastructure.Services.DTE;
using System.Xml.Linq;
using Moq;
using System.Net.Http;

namespace SistemaDeVentas.Infrastructure.Tests;

public class Pdf417ServiceTests
{
    private readonly Pdf417Service _pdf417Service;

    public Pdf417ServiceTests()
    {
        _pdf417Service = new Pdf417Service();
    }

    [Fact]
    public void GeneratePdf417_FromValidText_ReturnsBitmap()
    {
        // Arrange
        var text = "Test PDF417 generation";

        // Act
        var result = _pdf417Service.GeneratePdf417(text);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_FromValidBinaryData_ReturnsBitmap()
    {
        // Arrange
        var data = System.Text.Encoding.UTF8.GetBytes("Test binary data");

        // Act
        var result = _pdf417Service.GeneratePdf417(data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_FromNullText_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _pdf417Service.GeneratePdf417((string)null!));
    }

    [Fact]
    public void GeneratePdf417_FromNullBinaryData_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _pdf417Service.GeneratePdf417((byte[])null!));
    }

    [Fact]
    public void GeneratePdf417_FromEmptyText_ReturnsBitmap()
    {
        // Arrange
        var text = string.Empty;

        // Act
        var result = _pdf417Service.GeneratePdf417(text);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_FromEmptyBinaryData_ReturnsBitmap()
    {
        // Arrange
        var data = Array.Empty<byte>();

        // Act
        var result = _pdf417Service.GeneratePdf417(data);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_ValidatesBitmapDimensions()
    {
        // Arrange
        var text = "Sample text for dimension validation";

        // Act
        var result = _pdf417Service.GeneratePdf417(text);

        // Assert
        Assert.True(result.Width >= 10, "Width should be at least 10 pixels");
        Assert.True(result.Height >= 10, "Height should be at least 10 pixels");
        Assert.True(result.Width <= 1000, "Width should not exceed 1000 pixels");
        Assert.True(result.Height <= 1000, "Height should not exceed 1000 pixels");
    }

    [Fact]
    public void GeneratePdf417_WithDteData_ReturnsValidBitmap()
    {
        // Arrange - Datos típicos de DTE
        var dteData = "12345678-9|33|123|2024-01-15|99999999-9|Cliente Ejemplo|10000|Producto 1|CAF_DATA|2024-01-15T10:30:00";

        // Act
        var result = _pdf417Service.GeneratePdf417(dteData);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_WithTedBinaryData_ReturnsValidBitmap()
    {
        // Arrange - Datos binarios comprimidos simulados de TED
        var tedBinaryData = new byte[] { 0x78, 0x9C, 0xED, 0xC1, 0x01, 0x01, 0x00, 0x00, 0x00, 0x80, 0x90, 0xFE, 0x37, 0x10, 0x00, 0x01, 0x00, 0x01 };

        // Act
        var result = _pdf417Service.GeneratePdf417(tedBinaryData);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_WithLargeTextData_HandlesCorrectly()
    {
        // Arrange - Texto largo típico de DTE
        var largeText = new string('A', 1000) + "|12345678-9|33|123|2024-01-15|99999999-9|Cliente con nombre muy largo|100000|Producto con descripción muy detallada|CAF_DATA_LARGA|2024-01-15T10:30:00";

        // Act
        var result = _pdf417Service.GeneratePdf417(largeText);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange - Texto con caracteres especiales
        var specialText = "RUT: 12.345.678-9|Tipo: 33|Folio: 123|Fecha: 2024-01-15|RUT Receptor: 99.999.999-9|Cliente: José María Ñandú|Monto: $10.000|Producto: Café & Té|CAF: DATA|Timestamp: 2024-01-15T10:30:00";

        // Act
        var result = _pdf417Service.GeneratePdf417(specialText);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }
}

public class Pdf417ServiceIntegrationTests
{
    [Fact]
    public void GeneratePdf417Image_WithValidDteDocument_ReturnsBitmap()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockSignatureService = new Mock<IDigitalSignatureService>();
        var pdf417Service = new Pdf417Service();

        var stampingService = new StampingService(mockHttpClient.Object, mockSignatureService.Object, pdf417Service);

        // Crear documento XML simulado con TED
        var xmlDocument = new XDocument(
            new XElement("DTE",
                new XElement("Documento",
                    new XElement("TED",
                        new XAttribute("version", "1.0"),
                        new XElement("DD",
                            new XElement("RE", "12345678-9"),
                            new XElement("TD", "33"),
                            new XElement("F", "123"),
                            new XElement("FE", "2024-01-15"),
                            new XElement("RR", "99999999-9"),
                            new XElement("RSR", "Cliente Ejemplo"),
                            new XElement("MNT", "10000"),
                            new XElement("IT1", "Producto 1"),
                            new XElement("CAF", new XElement("DA", "CAF_DATA")),
                            new XElement("TSTED", "2024-01-15T10:30:00")
                        ),
                        new XElement("FRMT",
                            new XAttribute("algoritmo", "SHA1withRSA"),
                            "FirmaSimulada"
                        )
                    )
                )
            )
        );

        // Act
        var result = stampingService.GeneratePdf417Image(xmlDocument);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Bitmap>(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void GeneratePdf417Image_WithoutTed_ThrowsArgumentException()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockSignatureService = new Mock<IDigitalSignatureService>();
        var pdf417Service = new Pdf417Service();

        var stampingService = new StampingService(mockHttpClient.Object, mockSignatureService.Object, pdf417Service);

        // Documento sin TED
        var xmlDocument = new XDocument(
            new XElement("DTE",
                new XElement("Documento",
                    new XElement("IdDoc",
                        new XElement("TipoDTE", "33"),
                        new XElement("Folio", "123")
                    )
                )
            )
        );

        // Act & Assert
        Assert.Throws<ArgumentException>(() => stampingService.GeneratePdf417Image(xmlDocument));
    }

    [Fact]
    public void GeneratePdf417Barcode_WithValidDdElement_ReturnsCompressedBytes()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockSignatureService = new Mock<IDigitalSignatureService>();
        var pdf417Service = new Pdf417Service();

        var stampingService = new StampingService(mockHttpClient.Object, mockSignatureService.Object, pdf417Service);

        var ddElement = new XElement("DD",
            new XElement("RE", "12345678-9"),
            new XElement("TD", "33"),
            new XElement("F", "123"),
            new XElement("FE", "2024-01-15"),
            new XElement("RR", "99999999-9"),
            new XElement("RSR", "Cliente Ejemplo"),
            new XElement("MNT", "10000"),
            new XElement("IT1", "Producto 1"),
            new XElement("CAF", new XElement("DA", "CAF_DATA")),
            new XElement("TSTED", "2024-01-15T10:30:00")
        );

        // Act
        var result = stampingService.GeneratePdf417Barcode(ddElement);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        // Los datos comprimidos deberían ser más pequeños que el texto original
        var originalText = "12345678-9|33|123|2024-01-15|99999999-9|Cliente Ejemplo|10000|Producto 1|CAF_DATA|2024-01-15T10:30:00";
        var originalBytes = System.Text.Encoding.UTF8.GetBytes(originalText);
        Assert.True(result.Length <= originalBytes.Length);
    }

    [Fact]
    public void RequestElectronicStamp_GeneratesTedWithPdf417()
    {
        // Arrange
        var mockHttpClient = new Mock<HttpClient>();
        var mockSignatureService = new Mock<IDigitalSignatureService>();
        var pdf417Service = new Pdf417Service();

        var stampingService = new StampingService(mockHttpClient.Object, mockSignatureService.Object, pdf417Service);

        // Documento firmado simulado
        var signedDocument = new XDocument(
            new XElement("DTE",
                new XElement("Documento",
                    new XElement("IdDoc",
                        new XElement("TipoDTE", "33"),
                        new XElement("Folio", "123"),
                        new XElement("FchEmis", "2024-01-15")
                    ),
                    new XElement("Emisor",
                        new XElement("RUTEmisor", "12345678-9")
                    ),
                    new XElement("Receptor",
                        new XElement("RUTRecep", "99999999-9"),
                        new XElement("RznSocRecep", "Cliente Ejemplo")
                    ),
                    new XElement("Totales",
                        new XElement("MntTotal", "10000")
                    ),
                    new XElement("Detalle",
                        new XElement("NmbItem", "Producto 1")
                    ),
                    new XElement("CAF",
                        new XElement("DA", "CAF_DATA")
                    )
                )
            )
        );

        // Act
        var result = stampingService.RequestElectronicStamp(signedDocument, "12345678-9").Result;

        // Assert
        Assert.NotNull(result);
        var ted = stampingService.ExtractTED(result);
        Assert.NotNull(ted);
        Assert.Equal("TED", ted.Name);
        Assert.NotNull(ted.Element("DD"));
        Assert.NotNull(ted.Element("FRMT"));
    }
}