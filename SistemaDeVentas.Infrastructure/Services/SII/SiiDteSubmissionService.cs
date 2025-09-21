using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Servicio para envío de DTE al SII.
/// </summary>
public class SiiDteSubmissionService : ISiiDteSubmissionService
{
    private readonly HttpClient _httpClient;

    public SiiDteSubmissionService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Envía un lote de DTE al SII para procesamiento.
    /// </summary>
    public async Task<string> SubmitDteAsync(XDocument dteEnvelope, string token, int ambiente = 0)
    {
        var url = GetSubmissionUrl(ambiente);

        var soapEnvelope = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDoc xmlns=""http://www.sii.cl/wsdl"">
            <rutSender>0</rutSender>
            <dvSender>0</dvSender>
            <archivo>{dteEnvelope.ToString()}</archivo>
            <filename>EnvioDTE.xml</filename>
            <tipo>Boleta</tipo>
        </ingresarAceptarDoc>
    </soapenv:Body>
</soapenv:Envelope>";

        var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
        content.Headers.Add("Authorization", $"Bearer {token}");
        content.Headers.Add("SOAPAction", "");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return ExtractTrackIdFromSoapResponse(responseContent);
    }

    /// <summary>
    /// Envía un DTE individual al SII.
    /// </summary>
    public async Task<string> SubmitSingleDteAsync(string dteXml, string token, int ambiente = 0)
    {
        // Crear envelope simple para un DTE individual
        var envelope = new XDocument(
            new XElement("EnvioDTE",
                new XAttribute("version", "1.0"),
                new XElement("SetDTE",
                    new XAttribute("ID", "SetDoc"),
                    new XElement("Caratula",
                        new XAttribute("version", "1.0"),
                        new XElement("RutEmisor", "12345678-9"), // TODO: Extraer del DTE
                        new XElement("FchResol", DateTime.Now.ToString("yyyy-MM-dd")),
                        new XElement("NroResol", "0"),
                        new XElement("TmstFirmaEnv", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
                    ),
                    XDocument.Parse(dteXml).Root
                )
            )
        );

        return await SubmitDteAsync(envelope, token, ambiente);
    }

    private string GetSubmissionUrl(int ambiente)
    {
        return SiiSettings.GetServiceUrl(ambiente, SiiSettings.Urls.RecibeDTE);
    }

    private string ExtractTrackIdFromSoapResponse(string soapResponse)
    {
        var doc = XDocument.Parse(soapResponse);
        var trackIdElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "TRACKID");
        return trackIdElement?.Value ?? throw new InvalidOperationException("No se pudo extraer el TrackID de la respuesta SOAP");
    }
}