using Moq;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Infrastructure.Services.DTE;
using System.Xml.Linq;
using Xunit;
using SistemaDeVentas.Infrastructure.Core.Application.Services;

namespace SistemaDeVentas.WinUI.Tests;

/// <summary>
/// Pruebas unitarias para DteSaleService enfocadas en la emisión de boleta electrónica.
/// </summary>
public class DteSaleServiceTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IDetailRepository> _detailRepositoryMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IDteProcessingService> _dteProcessingServiceMock;
    private readonly Mock<ICafRepository> _cafRepositoryMock;
    private readonly Mock<IDteFileStorageService> _dteFileStorageServiceMock;
    private readonly Mock<IPdfGeneratorService> _pdfGeneratorServiceMock;

    public DteSaleServiceTests()
    {
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _detailRepositoryMock = new Mock<IDetailRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _dteProcessingServiceMock = new Mock<IDteProcessingService>();
        _cafRepositoryMock = new Mock<ICafRepository>();
        _dteFileStorageServiceMock = new Mock<IDteFileStorageService>();
        _pdfGeneratorServiceMock = new Mock<IPdfGeneratorService>();
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldGenerateBoletaElectronica_WhenValidSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = true, Date = DateTime.Now, Total = 1000 };
        var details = new List<Detail>
        {
            new Detail { IdProduct = Guid.NewGuid(), Amount = 1, Price = 1000, Total = 1000 }
        };
        var product = new Product { Id = details[0].IdProduct.Value, Name = "Producto Test", Exenta = false };
        var expectedDteXml = XDocument.Parse("<DTE><Documento><IdDoc><TipoDTE>39</TipoDTE><Folio>1</Folio></IdDoc></Documento></DTE>");
        var pdfBytes = new byte[] { 1, 2, 3 };

        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);
        _detailRepositoryMock.Setup(r => r.GetBySaleAsync(saleId)).ReturnsAsync(details);
        _productRepositoryMock.Setup(r => r.GetByIdAsync(details[0].IdProduct.Value)).ReturnsAsync(product);
        _dteProcessingServiceMock.Setup(p => p.BuildSignAndStampDteWithSettings(It.IsAny<DteDocument>(), 39)).ReturnsAsync(expectedDteXml);
        _pdfGeneratorServiceMock.Setup(p => p.GenerateBoletaPdf(It.IsAny<DteDocument>(), false)).Returns(pdfBytes);
        _dteFileStorageServiceMock.Setup(s => s.SaveDteXmlAsync(saleId, It.IsAny<int>(), expectedDteXml)).ReturnsAsync("xml_path");
        _dteFileStorageServiceMock.Setup(s => s.SaveDtePdfAsync(saleId, It.IsAny<int>(), pdfBytes)).ReturnsAsync("pdf_path");

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act
        var result = await service.GenerateDteForSaleAsync(saleId, 39);

        // Assert
        Assert.Equal(expectedDteXml.ToString(), result.ToString());
        _saleRepositoryMock.Verify(r => r.GetByIdAsync(saleId), Times.Once);
        _detailRepositoryMock.Verify(r => r.GetBySaleAsync(saleId), Times.Once);
        _dteProcessingServiceMock.Verify(p => p.BuildSignAndStampDteWithSettings(It.IsAny<DteDocument>(), 39), Times.Once);
        _dteFileStorageServiceMock.Verify(s => s.SaveDteXmlAsync(saleId, It.IsAny<int>(), expectedDteXml), Times.Once);
        _dteFileStorageServiceMock.Verify(s => s.SaveDtePdfAsync(saleId, It.IsAny<int>(), pdfBytes), Times.Once);
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldThrowException_WhenSaleNotFound()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync((Sale?)null);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.GenerateDteForSaleAsync(saleId, 39));
        Assert.Contains("no encontrada", exception.Message);
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldThrowException_WhenSaleNotCompleted()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = false }; // Not completed
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GenerateDteForSaleAsync(saleId, 39));
        Assert.Contains("no completada", exception.Message);
    }

    [Fact]
    public async Task CanGenerateDteForSaleAsync_ShouldReturnTrue_WhenSaleIsCompleted()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = true };
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act
        var result = await service.CanGenerateDteForSaleAsync(saleId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CanGenerateDteForSaleAsync_ShouldReturnFalse_WhenSaleNotCompleted()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = false };
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act
        var result = await service.CanGenerateDteForSaleAsync(saleId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetDteForSaleAsync_ShouldReturnXml_WhenExists()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var xmlString = "<DTE><Documento>Test</Documento></DTE>";
        var sale = new Sale { Id = saleId, DteXml = xmlString };
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act
        var result = await service.GetDteForSaleAsync(saleId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(xmlString, result!.ToString());
    }

    [Fact]
    public async Task GetDteForSaleAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, DteXml = null };
        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act
        var result = await service.GetDteForSaleAsync(saleId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldThrowException_WhenDteProcessingFails()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = true };
        var details = new List<Detail>
        {
            new Detail { IdProduct = Guid.NewGuid(), Amount = 1, Price = 1000, Total = 1000 }
        };
        var product = new Product { Id = details[0].IdProduct.Value, Name = "Producto Test", Exenta = false };

        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);
        _detailRepositoryMock.Setup(r => r.GetBySaleAsync(saleId)).ReturnsAsync(details);
        _productRepositoryMock.Setup(r => r.GetByIdAsync(details[0].IdProduct.Value)).ReturnsAsync(product);
        _dteProcessingServiceMock.Setup(p => p.BuildSignAndStampDteWithSettings(It.IsAny<DteDocument>(), 39))
            .ThrowsAsync(new Exception("Error de procesamiento DTE"));

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => service.GenerateDteForSaleAsync(saleId, 39));
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldThrowException_WhenNoDetails()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = true };
        var details = new List<Detail>(); // Empty details

        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);
        _detailRepositoryMock.Setup(r => r.GetBySaleAsync(saleId)).ReturnsAsync(details);

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.GenerateDteForSaleAsync(saleId, 39));
        Assert.Contains("no tiene detalles", exception.Message);
    }

    [Fact]
    public async Task GenerateDteForSaleAsync_ShouldHandleFileStorageErrors()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var sale = new Sale { Id = saleId, State = true };
        var details = new List<Detail>
        {
            new Detail { IdProduct = Guid.NewGuid(), Amount = 1, Price = 1000, Total = 1000 }
        };
        var product = new Product { Id = details[0].IdProduct.Value, Name = "Producto Test", Exenta = false };
        var expectedDteXml = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");
        var pdfBytes = new byte[] { 1, 2, 3 };

        _saleRepositoryMock.Setup(r => r.GetByIdAsync(saleId)).ReturnsAsync(sale);
        _detailRepositoryMock.Setup(r => r.GetBySaleAsync(saleId)).ReturnsAsync(details);
        _productRepositoryMock.Setup(r => r.GetByIdAsync(details[0].IdProduct.Value)).ReturnsAsync(product);
        _dteProcessingServiceMock.Setup(p => p.BuildSignAndStampDteWithSettings(It.IsAny<DteDocument>(), 39)).ReturnsAsync(expectedDteXml);
        _pdfGeneratorServiceMock.Setup(p => p.GenerateBoletaPdf(It.IsAny<DteDocument>(), false)).Returns(pdfBytes);
        _dteFileStorageServiceMock.Setup(s => s.SaveDteXmlAsync(saleId, It.IsAny<int>(), expectedDteXml))
            .ThrowsAsync(new IOException("Error de almacenamiento"));

        var service = new DteSaleService(
            _saleRepositoryMock.Object,
            _detailRepositoryMock.Object,
            _productRepositoryMock.Object,
            _dteProcessingServiceMock.Object,
            _cafRepositoryMock.Object,
            _dteFileStorageServiceMock.Object,
            _pdfGeneratorServiceMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<IOException>(() => service.GenerateDteForSaleAsync(saleId, 39));
    }
}