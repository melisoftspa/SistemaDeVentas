using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Interfaz para servicios de firma digital de documentos XML DTE.
/// </summary>
public interface IDigitalSignatureService
{
    /// <summary>
    /// Firma digitalmente un documento XML DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a firmar.</param>
    /// <param name="certificate">El certificado para la firma.</param>
    /// <returns>El documento XML firmado.</returns>
    XDocument SignXmlDocument(XDocument xmlDocument, X509Certificate2 certificate);

    /// <summary>
    /// Verifica la firma digital de un documento XML DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a verificar.</param>
    /// <returns>True si la firma es válida.</returns>
    bool VerifyXmlSignature(XDocument xmlDocument);

    /// <summary>
    /// Calcula el digest SHA-256 de los datos.
    /// </summary>
    /// <param name="data">Los datos para calcular el digest.</param>
    /// <returns>El digest en base64.</returns>
    string CalculateSha256Digest(string data);

    /// <summary>
    /// Firma datos con RSA-SHA256.
    /// </summary>
    /// <param name="data">Los datos a firmar.</param>
    /// <param name="certificate">El certificado con clave privada.</param>
    /// <returns>La firma en base64.</returns>
    string SignDataWithRsaSha256(string data, X509Certificate2 certificate);
}