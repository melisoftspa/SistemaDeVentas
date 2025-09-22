using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using SistemaDeVentas.Infrastructure.Services.Printer;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class PrinterConfigurationTests
{
    private readonly IConfiguration _configuration;
    private readonly PrinterConfiguration _service;

    public PrinterConfigurationTests()
    {
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["ThermalPrinters:TestPrinter:Model"] = "RPT008",
            ["ThermalPrinters:TestPrinter:ConnectionType"] = "USB",
            ["ThermalPrinters:TestPrinter:Port"] = "COM1",
            ["ThermalPrinters:TestPrinter:BaudRate"] = "9600",
            ["ThermalPrinters:TestPrinter:TimeoutMilliseconds"] = "5000",
            ["ThermalPrinters:TestPrinter:PaperWidth"] = "32",
            ["ThermalPrinters:TestPrinter:Name"] = "Test Printer",
            ["ThermalPrinters:InvalidPrinter:Model"] = "InvalidModel",
            ["ThermalPrinters:Printer1:Model"] = "RPT008",
            ["ThermalPrinters:Printer1:ConnectionType"] = "USB",
            ["ThermalPrinters:Printer1:Name"] = "Printer 1",
            ["ThermalPrinters:Printer2:Model"] = "Generic",
            ["ThermalPrinters:Printer2:ConnectionType"] = "USB",
            ["ThermalPrinters:Printer2:Name"] = "Printer 2"
        });
        _configuration = configBuilder.Build();
        _service = new PrinterConfiguration(_configuration);
    }

    [Fact]
    public async Task GetConfigurationAsync_WithValidConfig_ShouldReturnConfiguration()
    {
        // Arrange
        var printerId = "TestPrinter";

        // Act
        var result = await _service.GetConfigurationAsync(printerId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var config = result.Value;
        config.Id.Should().Be(printerId);
        config.Settings.Model.Should().Be(PrinterModel.RPT008);
        config.Settings.ConnectionType.Should().Be(ConnectionType.USB);
        config.Settings.Port.Should().Be("COM1");
        config.Settings.BaudRate.Should().Be(9600);
        config.Settings.TimeoutMilliseconds.Should().Be(5000);
        config.Settings.PaperWidth.Should().Be(32);
        config.Settings.Name.Should().Be("Test Printer");
    }

    [Fact]
    public async Task GetConfigurationAsync_WithNonExistentConfig_ShouldFail()
    {
        // Arrange
        var printerId = "NonExistentPrinter";

        // Act
        var result = await _service.GetConfigurationAsync(printerId);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("No se encontró configuración");
    }

    [Fact]
    public async Task GetConfigurationAsync_WithInvalidModel_ShouldFail()
    {
        // Arrange
        var printerId = "InvalidPrinter";

        // Act
        var result = await _service.GetConfigurationAsync(printerId);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Error al obtener configuración");
    }

    [Fact]
    public async Task SaveConfigurationAsync_WithValidConfig_ShouldSucceed()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB,
                Port = "COM1",
                BaudRate = 9600,
                TimeoutMilliseconds = 5000,
                PaperWidth = 32,
                Name = "Test Printer"
            }
        };

        // Act
        var result = await _service.SaveConfigurationAsync(config);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task SaveConfigurationAsync_WithInvalidConfig_ShouldFail()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB,
                Port = "", // Invalid: empty port
                BaudRate = 9600,
                TimeoutMilliseconds = 5000,
                PaperWidth = 32,
                Name = "Test Printer"
            }
        };

        // Act
        var result = await _service.SaveConfigurationAsync(config);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Configuración inválida");
    }

    [Fact]
    public async Task GetAllConfigurationsAsync_ShouldReturnAllConfigs()
    {
        // Act
        var result = await _service.GetAllConfigurationsAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        var configs = result.Value;
        configs.Should().HaveCount(2);
        configs.Should().Contain(c => c.Id == "Printer1");
        configs.Should().Contain(c => c.Id == "Printer2");
    }

    [Fact]
    public async Task ListAvailablePrintersAsync_ShouldReturnPrinterList()
    {
        // Act
        var result = await _service.ListAvailablePrintersAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        var printers = result.Value;
        printers.Should().NotBeNull();
        // Note: In test environment, may not have real printers, but should not fail
    }

    [Fact]
    public async Task ValidateConfigurationAsync_WithValidConfig_ShouldSucceed()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB,
                Port = "COM1",
                BaudRate = 9600,
                TimeoutMilliseconds = 5000,
                PaperWidth = 32,
                Name = "Test Printer"
            }
        };

        // Act
        var result = await _service.ValidateConfigurationAsync(config);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateConfigurationAsync_WithInvalidBaudRate_ShouldFail()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.Serial,
                Port = "COM1",
                BaudRate = 0, // Invalid: zero baud rate
                TimeoutMilliseconds = 5000,
                PaperWidth = 32,
                Name = "Test Printer"
            }
        };

        // Act
        var result = await _service.ValidateConfigurationAsync(config);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Configuración inválida");
    }

    [Fact]
    public async Task ValidateConfigurationAsync_WithInvalidPaperWidth_ShouldFail()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB,
                Port = "COM1",
                BaudRate = 9600,
                TimeoutMilliseconds = 5000,
                PaperWidth = 0, // Invalid: zero paper width
                Name = "Test Printer"
            }
        };

        // Act
        var result = await _service.ValidateConfigurationAsync(config);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Configuración inválida");
    }

    [Fact]
    public async Task ValidateConfigurationAsync_WithEmptyName_ShouldFail()
    {
        // Arrange
        var config = new PrinterConfig
        {
            Id = "TestPrinter",
            Settings = new ThermalPrinterSettings
            {
                Model = PrinterModel.RPT008,
                ConnectionType = ConnectionType.USB,
                Port = "COM1",
                BaudRate = 9600,
                TimeoutMilliseconds = 5000,
                PaperWidth = 32,
                Name = "" // Invalid: empty name
            }
        };

        // Act
        var result = await _service.ValidateConfigurationAsync(config);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.First().Message.Should().Contain("Configuración inválida");
    }
}