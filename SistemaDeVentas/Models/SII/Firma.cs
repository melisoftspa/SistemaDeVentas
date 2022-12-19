using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Xml;

namespace SistemaDeVentas.Models.SII
{
    public class Firma
    {
        private X509Certificate2 certificado { get; set; }

        public Firma(string certificatePath, string password)
        {
            certificado = new X509Certificate2(certificatePath, password);
        }

        ///
        /// Se le pasa un xml en string y lo devuelve firmado
        /// 

        /// xml no firmado
        /// si se quiere firmar una parte del xml se debe poner el #id, si no: ""
        /// para firmar semilla se requiere, para firmar envioDTE y DTE no se requiere
        public string Firmar(string xml, string referenceUri, bool addTransform)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            SignedXml signedXml = new SignedXml(xmlDocument);
            signedXml.SigningKey = certificado.PrivateKey;

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new RSAKeyValue((RSA)certificado.PrivateKey));
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            Reference reference = new Reference();
            reference.Uri = referenceUri;

            if (addTransform)
            {
                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            }

            Signature signature = signedXml.Signature;
            signature.SignedInfo.AddReference(reference);
            signature.KeyInfo = keyInfo;

            // Generar firma
            signedXml.ComputeSignature();

            // Insertar la firma en xmlDocument
            xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(signedXml.GetXml(), true));

            return xmlDocument.InnerXml;
        }
    }
}
