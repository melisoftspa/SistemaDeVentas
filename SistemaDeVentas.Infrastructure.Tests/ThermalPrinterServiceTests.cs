using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using SistemaDeVentas.Infrastructure.Services.Printer;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class ThermalPrinterServiceTests
{
    private readonly Mock<IPrinterConfiguration> _printerConfigurationMock;
    private readonly Mock<IPrintJobQueue> _printJobQueueMock;
    private readonly Mock<ILogger<ThermalPrinterService>> _loggerMock;
    private readonly Mock<IThermalPrinterService> _thermalPrinterMock;
    private readonly ThermalPrinterService _service;

    public ThermalPrinterServiceTests()
    {
        _printerConfigurationMock = new Mock<IPrinterConfiguration>();
        _printJobQueueMock = new Mock<IPrintJobQueue>();
        _loggerMock = new Mock<ILogger<ThermalPrinterService>>();
        _thermalPrinterMock = new Mock<IThermalPrinterService>();

        _service = new ThermalPrinterService(
            _printerConfigurationMock.Object,
            _printJobQueueMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task ConnectAsync_WithValidConfig_ShouldConnectSuccessfully()
    {
        // Arrange
        var printerName = "TestPrinter";
        var config = new PrinterConfig
        {
            Id = printerName,
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB
            }
        };

        _printerConfigurationMock
            .Setup(x => x.GetConfigurationAsync(printerName))
            .ReturnsAsync(Result.Ok(config));

        _thermalPrinterMock
            .Setup(x => x.ConnectAsync(It.IsAny<string>()))
            .ReturnsAsync(Result.Ok(true));

        // Act
        var result = await _service.ConnectAsync(printerName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task ConnectAsync_WithInvalidConfig_ShouldFail()
    {
        // Arrange
        var printerName = "InvalidPrinter";
        _printerConfigurationMock
            .Setup(x => x.GetConfigurationAsync(printerName))
            .ReturnsAsync(Result.Fail("Configuración no encontrada"));

        // Act
        var result = await _service.ConnectAsync(printerName);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Configuración no encontrada");
    }

    [Fact]
    public async Task PrintAsync_ShouldEnqueueJobSuccessfully()
    {
        // Arrange
        var data = "Test data";
        var priority = PrintJobPriority.Normal;
        var jobId = Guid.NewGuid();

        _printJobQueueMock
            .Setup(x => x.EnqueueAsync(It.IsAny<byte[]>(), priority))
            .ReturnsAsync(Result.Ok(jobId));

        // Act
        var result = await _service.PrintAsync(data, priority);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(jobId);
        _printJobQueueMock.Verify(x => x.EnqueueAsync(It.IsAny<byte[]>(), priority), Times.Once);
    }

    [Fact]
    public async Task PrintRawAsync_ShouldEnqueueRawDataSuccessfully()
    {
        // Arrange
        var data = new byte[] { 0x1B, 0x40 }; // ESC/POS initialize command
        var priority = PrintJobPriority.High;
        var jobId = Guid.NewGuid();

        _printJobQueueMock
            .Setup(x => x.EnqueueAsync(data, priority))
            .ReturnsAsync(Result.Ok(jobId));

        // Act
        var result = await _service.PrintRawAsync(data, priority);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(jobId);
        _printJobQueueMock.Verify(x => x.EnqueueAsync(data, priority), Times.Once);
    }

    [Fact]
    public async Task GetStatusAsync_WhenNotConnected_ShouldReturnDisconnected()
    {
        // Act
        var result = await _service.GetStatusAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("Desconectado");
    }

    [Fact]
    public async Task IsCompatibleAsync_WithRPT008_ShouldReturnTrue()
    {
        // Act
        var result = await _service.IsCompatibleAsync("RPT008");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task IsCompatibleAsync_WithUnsupportedModel_ShouldReturnFalse()
    {
        // Act
        var result = await _service.IsCompatibleAsync("UnsupportedModel");

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("no compatible");
    }

    [Fact]
    public async Task FormatReceiptDataAsync_WhenNotConnected_ShouldFail()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), Total = 100 };

        // Act
        var result = await _service.FormatReceiptDataAsync(sale);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("No hay impresora conectada");
    }

    [Fact]
    public async Task FormatInvoiceDataAsync_WhenNotConnected_ShouldFail()
    {
        // Arrange
        var dteDocument = new DteDocument();
        var qrCodeData = "test-qr";

        // Act
        var result = await _service.FormatInvoiceDataAsync(dteDocument, qrCodeData);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("No hay impresora conectada");
    }

    [Fact]
    public async Task PrintReceiptAsync_ShouldFormatAndEnqueueSuccessfully()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), Total = 100 };
        var formattedData = new byte[] { 0x1B, 0x40, 0x42, 0x4F, 0x4C, 0x45, 0x54, 0x41 }; // Formatted receipt data
        var jobId = Guid.NewGuid();

        // Setup connection
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings { Model = PrinterModel.RPT008 }
        };
        _printerConfigurationMock
            .Setup(x => x.GetConfigurationAsync("TestPrinter"))
            .ReturnsAsync(Result.Ok(config));
        _thermalPrinterMock.Setup(x => x.ConnectAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(true));

        await _service.ConnectAsync("TestPrinter");

        _thermalPrinterMock
            .Setup(x => x.FormatReceiptDataAsync(sale))
            .ReturnsAsync(Result.Ok(formattedData));

        _printJobQueueMock
            .Setup(x => x.EnqueueAsync(formattedData, PrintJobPriority.Normal))
            .ReturnsAsync(Result.Ok(jobId));

        // Act
        var result = await _service.PrintReceiptAsync(sale);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(jobId);
        _thermalPrinterMock.Verify(x => x.FormatReceiptDataAsync(sale), Times.Once);
        _printJobQueueMock.Verify(x => x.EnqueueAsync(formattedData, PrintJobPriority.Normal), Times.Once);
    }

    [Fact]
    public async Task PrintInvoiceAsync_ShouldFormatAndEnqueueWithHighPriority()
    {
        // Arrange
        var dteDocument = new DteDocument();
        var qrCodeData = "test-qr";
        var formattedData = new byte[] { 0x1B, 0x40, 0x46, 0x41, 0x43, 0x54, 0x55, 0x52, 0x41 }; // Formatted invoice data
        var jobId = Guid.NewGuid();

        // Setup connection
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings { Model = PrinterModel.RPT008 }
        };
        _printerConfigurationMock
            .Setup(x => x.GetConfigurationAsync("TestPrinter"))
            .ReturnsAsync(Result.Ok(config));
        _thermalPrinterMock.Setup(x => x.ConnectAsync(It.IsAny<string>())).ReturnsAsync(Result.Ok(true));

        await _service.ConnectAsync("TestPrinter");

        _thermalPrinterMock
            .Setup(x => x.FormatInvoiceDataAsync(dteDocument, qrCodeData))
            .ReturnsAsync(Result.Ok(formattedData));

        _printJobQueueMock
            .Setup(x => x.EnqueueAsync(formattedData, PrintJobPriority.High))
            .ReturnsAsync(Result.Ok(jobId));

        // Act
        var result = await _service.PrintInvoiceAsync(dteDocument, qrCodeData);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(jobId);
        _thermalPrinterMock.Verify(x => x.FormatInvoiceDataAsync(dteDocument, qrCodeData), Times.Once);
        _printJobQueueMock.Verify(x => x.EnqueueAsync(formattedData, PrintJobPriority.High), Times.Once);
    }

    [Fact]
    public async Task CutPaperAsync_WhenNotConnected_ShouldFail()
    {
        // Act
        var result = await _service.CutPaperAsync();

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("No hay impresora conectada");
    }

    [Fact]
    public async Task InitializeAsync_WhenNotConnected_ShouldFail()
    {
        // Act
        var result = await _service.InitializeAsync();

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Be("No hay impresora conectada");
    }

    [Fact]
    public async Task DisconnectAsync_WhenNotConnected_ShouldSucceed()
    {
        // Act
        var result = await _service.DisconnectAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }
}