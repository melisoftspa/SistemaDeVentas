using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio para firma digital de documentos XML DTE usando BouncyCastle.
/// Implementa el estándar XMLDSig compatible con LibreDTE.
/// </summary>
public class DigitalSignatureService : IDigitalSignatureService
{
    private readonly ICertificateService _certificateService;

    public DigitalSignatureService(ICertificateService certificateService)
    {
        _certificateService = certificateService;
    }

    /// <summary>
    /// Firma digitalmente un documento XML DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a firmar.</param>
    /// <param name="certificate">El certificado para la firma.</param>
    /// <returns>El documento XML firmado.</returns>
    public XDocument SignXmlDocument(XDocument xmlDocument, X509Certificate2 certificate)
    {
        if (!_certificateService.ValidateCertificateForSigning(certificate))
        {
            throw new InvalidOperationException("El certificado no es válido para firma digital.");
        }

        // Crear una copia del documento para no modificar el original
        var signedDocument = new XDocument(xmlDocument);

        // Preparar el documento XML para firma
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(signedDocument.CreateReader());

        // Crear el objeto SignedXml
        var signedXml = new SignedXml(xmlDoc);
        signedXml.SigningKey = _certificateService.GetPrivateKey(certificate);

        // Crear referencia al documento completo
        var reference = new Reference("");
        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
        reference.AddTransform(new XmlDsigC14NTransform());
        reference.DigestMethod = SignedXml.XmlDsigSHA256Url;

        signedXml.AddReference(reference);

        // Agregar información del certificado
        var keyInfo = new KeyInfo();
        keyInfo.AddClause(new KeyInfoX509Data(certificate));
        signedXml.KeyInfo = keyInfo;

        // Configurar algoritmo de firma
        signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;

        // Calcular la firma
        signedXml.ComputeSignature();

        // Obtener el elemento de firma
        var signatureElement = signedXml.GetXml();

        // Agregar la firma al documento
        var documentoElement = xmlDoc.DocumentElement;
        if (documentoElement != null)
        {
            documentoElement.AppendChild(xmlDoc.ImportNode(signatureElement, true));
        }

        // Convertir de vuelta a XDocument
        using var reader = new XmlNodeReader(xmlDoc);
        var result = XDocument.Load(reader);

        return result;
    }

    /// <summary>
    /// Verifica la firma digital de un documento XML DTE.
    /// </summary>
    /// <param name="xmlDocument">El documento XML a verificar.</param>
    /// <returns>True si la firma es válida.</returns>
    public bool VerifyXmlSignature(XDocument xmlDocument)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlDocument.CreateReader());

        var signedXml = new SignedXml(xmlDoc);

        // Buscar el elemento Signature
        var signatureElement = xmlDoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)
            .Cast<XmlElement>()
            .FirstOrDefault();

        if (signatureElement == null)
        {
            return false;
        }

        signedXml.LoadXml(signatureElement);

        // Verificar la firma
        return signedXml.CheckSignature();
    }

    /// <summary>
    /// Calcula el digest SHA-256 de los datos.
    /// </summary>
    /// <param name="data">Los datos para calcular el digest.</param>
    /// <returns>El digest en base64.</returns>
    public string CalculateSha256Digest(string data)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(data);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Firma datos con RSA-SHA256 usando BouncyCastle.
    /// </summary>
    /// <param name="data">Los datos a firmar.</param>
    /// <param name="certificate">El certificado con clave privada.</param>
    /// <returns>La firma en base64.</returns>
    public string SignDataWithRsaSha256(string data, X509Certificate2 certificate)
    {
        var rsa = _certificateService.GetPrivateKey(certificate);
        var privateKey = CertificateService.ConvertToBouncyCastle(rsa);

        // Crear el signer
        var signer = SignerUtilities.GetSigner("SHA256withRSA");
        signer.Init(true, privateKey);

        // Calcular hash SHA-256
        var dataBytes = Encoding.UTF8.GetBytes(data);
        signer.BlockUpdate(dataBytes, 0, dataBytes.Length);

        // Generar firma
        var signature = signer.GenerateSignature();

        return Convert.ToBase64String(signature);
    }

    /// <summary>
    /// Verifica una firma RSA-SHA256.
    /// </summary>
    /// <param name="data">Los datos originales.</param>
    /// <param name="signature">La firma en base64.</param>
    /// <param name="certificate">El certificado con clave pública.</param>
    /// <returns>True si la firma es válida.</returns>
    public bool VerifyRsaSha256Signature(string data, string signature, X509Certificate2 certificate)
    {
        try
        {
            var rsa = certificate.GetRSAPublicKey();
            if (rsa == null)
            {
                return false;
            }

            var signatureBytes = Convert.FromBase64String(signature);
            var dataBytes = Encoding.UTF8.GetBytes(data);

            return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
        catch
        {
            return false;
        }
    }
}