using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Interfaz para el servicio de autenticación con el SII.
/// Maneja la obtención de semillas y tokens de autenticación.
/// </summary>
public interface ISiiAuthenticationService
{
    /// <summary>
    /// Obtiene una semilla de autenticación del SII.
    /// </summary>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>La semilla en formato XML.</returns>
    Task<string> GetSeedAsync(int ambiente = 0);

    /// <summary>
    /// Solicita un token de autenticación al SII usando una semilla firmada.
    /// </summary>
    /// <param name="signedSeedXml">La semilla firmada en formato XML.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El token de autenticación.</returns>
    Task<string> GetTokenAsync(string signedSeedXml, int ambiente = 0);

    /// <summary>
    /// Verifica si un token es válido.
    /// </summary>
    /// <param name="token">El token a verificar.</param>
    /// <returns>True si el token es válido.</returns>
    Task<bool> ValidateTokenAsync(string token);
}