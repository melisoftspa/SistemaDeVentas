namespace SistemaDeVentas.Core.Domain.Enums;

/// <summary>
/// Enum para los estados de un trabajo de impresión.
/// </summary>
public enum PrintJobStatus
{
    /// <summary>
    /// Trabajo pendiente de procesamiento.
    /// </summary>
    Pending,

    /// <summary>
    /// Trabajo en proceso de impresión.
    /// </summary>
    Processing,

    /// <summary>
    /// Trabajo completado exitosamente.
    /// </summary>
    Completed,

    /// <summary>
    /// Trabajo fallido.
    /// </summary>
    Failed,

    /// <summary>
    /// Trabajo cancelado.
    /// </summary>
    Cancelled
}