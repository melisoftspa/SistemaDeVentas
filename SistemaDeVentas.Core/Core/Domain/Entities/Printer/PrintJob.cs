using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Core.Domain.Entities.Printer;

/// <summary>
/// Representa un trabajo de impresión para una impresora térmica.
/// </summary>
public class PrintJob
{
    /// <summary>
    /// Identificador único del trabajo de impresión.
    /// </summary>
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Datos a imprimir (comando ESC/POS o texto plano).
    /// </summary>
    [Required(ErrorMessage = "Los datos de impresión son obligatorios.")]
    public byte[] Data { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Prioridad del trabajo.
    /// </summary>
    [Required]
    public PrintJobPriority Priority { get; set; } = PrintJobPriority.Normal;

    /// <summary>
    /// Timestamp de creación del trabajo.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Estado actual del trabajo de impresión.
    /// </summary>
    [Required]
    public PrintJobStatus Status { get; set; } = PrintJobStatus.Pending;

    /// <summary>
    /// Timestamp de la última actualización del estado.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Mensaje de error si el trabajo falló.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Número de reintentos realizados.
    /// </summary>
    [Range(0, 5, ErrorMessage = "El número de reintentos no puede exceder 5.")]
    public int RetryCount { get; set; } = 0;

    /// <summary>
    /// Tamaño de los datos en bytes.
    /// </summary>
    public int DataSize => Data?.Length ?? 0;

    /// <summary>
    /// Actualiza el estado del trabajo.
    /// </summary>
    /// <param name="newStatus">Nuevo estado.</param>
    /// <param name="errorMessage">Mensaje de error (opcional).</param>
    public void UpdateStatus(PrintJobStatus newStatus, string? errorMessage = null)
    {
        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;

        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// Incrementa el contador de reintentos.
    /// </summary>
    public void IncrementRetry()
    {
        RetryCount++;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Verifica si el trabajo puede ser reintentado.
    /// </summary>
    /// <returns>True si puede ser reintentado.</returns>
    public bool CanRetry() => RetryCount < 5;
}