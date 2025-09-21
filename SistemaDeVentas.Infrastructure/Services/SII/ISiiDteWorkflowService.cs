using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Interfaz para el servicio de workflow completo de DTE con SII.
/// Maneja autenticación, envío y consulta de estados.
/// </summary>
public interface ISiiDteWorkflowService
{
    /// <summary>
    /// Envía un DTE al SII y opcionalmente consulta su estado.
    /// </summary>
    /// <param name="dteEnvelope">El envelope XML del DTE a enviar.</param>
    /// <param name="signedSeedXml">La semilla firmada para autenticación.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <param name="queryStatus">Si se debe consultar el estado después del envío.</param>
    /// <returns>Resultado del envío con TrackID y estado si se consultó.</returns>
    Task<SiiSubmissionResult> SubmitAndQueryDteAsync(XDocument dteEnvelope, string signedSeedXml, int ambiente = 0, bool queryStatus = false);

    /// <summary>
    /// Consulta el estado de un envío por TrackID.
    /// </summary>
    /// <param name="trackId">El TrackID del envío.</param>
    /// <param name="signedSeedXml">La semilla firmada para autenticación.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El estado del envío.</returns>
    Task<SiiSubmissionStatus> QuerySubmissionStatusAsync(string trackId, string signedSeedXml, int ambiente = 0);
}

/// <summary>
/// Resultado de un envío al SII.
/// </summary>
public class SiiSubmissionResult
{
    public string TrackId { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public SiiSubmissionStatus Status { get; set; }
}