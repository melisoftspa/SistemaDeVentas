using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Servicio de workflow completo para DTE con SII.
/// Coordina autenticación, envío y consulta de estados.
/// </summary>
public class SiiDteWorkflowService : ISiiDteWorkflowService
{
    private readonly ISiiAuthenticationService _authService;
    private readonly ISiiDteSubmissionService _submissionService;
    private readonly ISiiStatusQueryService _statusService;

    public SiiDteWorkflowService(
        ISiiAuthenticationService authService,
        ISiiDteSubmissionService submissionService,
        ISiiStatusQueryService statusService)
    {
        _authService = authService;
        _submissionService = submissionService;
        _statusService = statusService;
    }

    /// <summary>
    /// Envía un DTE al SII y opcionalmente consulta su estado.
    /// </summary>
    public async Task<SiiSubmissionResult> SubmitAndQueryDteAsync(XDocument dteEnvelope, string signedSeedXml, int ambiente = 0, bool queryStatus = false)
    {
        try
        {
            // Obtener token de autenticación
            var token = await _authService.GetTokenAsync(signedSeedXml, ambiente);

            // Enviar DTE
            var trackId = await _submissionService.SubmitDteAsync(dteEnvelope, token, ambiente);

            var result = new SiiSubmissionResult
            {
                TrackId = trackId,
                IsSuccess = true,
                Message = "DTE enviado exitosamente"
            };

            // Consultar estado si se solicita
            if (queryStatus)
            {
                result.Status = await QuerySubmissionStatusAsync(trackId, signedSeedXml, ambiente);
            }

            return result;
        }
        catch (Exception ex)
        {
            return new SiiSubmissionResult
            {
                IsSuccess = false,
                Message = $"Error en el envío: {ex.Message}"
            };
        }
    }

    /// <summary>
    /// Consulta el estado de un envío por TrackID.
    /// </summary>
    public async Task<SiiSubmissionStatus> QuerySubmissionStatusAsync(string trackId, string signedSeedXml, int ambiente = 0)
    {
        try
        {
            // Obtener token de autenticación
            var token = await _authService.GetTokenAsync(signedSeedXml, ambiente);

            // Consultar estado
            var status = await _statusService.QueryStatusAsync(trackId, token, ambiente);

            return status;
        }
        catch (Exception ex)
        {
            return new SiiSubmissionStatus
            {
                TrackId = trackId,
                Estado = "ERROR",
                Detalle = ex.Message,
                IsSuccess = false
            };
        }
    }
}