using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Interfaz para validación de DTE contra estándares del SII.
/// </summary>
public interface ISiiValidationService
{
    /// <summary>
    /// Valida un DTE contra los esquemas XML del SII.
    /// </summary>
    /// <param name="dteXml">El XML del DTE a validar.</param>
    /// <returns>Resultado de la validación.</returns>
    Task<SiiValidationResult> ValidateDteAsync(string dteXml);

    /// <summary>
    /// Valida un DTE contra los esquemas XML del SII.
    /// </summary>
    /// <param name="dteDocument">El documento XDocument del DTE.</param>
    /// <returns>Resultado de la validación.</returns>
    Task<SiiValidationResult> ValidateDteAsync(XDocument dteDocument);

    /// <summary>
    /// Valida reglas de negocio específicas del SII.
    /// </summary>
    /// <param name="dteXml">El XML del DTE.</param>
    /// <returns>Resultado de la validación de reglas.</returns>
    Task<SiiValidationResult> ValidateBusinessRulesAsync(string dteXml);
}

/// <summary>
/// Resultado de validación del SII.
/// </summary>
public class SiiValidationResult
{
    public bool IsValid { get; set; }
    public string[] Errors { get; set; } = Array.Empty<string>();
    public string[] Warnings { get; set; } = Array.Empty<string>();
}