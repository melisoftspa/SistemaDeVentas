using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Infrastructure.Services.DTE;
using System.Xml.Linq;
using Xunit;

namespace SistemaDeVentas.WinUI.Tests;

/// <summary>
/// Pruebas unitarias para DteFileStorageService.
/// </summary>
public class DteFileStorageServiceTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly IDteFileStorageService _service;

    public DteFileStorageServiceTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), "DteStorageTests", Guid.NewGuid().ToString());
        _service = new DteFileStorageService(_testDirectory);
    }

    [Fact]
    public async Task SaveDteXmlAsync_ShouldSaveXmlFile()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var folio = 123;
        var xmlContent = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");

        // Act
        var filePath = await _service.SaveDteXmlAsync(saleId, folio, xmlContent);

        // Assert
        Assert.True(File.Exists(filePath));
        var savedContent = XDocument.Load(filePath);
        Assert.Equal(xmlContent.ToString(), savedContent.ToString());
        Assert.Contains($"DTE_{saleId}_{folio}.xml", filePath);
    }

    [Fact]
    public async Task SaveDtePdfAsync_ShouldSavePdfFile()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var folio = 456;
        var pdfBytes = new byte[] { 1, 2, 3, 4, 5 }; // Mock PDF content

        // Act
        var filePath = await _service.SaveDtePdfAsync(saleId, folio, pdfBytes);

        // Assert
        Assert.True(File.Exists(filePath));
        var savedBytes = await File.ReadAllBytesAsync(filePath);
        Assert.Equal(pdfBytes, savedBytes);
        Assert.Contains($"DTE_{saleId}_{folio}.pdf", filePath);
    }

    [Fact]
    public void GetStorageDirectory_ShouldReturnConfiguredDirectory()
    {
        // Act
        var directory = _service.GetStorageDirectory();

        // Assert
        Assert.Equal(_testDirectory, directory);
    }

    [Fact]
    public async Task SaveDteXmlAsync_ShouldCreateDirectoryIfNotExists()
    {
        // Arrange
        var newDirectory = Path.Combine(_testDirectory, "SubDir");
        var service = new DteFileStorageService(newDirectory);
        var saleId = Guid.NewGuid();
        var folio = 789;
        var xmlContent = XDocument.Parse("<DTE><Documento>Test</Documento></DTE>");

        // Act
        var filePath = await service.SaveDteXmlAsync(saleId, folio, xmlContent);

        // Assert
        Assert.True(Directory.Exists(newDirectory));
        Assert.True(File.Exists(filePath));
    }

    public void Dispose()
    {
        // Cleanup
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }
}