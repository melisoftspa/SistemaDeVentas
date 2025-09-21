using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Application.Services.DTE;

/// <summary>
/// Interfaz completa para procesamiento de DTE: construcción, firma y timbrado.
/// </summary>
public interface IDteProcessingService : IDteBuilderService
{
    /// <summary>
    /// Construye, firma y timbra un documento DTE completo usando configuración.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a procesar.</param>
    /// <param name="tipoDocumento">Tipo de documento DTE.</param>
    /// <returns>El documento XML completo, firmado y timbrado.</returns>
    Task<XDocument> BuildSignAndStampDteWithSettings(DteDocument dteDocument, int tipoDocumento);

    /// <summary>
    /// Construye, firma y timbra un documento DTE completo.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a procesar.</param>
    /// <param name="certificate">El certificado para firma digital.</param>
    /// <param name="rutEmisor">RUT del emisor para timbrado.</param>
    /// <param name="ambiente">Ambiente del SII (0=producción, 1=certificación).</param>
    /// <returns>El documento XML completo, firmado y timbrado.</returns>
    Task<XDocument> BuildSignAndStampDte(DteDocument dteDocument, X509Certificate2 certificate, string rutEmisor, int ambiente = 0);

    /// <summary>
    /// Construye y firma un documento DTE.
    /// </summary>
    /// <param name="dteDocument">El documento DTE a procesar.</param>
    /// <param name="certificate">El certificado para firma digital.</param>
    /// <returns>El documento XML firmado.</returns>
    XDocument BuildAndSignDte(DteDocument dteDocument, X509Certificate2 certificate);

    /// <summary>
    /// Timbra un documento DTE ya firmado.
    /// </summary>
    /// <param name="signedXmlDocument">El documento XML firmado.</param>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <param name="ambiente">Ambiente del SII.</param>
    /// <returns>El documento XML timbrado.</returns>
    Task<XDocument> StampSignedDte(XDocument signedXmlDocument, string rutEmisor, int ambiente = 0);

    /// <summary>
    /// Valida un documento DTE completo (estructura, firma y timbre).
    /// </summary>
    /// <param name="xmlDocument">El documento XML a validar.</param>
    /// <returns>True si es válido.</returns>
    bool ValidateCompleteDte(XDocument xmlDocument);
}