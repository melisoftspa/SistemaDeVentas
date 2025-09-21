using System.ComponentModel.DataAnnotations;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Core.Domain.Entities.Printer;

/// <summary>
/// Configuración para impresoras térmicas.
/// </summary>
public class ThermalPrinterSettings
{
    /// <summary>
    /// Modelo de la impresora térmica.
    /// </summary>
    [Required(ErrorMessage = "El modelo de la impresora es obligatorio.")]
    public PrinterModel Model { get; set; }

    /// <summary>
    /// Tipo de conexión de la impresora.
    /// </summary>
    [Required(ErrorMessage = "El tipo de conexión es obligatorio.")]
    public ConnectionType ConnectionType { get; set; }

    /// <summary>
    /// Puerto de conexión (COM para serial, IP para LAN, vacío para USB).
    /// </summary>
    public string? Port { get; set; }

    /// <summary>
    /// Velocidad de baudios para conexiones seriales.
    /// </summary>
    [Range(9600, 115200, ErrorMessage = "El baud rate debe estar entre 9600 y 115200.")]
    public int BaudRate { get; set; } = 9600;

    /// <summary>
    /// Timeout en milisegundos para operaciones de impresión.
    /// </summary>
    [Range(1000, 30000, ErrorMessage = "El timeout debe estar entre 1000 y 30000 milisegundos.")]
    public int TimeoutMilliseconds { get; set; } = 5000;

    /// <summary>
    /// Ancho del papel en caracteres (típicamente 32 o 48).
    /// </summary>
    [Range(16, 64, ErrorMessage = "El ancho del papel debe estar entre 16 y 64 caracteres.")]
    public int PaperWidth { get; set; } = 32;

    /// <summary>
    /// Nombre descriptivo de la impresora.
    /// </summary>
    [Required(ErrorMessage = "El nombre de la impresora es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Valida que la configuración sea correcta.
    /// </summary>
    /// <returns>Lista de errores de validación.</returns>
    public List<string> Validar()
    {
        var errores = new List<string>();

        if (ConnectionType == ConnectionType.Serial && string.IsNullOrWhiteSpace(Port))
            errores.Add("El puerto es obligatorio para conexiones seriales.");

        if (ConnectionType == ConnectionType.LAN && string.IsNullOrWhiteSpace(Port))
            errores.Add("La dirección IP es obligatoria para conexiones LAN.");

        if (ConnectionType == ConnectionType.USB && !string.IsNullOrWhiteSpace(Port))
            errores.Add("El puerto no debe especificarse para conexiones USB.");

        if (BaudRate < 9600 || BaudRate > 115200)
            errores.Add("El baud rate debe estar entre 9600 y 115200.");

        if (TimeoutMilliseconds < 1000 || TimeoutMilliseconds > 30000)
            errores.Add("El timeout debe estar entre 1000 y 30000 milisegundos.");

        if (PaperWidth < 16 || PaperWidth > 64)
            errores.Add("El ancho del papel debe estar entre 16 y 64 caracteres.");

        if (string.IsNullOrWhiteSpace(Name))
            errores.Add("El nombre de la impresora es obligatorio.");

        return errores;
    }
}