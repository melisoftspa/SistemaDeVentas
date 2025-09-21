using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using SistemaDeVentas.Infrastructure.Services.Printer;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class PrinterConfigurationTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly PrinterConfiguration _service;

    public PrinterConfigurationTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _service = new PrinterConfiguration(_configurationMock.Object);
    }

    [Fact]
    public async Task GetConfigurationAsync_WithValidConfig_ShouldReturnConfiguration()
    {
        // Arrange
        var printerId = "TestPrinter";
        var mockSection = new Mock<IConfigurationSection>();
        var settingsSection = new Mock<IConfigurationSection>();

        // Setup section hierarchy
        _configurationMock
            .Setup(x => x.GetSection("ThermalPrinters"))
            .Returns(mockSection.Object);

        mockSection
            .Setup(x => x.GetSection(printerId))
            .Returns(settingsSection.Object);

        settingsSection
            .Setup(x => x.Exists())
            .Returns(true);

        settingsSection
            .Setup(x => x["Model"])
            .Returns("RPT008");

        settingsSection
            .Setup(x => x["ConnectionType"])
            .Returns("USB");

        settingsSection
            .Setup(x => x["Port"])
            .Returns("COM1");

        settingsSection
            .Setup(x => x["BaudRate"])
            .Returns("9600");

        settingsSection
            .Setup(x => x["TimeoutMilliseconds"])
            .Returns("5000");

        settingsSection
            .Setup(x => x["PaperWidth"])
            .Returns("32");

        settingsSection
            .Setup(x => x["Name"])
            .Returns("Test Printer");

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
        var mockSection = new Mock<IConfigurationSection>();
        var settingsSection = new Mock<IConfigurationSection>();

        _configurationMock
            .Setup(x => x.GetSection("ThermalPrinters"))
            .Returns(mockSection.Object);

        mockSection
            .Setup(x => x.GetSection(printerId))
            .Returns(settingsSection.Object);

        settingsSection
            .Setup(x => x.Exists())
            .Returns(false);

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
        var printerId = "TestPrinter";
        var mockSection = new Mock<IConfigurationSection>();
        var settingsSection = new Mock<IConfigurationSection>();

        _configurationMock
            .Setup(x => x.GetSection("ThermalPrinters"))
            .Returns(mockSection.Object);

        mockSection
            .Setup(x => x.GetSection(printerId))
            .Returns(settingsSection.Object);

        settingsSection
            .Setup(x => x.Exists())
            .Returns(true);

        settingsSection
            .Setup(x => x["Model"])
            .Returns("InvalidModel");

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
        // Arrange
        var mockThermalPrintersSection = new Mock<IConfigurationSection>();
        var mockPrinter1Section = new Mock<IConfigurationSection>();
        var mockPrinter2Section = new Mock<IConfigurationSection>();

        _configurationMock
            .Setup(x => x.GetSection("ThermalPrinters"))
            .Returns(mockThermalPrintersSection.Object);

        var children = new List<IConfigurationSection> { mockPrinter1Section.Object, mockPrinter2Section.Object };
        mockThermalPrintersSection
            .Setup(x => x.GetChildren())
            .Returns(children);

        // Setup first printer
        mockPrinter1Section.Setup(x => x.Key).Returns("Printer1");
        mockPrinter1Section.Setup(x => x.Exists()).Returns(true);
        mockPrinter1Section.Setup(x => x["Model"]).Returns("RPT008");
        mockPrinter1Section.Setup(x => x["ConnectionType"]).Returns("USB");
        mockPrinter1Section.Setup(x => x["Name"]).Returns("Printer 1");

        // Setup second printer
        mockPrinter2Section.Setup(x => x.Key).Returns("Printer2");
        mockPrinter2Section.Setup(x => x.Exists()).Returns(true);
        mockPrinter2Section.Setup(x => x["Model"]).Returns("Generic");
        mockPrinter2Section.Setup(x => x["ConnectionType"]).Returns("USB");
        mockPrinter2Section.Setup(x => x["Name"]).Returns("Printer 2");

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