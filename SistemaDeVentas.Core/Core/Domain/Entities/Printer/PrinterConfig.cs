using System.ComponentModel.DataAnnotations;

namespace SistemaDeVentas.Core.Domain.Entities.Printer;

/// <summary>
/// Configuración completa de una impresora térmica.
/// </summary>
public class PrinterConfig
{
    /// <summary>
    /// Identificador único de la impresora.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Configuración específica de la impresora térmica.
    /// </summary>
    [Required]
    public ThermalPrinterSettings Settings { get; set; } = new ThermalPrinterSettings();

    /// <summary>
    /// Valida que la configuración sea correcta.
    /// </summary>
    /// <returns>Lista de errores de validación.</returns>
    public List<string> Validate()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Id))
            errors.Add("El ID de la impresora es obligatorio.");

        if (Settings == null)
            errors.Add("La configuración de la impresora es obligatoria.");
        else
            errors.AddRange(Settings.Validar());

        return errors;
    }
}