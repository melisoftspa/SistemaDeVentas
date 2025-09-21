using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Interfaz para el servicio de envío de DTE al SII.
/// </summary>
public interface ISiiDteSubmissionService
{
    /// <summary>
    /// Envía un lote de DTE al SII para procesamiento.
    /// </summary>
    /// <param name="dteEnvelope">El envelope XML con los DTE a enviar.</param>
    /// <param name="token">Token de autenticación.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El track ID del envío.</returns>
    Task<string> SubmitDteAsync(XDocument dteEnvelope, string token, int ambiente = 0);

    /// <summary>
    /// Envía un DTE individual al SII.
    /// </summary>
    /// <param name="dteXml">El XML del DTE.</param>
    /// <param name="token">Token de autenticación.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El track ID del envío.</returns>
    Task<string> SubmitSingleDteAsync(string dteXml, string token, int ambiente = 0);
}