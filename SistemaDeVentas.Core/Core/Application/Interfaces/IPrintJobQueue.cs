using System.Threading.Tasks;
using FluentResults;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Core.Application.Interfaces;

/// <summary>
/// Interfaz para la gestión de una cola de trabajos de impresión.
/// </summary>
public interface IPrintJobQueue
{
    /// <summary>
    /// Encola un trabajo de impresión con la prioridad especificada.
    /// </summary>
    /// <param name="data">Datos a imprimir.</param>
    /// <param name="priority">Prioridad del trabajo.</param>
    /// <returns>ID del trabajo encolado.</returns>
    Task<Result<Guid>> EnqueueAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal);

    /// <summary>
    /// Desencola el siguiente trabajo de impresión según prioridad y timestamp.
    /// </summary>
    /// <returns>Trabajo de impresión o null si la cola está vacía.</returns>
    Task<PrintJob?> DequeueAsync();

    /// <summary>
    /// Cancela un trabajo de impresión pendiente.
    /// </summary>
    /// <param name="jobId">ID del trabajo a cancelar.</param>
    /// <returns>Resultado de la operación.</returns>
    Task<Result<bool>> CancelAsync(Guid jobId);

    /// <summary>
    /// Obtiene el estado de un trabajo de impresión.
    /// </summary>
    /// <param name="jobId">ID del trabajo.</param>
    /// <returns>Estado del trabajo.</returns>
    Task<Result<PrintJobStatus>> GetStatusAsync(Guid jobId);

    /// <summary>
    /// Obtiene todos los trabajos pendientes.
    /// </summary>
    /// <returns>Lista de trabajos pendientes.</returns>
    Task<Result<IEnumerable<PrintJob>>> GetPendingJobsAsync();

    /// <summary>
    /// Obtiene métricas de rendimiento de la cola.
    /// </summary>
    /// <returns>Métricas de la cola.</returns>
    Task<Result<PrintQueueMetrics>> GetMetricsAsync();

    /// <summary>
    /// Limpia trabajos completados o fallidos antiguos.
    /// </summary>
    /// <param name="olderThan">Trabajos más antiguos que esta fecha.</param>
    /// <returns>Número de trabajos limpiados.</returns>
    Task<Result<int>> CleanupAsync(DateTime olderThan);
}

/// <summary>
/// Métricas de rendimiento de la cola de impresión.
/// </summary>
public class PrintQueueMetrics
{
    /// <summary>
    /// Número total de trabajos en la cola.
    /// </summary>
    public int TotalJobs { get; set; }

    /// <summary>
    /// Número de trabajos pendientes.
    /// </summary>
    public int PendingJobs { get; set; }

    /// <summary>
    /// Número de trabajos en procesamiento.
    /// </summary>
    public int ProcessingJobs { get; set; }

    /// <summary>
    /// Número de trabajos completados.
    /// </summary>
    public int CompletedJobs { get; set; }

    /// <summary>
    /// Número de trabajos fallidos.
    /// </summary>
    public int FailedJobs { get; set; }

    /// <summary>
    /// Número de trabajos cancelados.
    /// </summary>
    public int CancelledJobs { get; set; }

    /// <summary>
    /// Tiempo promedio de procesamiento en segundos.
    /// </summary>
    public double AverageProcessingTime { get; set; }

    /// <summary>
    /// Tasa de éxito (trabajos completados / total procesados).
    /// </summary>
    public double SuccessRate { get; set; }
}