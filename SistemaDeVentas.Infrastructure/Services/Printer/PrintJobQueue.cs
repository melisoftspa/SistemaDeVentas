using System.Collections.Concurrent;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Implementación de cola de trabajos de impresión con gestión en memoria.
/// </summary>
public class PrintJobQueue : IPrintJobQueue
{
    private readonly ILogger<PrintJobQueue> _logger;
    private readonly ConcurrentDictionary<Guid, PrintJob> _jobs = new();
    private readonly PriorityQueue<PrintJob, (PrintJobPriority Priority, DateTime CreatedAt)> _queue = new(Comparer<(PrintJobPriority Priority, DateTime CreatedAt)>.Create((a, b) =>
    {
        // Mayor prioridad primero (High > Normal > Low)
        int priorityCompare = b.Priority.CompareTo(a.Priority);
        if (priorityCompare != 0) return priorityCompare;
        // Si misma prioridad, primero el más antiguo (timestamp menor)
        return a.CreatedAt.CompareTo(b.CreatedAt);
    }));

    // Métricas
    private readonly ConcurrentDictionary<PrintJobStatus, int> _statusCounts = new();
    private double _totalProcessingTime;
    private int _processedJobsCount;

    public PrintJobQueue(ILogger<PrintJobQueue> logger)
    {
        _logger = logger;
        // Inicializar contadores
        foreach (PrintJobStatus status in Enum.GetValues(typeof(PrintJobStatus)))
        {
            _statusCounts[status] = 0;
        }
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> EnqueueAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal)
    {
        try
        {
            var job = new PrintJob
            {
                Data = data,
                Priority = priority,
                Status = PrintJobStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _jobs[job.Id] = job;
            _queue.Enqueue(job, (job.Priority, job.CreatedAt));
            UpdateStatusCount(PrintJobStatus.Pending, 1);

            _logger.LogInformation("Trabajo de impresión encolado: {JobId}, Prioridad: {Priority}, Tamaño: {Size} bytes",
                job.Id, priority, data.Length);

            return Result.Ok(job.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al encolar trabajo de impresión");
            return Result.Fail($"Error al encolar trabajo: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<PrintJob?> DequeueAsync()
    {
        try
        {
            if (_queue.TryDequeue(out var job, out _))
            {
                if (_jobs.TryGetValue(job.Id, out var storedJob))
                {
                    storedJob.UpdateStatus(PrintJobStatus.Processing);
                    UpdateStatusCount(PrintJobStatus.Pending, -1);
                    UpdateStatusCount(PrintJobStatus.Processing, 1);

                    _logger.LogInformation("Trabajo de impresión desencolado: {JobId}", job.Id);
                    return storedJob;
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al desencolar trabajo de impresión");
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<Result<bool>> CancelAsync(Guid jobId)
    {
        try
        {
            if (_jobs.TryGetValue(jobId, out var job))
            {
                if (job.Status == PrintJobStatus.Pending)
                {
                    job.UpdateStatus(PrintJobStatus.Cancelled);
                    UpdateStatusCount(PrintJobStatus.Pending, -1);
                    UpdateStatusCount(PrintJobStatus.Cancelled, 1);

                    _logger.LogInformation("Trabajo de impresión cancelado: {JobId}", jobId);
                    return Result.Ok(true);
                }
                else
                {
                    return Result.Fail("Solo se pueden cancelar trabajos pendientes");
                }
            }

            return Result.Fail("Trabajo no encontrado");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cancelar trabajo de impresión {JobId}", jobId);
            return Result.Fail($"Error al cancelar trabajo: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<PrintJobStatus>> GetStatusAsync(Guid jobId)
    {
        try
        {
            if (_jobs.TryGetValue(jobId, out var job))
            {
                return Result.Ok(job.Status);
            }

            return Result.Fail("Trabajo no encontrado");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado del trabajo {JobId}", jobId);
            return Result.Fail($"Error al obtener estado: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<IEnumerable<PrintJob>>> GetPendingJobsAsync()
    {
        try
        {
            var pendingJobs = _jobs.Values.Where(j => j.Status == PrintJobStatus.Pending).ToList();
            return Result.Ok(pendingJobs.AsEnumerable());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener trabajos pendientes");
            return Result.Fail($"Error al obtener trabajos pendientes: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<PrintQueueMetrics>> GetMetricsAsync()
    {
        try
        {
            var metrics = new PrintQueueMetrics
            {
                TotalJobs = _jobs.Count,
                PendingJobs = _statusCounts[PrintJobStatus.Pending],
                ProcessingJobs = _statusCounts[PrintJobStatus.Processing],
                CompletedJobs = _statusCounts[PrintJobStatus.Completed],
                FailedJobs = _statusCounts[PrintJobStatus.Failed],
                CancelledJobs = _statusCounts[PrintJobStatus.Cancelled],
                AverageProcessingTime = _processedJobsCount > 0 ? _totalProcessingTime / _processedJobsCount : 0,
                SuccessRate = CalculateSuccessRate()
            };

            return Result.Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener métricas de la cola");
            return Result.Fail($"Error al obtener métricas: {ex.Message}");
        }
    }

    /// <inheritdoc/>
    public async Task<Result<int>> CleanupAsync(DateTime olderThan)
    {
        try
        {
            var jobsToRemove = _jobs.Values
                .Where(j => (j.Status == PrintJobStatus.Completed || j.Status == PrintJobStatus.Failed || j.Status == PrintJobStatus.Cancelled)
                         && j.UpdatedAt < olderThan)
                .ToList();

            foreach (var job in jobsToRemove)
            {
                _jobs.TryRemove(job.Id, out _);
                UpdateStatusCount(job.Status, -1);
            }

            _logger.LogInformation("Limpiados {Count} trabajos antiguos", jobsToRemove.Count);
            return Result.Ok(jobsToRemove.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al limpiar trabajos antiguos");
            return Result.Fail($"Error al limpiar trabajos: {ex.Message}");
        }
    }

    /// <summary>
    /// Marca un trabajo como completado y registra el tiempo de procesamiento.
    /// </summary>
    public void MarkCompleted(Guid jobId, TimeSpan processingTime)
    {
        if (_jobs.TryGetValue(jobId, out var job))
        {
            job.UpdateStatus(PrintJobStatus.Completed);
            UpdateStatusCount(PrintJobStatus.Processing, -1);
            UpdateStatusCount(PrintJobStatus.Completed, 1);
            _totalProcessingTime += processingTime.TotalSeconds;
            _processedJobsCount++;

            _logger.LogInformation("Trabajo completado: {JobId}, Tiempo: {Time}s", jobId, processingTime.TotalSeconds);
        }
    }

    /// <summary>
    /// Marca un trabajo como fallido y lo reencola si puede ser reintentado.
    /// </summary>
    public void MarkFailed(Guid jobId, string errorMessage)
    {
        if (_jobs.TryGetValue(jobId, out var job))
        {
            if (job.CanRetry())
            {
                job.IncrementRetry();
                job.UpdateStatus(PrintJobStatus.Pending, errorMessage);
                _queue.Enqueue(job, (job.Priority, job.CreatedAt));
                UpdateStatusCount(PrintJobStatus.Processing, -1);
                UpdateStatusCount(PrintJobStatus.Pending, 1);

                _logger.LogWarning("Trabajo reencolado por reintento: {JobId}, Intento: {Retry}", jobId, job.RetryCount);
            }
            else
            {
                job.UpdateStatus(PrintJobStatus.Failed, errorMessage);
                UpdateStatusCount(PrintJobStatus.Processing, -1);
                UpdateStatusCount(PrintJobStatus.Failed, 1);

                _logger.LogError("Trabajo fallido definitivamente: {JobId}, Error: {Error}", jobId, errorMessage);
            }
        }
    }

    private void UpdateStatusCount(PrintJobStatus status, int delta)
    {
        _statusCounts.AddOrUpdate(status, delta, (key, oldValue) => oldValue + delta);
    }

    private double CalculateSuccessRate()
    {
        int totalProcessed = _statusCounts[PrintJobStatus.Completed] + _statusCounts[PrintJobStatus.Failed];
        return totalProcessed > 0 ? (double)_statusCounts[PrintJobStatus.Completed] / totalProcessed : 0;
    }
}