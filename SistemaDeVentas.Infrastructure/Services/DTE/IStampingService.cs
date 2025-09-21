using System.Drawing;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Interfaz para servicios de timbrado electrónico de DTE.
/// </summary>
public interface IStampingService
{
    /// <summary>
    /// Solicita timbrado electrónico para un DTE firmado.
    /// </summary>
    /// <param name="signedXmlDocument">El documento XML firmado.</param>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <param name="ambiente">Ambiente del SII (certificación=1, producción=0).</param>
    /// <returns>El documento con timbre electrónico.</returns>
    Task<XDocument> RequestElectronicStamp(XDocument signedXmlDocument, string rutEmisor, int ambiente = 0);

    /// <summary>
    /// Verifica el timbre electrónico de un DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML con timbre.</param>
    /// <returns>True si el timbre es válido.</returns>
    bool VerifyElectronicStamp(XDocument xmlDocument);

    /// <summary>
    /// Extrae el TED (Timbre Electrónico del Documento) de un DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML.</param>
    /// <returns>El elemento TED o null si no existe.</returns>
    XElement? ExtractTED(XDocument xmlDocument);

    /// <summary>
    /// Genera el código de barras PDF417 para el TED.
    /// </summary>
    /// <param name="ddElement">El elemento DD del TED.</param>
    /// <returns>Los bytes comprimidos del TED para el PDF417.</returns>
    byte[] GeneratePdf417Barcode(XElement ddElement);

    /// <summary>
    /// Genera el código de barras PDF417 como imagen para impresión.
    /// </summary>
    /// <param name="xmlDocument">El documento XML con TED.</param>
    /// <returns>Bitmap del código PDF417.</returns>
    Bitmap GeneratePdf417Image(XDocument xmlDocument);
}