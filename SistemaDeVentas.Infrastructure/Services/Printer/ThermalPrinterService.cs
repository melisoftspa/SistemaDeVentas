using System;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;
using PrintJobPriority = SistemaDeVentas.Core.Domain.Enums.PrintJobPriority;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Servicio principal para impresoras térmicas con soporte ESC/POS.
/// </summary>
public class ThermalPrinterService : IThermalPrinterService
{
    private readonly IPrinterConfiguration _printerConfiguration;
    private readonly IPrintJobQueue _printJobQueue;
    private readonly ILogger<ThermalPrinterService> _logger;
    private IThermalPrinter? _currentPrinter;
    private PrinterConfig? _currentConfig;

    public ThermalPrinterService(
        IPrinterConfiguration printerConfiguration,
        IPrintJobQueue printJobQueue,
        ILogger<ThermalPrinterService> logger)
    {
        _printerConfiguration = printerConfiguration;
        _printJobQueue = printJobQueue;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> ConnectAsync(string printerName)
    {
        try
        {
            _logger.LogInformation("Intentando conectar a la impresora: {PrinterName}", printerName);

            // Obtener configuración de la impresora
            var configResult = await _printerConfiguration.GetConfigurationAsync(printerName);
            if (configResult.IsFailed)
            {
                _logger.LogError("Error al obtener configuración para {PrinterName}: {Error}",
                    printerName, configResult.Errors.First().Message);
                return Result.Fail(configResult.Errors.First().Message);
            }

            _currentConfig = configResult.Value;

            // Crear instancia del printer específico según el modelo
            _currentPrinter = CreatePrinterInstance(_currentConfig);

            // Intentar conectar
            var connectResult = await _currentPrinter.ConnectAsync();
            if (connectResult.IsFailed)
            {
                _logger.LogError("Error al conectar a {PrinterName}: {Error}",
                    printerName, connectResult.Errors.First().Message);
                return connectResult;
            }

            _logger.LogInformation("Conexión exitosa a la impresora: {PrinterName}", printerName);
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al conectar a {PrinterName}", printerName);
            return Result.Fail($"Error al conectar: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> DisconnectAsync()
    {
        if (_currentPrinter == null)
        {
            return Result.Ok(true); // Ya desconectado
        }

        try
        {
            var result = await _currentPrinter.DisconnectAsync();
            _currentPrinter = null;
            _currentConfig = null;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al desconectar la impresora");
            return Result.Fail($"Error al desconectar: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> PrintAsync(string data, PrintJobPriority priority = PrintJobPriority.Normal)
    {
        try
        {
            // Convertir string a bytes usando UTF-8
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            return await _printJobQueue.EnqueueAsync(bytes, priority);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al encolar datos de texto para impresión");
            return Result.Fail($"Error al encolar impresión: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> PrintRawAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal)
    {
        try
        {
            return await _printJobQueue.EnqueueAsync(data, priority);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al encolar datos binarios para impresión");
            return Result.Fail($"Error al encolar impresión: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<string>> GetStatusAsync()
    {
        if (_currentPrinter == null)
        {
            return Result.Ok("Desconectado");
        }

        try
        {
            return await _currentPrinter.GetStatusAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado de la impresora");
            return Result.Fail($"Error al obtener estado: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public Task<Result<bool>> IsCompatibleAsync(string printerModel)
    {
        // Por ahora, solo compatible con RPT008
        var isCompatible = Enum.TryParse<PrinterModel>(printerModel, out var model) &&
                          (model == PrinterModel.RPT008 || model == PrinterModel.Generic);

        return Task.FromResult(isCompatible
            ? Result.Ok(true)
            : Result.Fail($"Modelo de impresora no compatible: {printerModel}"));
    }

    /// <inheritdoc/>
    public async Task<Result<byte[]>> FormatReceiptDataAsync(Sale sale)
    {
        if (_currentPrinter == null)
        {
            return Result.Fail("No hay impresora conectada");
        }

        try
        {
            return await _currentPrinter.FormatReceiptDataAsync(sale);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al formatear datos de boleta para venta {SaleId}", sale.Id);
            return Result.Fail($"Error al formatear boleta: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<byte[]>> FormatInvoiceDataAsync(DteDocument dteDocument, string qrCodeData)
    {
        if (_currentPrinter == null)
        {
            return Result.Fail("No hay impresora conectada");
        }

        try
        {
            return await _currentPrinter.FormatInvoiceDataAsync(dteDocument, qrCodeData);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al formatear datos de factura DTE");
            return Result.Fail($"Error al formatear factura: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> PrintReceiptAsync(Sale sale)
    {
        var formatResult = await FormatReceiptDataAsync(sale);
        if (formatResult.IsFailed)
        {
            return Result.Fail(formatResult.Errors.First().Message);
        }

        return await PrintRawAsync(formatResult.Value, PrintJobPriority.Normal);
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> PrintInvoiceAsync(DteDocument dteDocument, string qrCodeData)
    {
        var formatResult = await FormatInvoiceDataAsync(dteDocument, qrCodeData);
        if (formatResult.IsFailed)
        {
            return Result.Fail(formatResult.Errors.First().Message);
        }

        return await PrintRawAsync(formatResult.Value, PrintJobPriority.High);
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> CutPaperAsync()
    {
        if (_currentPrinter == null)
        {
            return Result.Fail("No hay impresora conectada");
        }

        try
        {
            return await _currentPrinter.CutPaperAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cortar papel");
            return Result.Fail($"Error al cortar papel: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> InitializeAsync()
    {
        if (_currentPrinter == null)
        {
            return Result.Fail("No hay impresora conectada");
        }

        try
        {
            return await _currentPrinter.InitializeAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al inicializar impresora");
            return Result.Fail($"Error al inicializar: {ex.Message}");
        }
    }

    /// <summary>
    /// Imprime datos binarios directamente sin encolar (usado por el procesador de cola).
    /// </summary>
    internal async Task<Result<bool>> PrintDirectAsync(byte[] data)
    {
        if (_currentPrinter == null)
        {
            return Result.Fail("No hay impresora conectada");
        }

        try
        {
            return await _currentPrinter.PrintRawAsync(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al imprimir datos binarios directamente");
            return Result.Fail($"Error al imprimir: {ex.Message}");
        }
    }

    /// <summary>
    /// Crea una instancia del printer específico según la configuración.
    /// </summary>
    private IThermalPrinter CreatePrinterInstance(PrinterConfig config)
    {
        return config.Settings.Model switch
        {
            PrinterModel.RPT008 => new Rpt008ThermalPrinter(config.Settings, _logger),
            PrinterModel.Generic => new Rpt008ThermalPrinter(config.Settings, _logger), // Por defecto usar RPT008
            _ => throw new NotSupportedException($"Modelo de impresora no soportado: {config.Settings.Model}")
        };
    }
}