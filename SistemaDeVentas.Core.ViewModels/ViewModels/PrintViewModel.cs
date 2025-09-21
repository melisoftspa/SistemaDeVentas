using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentResults;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Core.ViewModels.ViewModels
{
    public class PrintViewModel : BaseViewModel
    {
        private readonly IThermalPrinterService _thermalPrinterService;
        private readonly IPrinterConfiguration _printerConfiguration;
        private readonly IPrintJobQueue _printJobQueue;
        private readonly ILogger<PrintViewModel> _logger;

        public PrintViewModel(
            IThermalPrinterService thermalPrinterService,
            IPrinterConfiguration printerConfiguration,
            IPrintJobQueue printJobQueue,
            ILogger<PrintViewModel> logger)
        {
            _thermalPrinterService = thermalPrinterService ?? throw new ArgumentNullException(nameof(thermalPrinterService));
            _printerConfiguration = printerConfiguration ?? throw new ArgumentNullException(nameof(printerConfiguration));
            _printJobQueue = printJobQueue ?? throw new ArgumentNullException(nameof(printJobQueue));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Title = "Gestión de Impresión";
            AvailablePrinters = new ObservableCollection<string>();
            PrintJobs = new ObservableCollection<PrintJob>();

            PrintReceiptCommand = new RelayCommand(async () => await PrintReceiptAsync(), CanPrintReceipt);
            PrintInvoiceCommand = new RelayCommand(async () => await PrintInvoiceAsync(), CanPrintInvoice);
            TestPrintCommand = new RelayCommand(async () => await TestPrintAsync(), CanTestPrint);
            CancelPrintJobCommand = new RelayCommand<PrintJob>(async (job) => await CancelPrintJobAsync(job), CanCancelPrintJob);
            RefreshPrintersCommand = new RelayCommand(async () => await RefreshPrintersAsync());

            // Cargar impresoras disponibles al inicializar
            _ = LoadAvailablePrintersAsync();
        }

        // Propiedades observables
        private bool _isPrinting;
        public bool IsPrinting
        {
            get => _isPrinting;
            set => SetProperty(ref _isPrinting, value);
        }

        private string _selectedPrinter = string.Empty;
        public string SelectedPrinter
        {
            get => _selectedPrinter;
            set
            {
                if (SetProperty(ref _selectedPrinter, value))
                {
                    ((RelayCommand)PrintReceiptCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)PrintInvoiceCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)TestPrintCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private ObservableCollection<string> _availablePrinters;
        public ObservableCollection<string> AvailablePrinters
        {
            get => _availablePrinters;
            set => SetProperty(ref _availablePrinters, value);
        }

        private ObservableCollection<PrintJob> _printJobs;
        public ObservableCollection<PrintJob> PrintJobs
        {
            get => _printJobs;
            set => SetProperty(ref _printJobs, value);
        }

        private string _printStatus = "Listo";
        public string PrintStatus
        {
            get => _printStatus;
            set => SetProperty(ref _printStatus, value);
        }

        private string _successMessage = string.Empty;
        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        // Propiedades para impresión (deben ser asignadas desde la UI)
        private Sale? _currentSale;
        public Sale? CurrentSale
        {
            get => _currentSale;
            set
            {
                if (SetProperty(ref _currentSale, value))
                {
                    ((RelayCommand)PrintReceiptCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private DteDocument? _currentDteDocument;
        public DteDocument? CurrentDteDocument
        {
            get => _currentDteDocument;
            set
            {
                if (SetProperty(ref _currentDteDocument, value))
                {
                    ((RelayCommand)PrintInvoiceCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private string _qrCodeData = string.Empty;
        public string QrCodeData
        {
            get => _qrCodeData;
            set => SetProperty(ref _qrCodeData, value);
        }

        // Comandos
        public ICommand PrintReceiptCommand { get; }
        public ICommand PrintInvoiceCommand { get; }
        public ICommand TestPrintCommand { get; }
        public ICommand CancelPrintJobCommand { get; }
        public ICommand RefreshPrintersCommand { get; }

        // Métodos de comandos
        private async Task PrintReceiptAsync()
        {
            if (CurrentSale == null || string.IsNullOrEmpty(SelectedPrinter))
                return;

            try
            {
                IsPrinting = true;
                ClearError();
                SuccessMessage = string.Empty;
                PrintStatus = "Imprimiendo boleta...";

                _logger.LogInformation("Iniciando impresión de boleta para venta {SaleId}", CurrentSale.Id);

                var result = await _thermalPrinterService.PrintReceiptAsync(CurrentSale);

                if (result.IsSuccess)
                {
                    SuccessMessage = $"Boleta impresa exitosamente. ID trabajo: {result.Value}";
                    PrintStatus = "Boleta impresa";
                    _logger.LogInformation("Boleta impresa exitosamente. JobId: {JobId}", result.Value);
                    await LoadPrintJobsAsync();
                }
                else
                {
                    var errorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                    SetError($"Error al imprimir boleta: {errorMessage}");
                    PrintStatus = "Error en impresión";
                    _logger.LogError("Error al imprimir boleta: {Error}", errorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado al imprimir boleta: {ex.Message}");
                PrintStatus = "Error en impresión";
                _logger.LogError(ex, "Error inesperado al imprimir boleta");
            }
            finally
            {
                IsPrinting = false;
            }
        }

        private async Task PrintInvoiceAsync()
        {
            if (CurrentDteDocument == null || string.IsNullOrEmpty(SelectedPrinter))
                return;

            try
            {
                IsPrinting = true;
                ClearError();
                SuccessMessage = string.Empty;
                PrintStatus = "Imprimiendo factura...";

                _logger.LogInformation("Iniciando impresión de factura DTE {DteFolio}", CurrentDteDocument.IdDoc.Folio);

                var result = await _thermalPrinterService.PrintInvoiceAsync(CurrentDteDocument, QrCodeData);

                if (result.IsSuccess)
                {
                    SuccessMessage = $"Factura impresa exitosamente. ID trabajo: {result.Value}";
                    PrintStatus = "Factura impresa";
                    _logger.LogInformation("Factura impresa exitosamente. JobId: {JobId}", result.Value);
                    await LoadPrintJobsAsync();
                }
                else
                {
                    var errorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                    SetError($"Error al imprimir factura: {errorMessage}");
                    PrintStatus = "Error en impresión";
                    _logger.LogError("Error al imprimir factura: {Error}", errorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado al imprimir factura: {ex.Message}");
                PrintStatus = "Error en impresión";
                _logger.LogError(ex, "Error inesperado al imprimir factura");
            }
            finally
            {
                IsPrinting = false;
            }
        }

        private async Task TestPrintAsync()
        {
            if (string.IsNullOrEmpty(SelectedPrinter))
                return;

            try
            {
                IsPrinting = true;
                ClearError();
                SuccessMessage = string.Empty;
                PrintStatus = "Imprimiendo prueba...";

                _logger.LogInformation("Iniciando impresión de prueba en impresora {Printer}", SelectedPrinter);

                var testData = "PRUEBA DE IMPRESION\n" +
                              "==================\n" +
                              "Esta es una prueba de impresión térmica.\n" +
                              "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\n" +
                              "Impresora: " + SelectedPrinter + "\n\n";

                var result = await _thermalPrinterService.PrintAsync(testData, PrintJobPriority.Normal);

                if (result.IsSuccess)
                {
                    SuccessMessage = $"Prueba impresa exitosamente. ID trabajo: {result.Value}";
                    PrintStatus = "Prueba impresa";
                    _logger.LogInformation("Prueba impresa exitosamente. JobId: {JobId}", result.Value);
                    await LoadPrintJobsAsync();
                }
                else
                {
                    var errorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                    SetError($"Error al imprimir prueba: {errorMessage}");
                    PrintStatus = "Error en impresión";
                    _logger.LogError("Error al imprimir prueba: {Error}", errorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado al imprimir prueba: {ex.Message}");
                PrintStatus = "Error en impresión";
                _logger.LogError(ex, "Error inesperado al imprimir prueba");
            }
            finally
            {
                IsPrinting = false;
            }
        }

        private async Task CancelPrintJobAsync(PrintJob? job)
        {
            if (job == null) return;

            try
            {
                ClearError();
                _logger.LogInformation("Cancelando trabajo de impresión {JobId}", job.Id);

                var result = await _printJobQueue.CancelAsync(job.Id);

                if (result.IsSuccess)
                {
                    SuccessMessage = "Trabajo de impresión cancelado exitosamente";
                    _logger.LogInformation("Trabajo de impresión cancelado exitosamente: {JobId}", job.Id);
                    await LoadPrintJobsAsync();
                }
                else
                {
                    var errorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                    SetError($"Error al cancelar trabajo: {errorMessage}");
                    _logger.LogError("Error al cancelar trabajo de impresión {JobId}: {Error}", job.Id, errorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado al cancelar trabajo: {ex.Message}");
                _logger.LogError(ex, "Error inesperado al cancelar trabajo de impresión {JobId}", job.Id);
            }
        }

        private async Task RefreshPrintersAsync()
        {
            await LoadAvailablePrintersAsync();
        }

        // Métodos auxiliares
        private async Task LoadAvailablePrintersAsync()
        {
            try
            {
                ClearError();
                _logger.LogInformation("Cargando lista de impresoras disponibles");

                var result = await _printerConfiguration.ListAvailablePrintersAsync();

                if (result.IsSuccess)
                {
                    AvailablePrinters.Clear();
                    foreach (var printer in result.Value)
                    {
                        AvailablePrinters.Add(printer);
                    }

                    if (AvailablePrinters.Count > 0 && string.IsNullOrEmpty(SelectedPrinter))
                    {
                        SelectedPrinter = AvailablePrinters.First();
                    }

                    _logger.LogInformation("Cargadas {Count} impresoras disponibles", AvailablePrinters.Count);
                }
                else
                {
                    var errorMessage = string.Join(", ", result.Errors.Select(e => e.Message));
                    SetError($"Error al cargar impresoras: {errorMessage}");
                    _logger.LogError("Error al cargar impresoras disponibles: {Error}", errorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError($"Error inesperado al cargar impresoras: {ex.Message}");
                _logger.LogError(ex, "Error inesperado al cargar impresoras disponibles");
            }
        }

        private async Task LoadPrintJobsAsync()
        {
            try
            {
                var result = await _printJobQueue.GetPendingJobsAsync();

                if (result.IsSuccess)
                {
                    PrintJobs.Clear();
                    foreach (var job in result.Value)
                    {
                        PrintJobs.Add(job);
                    }
                }
                else
                {
                    _logger.LogWarning("Error al cargar trabajos de impresión pendientes: {Error}",
                        string.Join(", ", result.Errors.Select(e => e.Message)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al cargar trabajos de impresión");
            }
        }

        // Validaciones para comandos
        private bool CanPrintReceipt()
        {
            return !IsPrinting && !string.IsNullOrEmpty(SelectedPrinter) && CurrentSale != null;
        }

        private bool CanPrintInvoice()
        {
            return !IsPrinting && !string.IsNullOrEmpty(SelectedPrinter) && CurrentDteDocument != null;
        }

        private bool CanTestPrint()
        {
            return !IsPrinting && !string.IsNullOrEmpty(SelectedPrinter);
        }

        private bool CanCancelPrintJob(PrintJob? job)
        {
            return job != null && (job.Status == PrintJobStatus.Pending || job.Status == PrintJobStatus.Processing);
        }
    }
}