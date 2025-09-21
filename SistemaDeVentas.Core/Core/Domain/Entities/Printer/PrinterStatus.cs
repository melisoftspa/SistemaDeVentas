namespace SistemaDeVentas.Core.Domain.Entities.Printer;

/// <summary>
/// Enum para los estados de una impresora térmica.
/// </summary>
public enum PrinterStatus
{
    /// <summary>
    /// Impresora conectada y operativa.
    /// </summary>
    Connected,

    /// <summary>
    /// Impresora desconectada.
    /// </summary>
    Disconnected,

    /// <summary>
    /// Error en la impresora.
    /// </summary>
    Error,

    /// <summary>
    /// Impresora sin papel.
    /// </summary>
    OutOfPaper,

    /// <summary>
    /// Impresora ocupada.
    /// </summary>
    Busy
}