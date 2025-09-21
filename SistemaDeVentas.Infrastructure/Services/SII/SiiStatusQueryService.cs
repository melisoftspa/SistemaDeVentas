using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Servicio para consulta de estados de envíos al SII.
/// </summary>
public class SiiStatusQueryService : ISiiStatusQueryService
{
    private readonly HttpClient _httpClient;

    public SiiStatusQueryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Consulta el estado de un envío por TrackID.
    /// </summary>
    public async Task<SiiSubmissionStatus> QueryStatusAsync(string trackId, string token, int ambiente = 0)
    {
        var url = GetQueryUrl(ambiente);

        var soapEnvelope = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getEstDte xmlns=""http://www.sii.cl/wsdl"">
            <rutCompany>0</rutCompany>
            <dvCompany>0</dvCompany>
            <trackId>{trackId}</trackId>
        </getEstDte>
    </soapenv:Body>
</soapenv:Envelope>";

        var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
        content.Headers.Add("Authorization", $"Bearer {token}");
        content.Headers.Add("SOAPAction", "");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return ParseStatusResponse(responseContent, trackId);
    }

    private string GetQueryUrl(int ambiente)
    {
        return SiiSettings.GetServiceUrl(ambiente, SiiSettings.Urls.QueryEstDte);
    }

    private SiiSubmissionStatus ParseStatusResponse(string soapResponse, string trackId)
    {
        var doc = XDocument.Parse(soapResponse);

        var estadoElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "ESTADO");
        var detalleElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "DETALLE");

        var estado = estadoElement?.Value ?? "DESCONOCIDO";
        var detalle = detalleElement?.Value ?? "";

        // Determinar si es exitoso basado en el estado
        var isSuccess = estado == "0" || estado.ToUpper() == "OK" || estado.ToUpper() == "ACEPTADO";

        return new SiiSubmissionStatus
        {
            TrackId = trackId,
            Estado = estado,
            Detalle = detalle,
            IsSuccess = isSuccess
        };
    }
}