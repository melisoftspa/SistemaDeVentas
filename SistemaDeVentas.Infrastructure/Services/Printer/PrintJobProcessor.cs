using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities.Printer;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Servicio en background que procesa la cola de trabajos de impresión.
/// </summary>
public class PrintJobProcessor : BackgroundService
{
    private readonly IPrintJobQueue _printJobQueue;
    private readonly ThermalPrinterService _thermalPrinterService;
    private readonly ILogger<PrintJobProcessor> _logger;

    public PrintJobProcessor(
        IPrintJobQueue printJobQueue,
        ThermalPrinterService thermalPrinterService,
        ILogger<PrintJobProcessor> logger)
    {
        _printJobQueue = printJobQueue;
        _thermalPrinterService = thermalPrinterService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PrintJobProcessor iniciado");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Intentar obtener el siguiente trabajo
                var job = await _printJobQueue.DequeueAsync();
                if (job != null)
                {
                    await ProcessJobAsync(job, stoppingToken);
                }
                else
                {
                    // Si no hay trabajos, esperar un poco antes de verificar nuevamente
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (TaskCanceledException)
            {
                // Servicio detenido
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el procesamiento de la cola de impresión");
                await Task.Delay(5000, stoppingToken); // Esperar más tiempo en caso de error
            }
        }

        _logger.LogInformation("PrintJobProcessor detenido");
    }

    private async Task ProcessJobAsync(PrintJob job, CancellationToken stoppingToken)
    {
        var startTime = DateTime.UtcNow;

        try
        {
            _logger.LogInformation("Procesando trabajo de impresión: {JobId}", job.Id);

            // Enviar datos a la impresora
            var printResult = await _thermalPrinterService.PrintDirectAsync(job.Data);

            if (printResult.IsSuccess)
            {
                var processingTime = DateTime.UtcNow - startTime;
                ((PrintJobQueue)_printJobQueue).MarkCompleted(job.Id, processingTime);
                _logger.LogInformation("Trabajo de impresión completado exitosamente: {JobId}", job.Id);
            }
            else
            {
                var errorMessage = printResult.Errors.First().Message;
                ((PrintJobQueue)_printJobQueue).MarkFailed(job.Id, errorMessage);
                _logger.LogWarning("Trabajo de impresión fallido: {JobId}, Error: {Error}", job.Id, errorMessage);
            }
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error inesperado: {ex.Message}";
            ((PrintJobQueue)_printJobQueue).MarkFailed(job.Id, errorMessage);
            _logger.LogError(ex, "Error al procesar trabajo de impresión: {JobId}", job.Id);
        }
    }
}