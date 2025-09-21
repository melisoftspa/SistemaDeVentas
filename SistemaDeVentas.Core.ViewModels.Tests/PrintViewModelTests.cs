using FluentAssertions;
using FluentResults;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using Xunit;

namespace SistemaDeVentas.Core.ViewModels.Tests;

public class PrintViewModelTests
{
    private readonly Mock<IThermalPrinterService> _thermalPrinterServiceMock;
    private readonly Mock<IPrinterConfiguration> _printerConfigurationMock;
    private readonly Mock<IPrintJobQueue> _printJobQueueMock;
    private readonly Mock<ILogger<PrintViewModel>> _loggerMock;
    private readonly PrintViewModel _viewModel;

    public PrintViewModelTests()
    {
        _thermalPrinterServiceMock = new Mock<IThermalPrinterService>();
        _printerConfigurationMock = new Mock<IPrinterConfiguration>();
        _printJobQueueMock = new Mock<IPrintJobQueue>();
        _loggerMock = new Mock<ILogger<PrintViewModel>>();

        _viewModel = new PrintViewModel(
            _thermalPrinterServiceMock.Object,
            _printerConfigurationMock.Object,
            _printJobQueueMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Assert
        _viewModel.Title.Should().Be("Gestión de Impresión");
        _viewModel.AvailablePrinters.Should().NotBeNull();
        _viewModel.PrintJobs.Should().NotBeNull();
        _viewModel.PrintStatus.Should().Be("Listo");
        _viewModel.IsPrinting.Should().BeFalse();
    }

    [Fact]
    public void Constructor_ShouldLoadAvailablePrinters()
    {
        // Arrange
        var printers = new List<string> { "Printer1", "Printer2" };
        _printerConfigurationMock
            .Setup(x => x.ListAvailablePrintersAsync())
            .ReturnsAsync(Result.Ok(printers));

        // Act - Constructor ya llama a LoadAvailablePrintersAsync

        // Assert
        _printerConfigurationMock.Verify(x => x.ListAvailablePrintersAsync(), Times.Once);
    }

    [Fact]
    public void SelectedPrinter_SetProperty_ShouldRaiseCanExecuteChanged()
    {
        // Arrange
        var initialValue = _viewModel.PrintReceiptCommand.CanExecute(null);
        var initialValue2 = _viewModel.PrintInvoiceCommand.CanExecute(null);
        var initialValue3 = _viewModel.TestPrintCommand.CanExecute(null);

        // Act
        _viewModel.SelectedPrinter = "TestPrinter";

        // Assert
        _viewModel.PrintReceiptCommand.CanExecute(null).Should().Be(initialValue); // Sin CurrentSale
        _viewModel.PrintInvoiceCommand.CanExecute(null).Should().Be(initialValue2); // Sin CurrentDteDocument
        _viewModel.TestPrintCommand.CanExecute(null).Should().BeTrue(); // Solo requiere SelectedPrinter
    }

    [Fact]
    public void CurrentSale_SetProperty_ShouldRaiseCanExecuteChanged()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";
        var sale = new Sale { Id = Guid.NewGuid(), Total = 100 };

        // Act
        _viewModel.CurrentSale = sale;

        // Assert
        _viewModel.PrintReceiptCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public void CurrentDteDocument_SetProperty_ShouldRaiseCanExecuteChanged()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";
        var dteDocument = new DteDocument();

        // Act
        _viewModel.CurrentDteDocument = dteDocument;

        // Assert
        _viewModel.PrintInvoiceCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public async Task PrintReceiptAsync_WithValidData_ShouldPrintSuccessfully()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), Total = 100 };
        _viewModel.CurrentSale = sale;
        _viewModel.SelectedPrinter = "TestPrinter";

        var jobId = Guid.NewGuid();
        _thermalPrinterServiceMock
            .Setup(x => x.PrintReceiptAsync(sale))
            .ReturnsAsync(Result.Ok(jobId));

        _printJobQueueMock
            .Setup(x => x.GetPendingJobsAsync())
            .ReturnsAsync(Result.Ok(new List<PrintJob>()));

        // Act
        await _viewModel.PrintReceiptCommand.Execute(null);

        // Assert
        _thermalPrinterServiceMock.Verify(x => x.PrintReceiptAsync(sale), Times.Once);
        _viewModel.SuccessMessage.Should().Contain($"Boleta impresa exitosamente. ID trabajo: {jobId}");
        _viewModel.PrintStatus.Should().Be("Boleta impresa");
        _viewModel.IsPrinting.Should().BeFalse();
    }

    [Fact]
    public async Task PrintReceiptAsync_WithError_ShouldSetErrorMessage()
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid(), Total = 100 };
        _viewModel.CurrentSale = sale;
        _viewModel.SelectedPrinter = "TestPrinter";

        var errorMessage = "Error de conexión";
        _thermalPrinterServiceMock
            .Setup(x => x.PrintReceiptAsync(sale))
            .ReturnsAsync(Result.Fail(errorMessage));

        // Act
        _viewModel.PrintReceiptCommand.Execute(null);

        // Assert
        _viewModel.SuccessMessage.Should().BeEmpty();
        _viewModel.PrintStatus.Should().Be("Error en impresión");
        _viewModel.IsPrinting.Should().BeFalse();
    }

    [Fact]
    public async Task PrintInvoiceAsync_WithValidData_ShouldPrintSuccessfully()
    {
        // Arrange
        var dteDocument = new DteDocument();
        _viewModel.CurrentDteDocument = dteDocument;
        _viewModel.SelectedPrinter = "TestPrinter";
        _viewModel.QrCodeData = "test-qr";

        var jobId = Guid.NewGuid();
        _thermalPrinterServiceMock
            .Setup(x => x.PrintInvoiceAsync(dteDocument, "test-qr"))
            .ReturnsAsync(Result.Ok(jobId));

        _printJobQueueMock
            .Setup(x => x.GetPendingJobsAsync())
            .ReturnsAsync(Result.Ok(new List<PrintJob>()));

        // Act
        _viewModel.PrintInvoiceCommand.Execute(null);

        // Assert
        _thermalPrinterServiceMock.Verify(x => x.PrintInvoiceAsync(dteDocument, "test-qr"), Times.Once);
        _viewModel.SuccessMessage.Should().Contain($"Factura impresa exitosamente. ID trabajo: {jobId}");
        _viewModel.PrintStatus.Should().Be("Factura impresa");
    }

    [Fact]
    public async Task TestPrintAsync_WithValidData_ShouldPrintSuccessfully()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";

        var jobId = Guid.NewGuid();
        _thermalPrinterServiceMock
            .Setup(x => x.PrintAsync(It.IsAny<string>(), PrintJobPriority.Normal))
            .ReturnsAsync(Result.Ok(jobId));

        _printJobQueueMock
            .Setup(x => x.GetPendingJobsAsync())
            .ReturnsAsync(Result.Ok(new List<PrintJob>()));

        // Act
        _viewModel.TestPrintCommand.Execute(null);

        // Assert
        _thermalPrinterServiceMock.Verify(x => x.PrintAsync(It.IsAny<string>(), PrintJobPriority.Normal), Times.Once);
        _viewModel.SuccessMessage.Should().Contain($"Prueba impresa exitosamente. ID trabajo: {jobId}");
        _viewModel.PrintStatus.Should().Be("Prueba impresa");
    }

    [Fact]
    public async Task CancelPrintJobAsync_WithValidJob_ShouldCancelSuccessfully()
    {
        // Arrange
        var job = new PrintJob { Id = Guid.NewGuid(), Status = PrintJobStatus.Pending };
        _printJobQueueMock
            .Setup(x => x.CancelAsync(job.Id))
            .ReturnsAsync(Result.Ok(true));

        _printJobQueueMock
            .Setup(x => x.GetPendingJobsAsync())
            .ReturnsAsync(Result.Ok(new List<PrintJob>()));

        // Act
        _viewModel.CancelPrintJobCommand.Execute(job);

        // Assert
        _printJobQueueMock.Verify(x => x.CancelAsync(job.Id), Times.Once);
        _viewModel.SuccessMessage.Should().Be("Trabajo de impresión cancelado exitosamente");
    }

    [Fact]
    public async Task RefreshPrintersCommand_ShouldLoadAvailablePrinters()
    {
        // Arrange
        var printers = new List<string> { "Printer1", "Printer2" };
        _printerConfigurationMock
            .Setup(x => x.ListAvailablePrintersAsync())
            .ReturnsAsync(Result.Ok(printers));

        // Act
        _viewModel.RefreshPrintersCommand.Execute(null);

        // Assert
        _printerConfigurationMock.Verify(x => x.ListAvailablePrintersAsync(), Times.Once);
        _viewModel.AvailablePrinters.Should().BeEquivalentTo(printers);
    }

    [Fact]
    public void CanPrintReceipt_ShouldReturnFalse_WhenNoSale()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";
        _viewModel.CurrentSale = null;

        // Act & Assert
        _viewModel.PrintReceiptCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CanPrintReceipt_ShouldReturnFalse_WhenNoPrinter()
    {
        // Arrange
        _viewModel.SelectedPrinter = string.Empty;
        _viewModel.CurrentSale = new Sale { Id = Guid.NewGuid() };

        // Act & Assert
        _viewModel.PrintReceiptCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CanPrintReceipt_ShouldReturnFalse_WhenPrinting()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";
        _viewModel.CurrentSale = new Sale { Id = Guid.NewGuid() };
        _viewModel.IsPrinting = true;

        // Act & Assert
        _viewModel.PrintReceiptCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CanPrintInvoice_ShouldReturnFalse_WhenNoDteDocument()
    {
        // Arrange
        _viewModel.SelectedPrinter = "TestPrinter";
        _viewModel.CurrentDteDocument = null;

        // Act & Assert
        _viewModel.PrintInvoiceCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CanTestPrint_ShouldReturnFalse_WhenNoPrinter()
    {
        // Arrange
        _viewModel.SelectedPrinter = string.Empty;

        // Act & Assert
        _viewModel.TestPrintCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CancelPrintJobCommand_ShouldNotExecute_WhenJobIsNull()
    {
        // Act & Assert
        _viewModel.CancelPrintJobCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void CancelPrintJobCommand_ShouldNotExecute_WhenJobIsCompleted()
    {
        // Arrange
        var job = new PrintJob { Status = PrintJobStatus.Completed };

        // Act & Assert
        _viewModel.CancelPrintJobCommand.CanExecute(job).Should().BeFalse();
    }

    [Fact]
    public void CancelPrintJobCommand_ShouldExecute_WhenJobIsPending()
    {
        // Arrange
        var job = new PrintJob { Status = PrintJobStatus.Pending };

        // Act & Assert
        _viewModel.CancelPrintJobCommand.CanExecute(job).Should().BeTrue();
    }
}