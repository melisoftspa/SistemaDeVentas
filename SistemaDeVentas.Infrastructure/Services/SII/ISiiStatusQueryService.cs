using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Interfaz para consulta de estados de envíos al SII.
/// </summary>
public interface ISiiStatusQueryService
{
    /// <summary>
    /// Consulta el estado de un envío por TrackID.
    /// </summary>
    /// <param name="trackId">El TrackID del envío.</param>
    /// <param name="token">Token de autenticación.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El estado del envío.</returns>
    Task<SiiSubmissionStatus> QueryStatusAsync(string trackId, string token, int ambiente = 0);
}

/// <summary>
/// Representa el estado de un envío al SII.
/// </summary>
public class SiiSubmissionStatus
{
    public string TrackId { get; set; }
    public string Estado { get; set; }
    public string Detalle { get; set; }
    public bool IsSuccess { get; set; }
}