using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;

namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Servicio de autenticación con el SII.
/// Implementa la obtención de semillas y tokens usando SOAP.
/// </summary>
public class SiiAuthenticationService : ISiiAuthenticationService
{
    private readonly HttpClient _httpClient;

    public SiiAuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Obtiene una semilla de autenticación del SII.
    /// </summary>
    public async Task<string> GetSeedAsync(int ambiente = 0)
    {
        try
        {
            var url = GetSeedUrl(ambiente);

            var soapEnvelope = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getSeed xmlns=""http://www.sii.cl/wsdl""/>
    </soapenv:Body>
</soapenv:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return ExtractSeedFromSoapResponse(responseContent);
        }
        catch (HttpRequestException ex)
        {
            throw new SiiCommunicationException("Error al obtener semilla de autenticación del SII", ex);
        }
        catch (Exception ex)
        {
            throw new SiiCommunicationException("Error inesperado al obtener semilla del SII", ex);
        }
    }

    /// <summary>
    /// Solicita un token de autenticación al SII usando una semilla firmada.
    /// </summary>
    public async Task<string> GetTokenAsync(string signedSeedXml, int ambiente = 0)
    {
        try
        {
            var url = GetTokenUrl(ambiente);

            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getToken xmlns=""http://www.sii.cl/wsdl"">
            <item>{signedSeedXml}</item>
        </getToken>
    </soapenv:Body>
</soapenv:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return ExtractTokenFromSoapResponse(responseContent);
        }
        catch (HttpRequestException ex)
        {
            throw new SiiCommunicationException("Error al obtener token de autenticación del SII", ex);
        }
        catch (Exception ex)
        {
            throw new SiiCommunicationException("Error inesperado al obtener token del SII", ex);
        }
    }

    /// <summary>
    /// Verifica si un token es válido (simulado - en producción verificar con SII).
    /// </summary>
    public Task<bool> ValidateTokenAsync(string token)
    {
        // TODO: Implementar validación real del token con el SII
        // Por ahora, asumir válido si no está vacío
        return Task.FromResult(!string.IsNullOrEmpty(token));
    }

    private string GetSeedUrl(int ambiente)
    {
        return SiiSettings.GetServiceUrl(ambiente, SiiSettings.Urls.CrSeed);
    }

    private string GetTokenUrl(int ambiente)
    {
        return SiiSettings.GetServiceUrl(ambiente, SiiSettings.Urls.GetTokenFromSeed);
    }

    private string ExtractSeedFromSoapResponse(string soapResponse)
    {
        try
        {
            var doc = XDocument.Parse(soapResponse);
            var seedElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "SEMILLA");
            return seedElement?.Value ?? throw new SiiCommunicationException("No se pudo extraer la semilla de la respuesta SOAP del SII");
        }
        catch (SiiCommunicationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SiiCommunicationException("Error al procesar la respuesta SOAP del SII para obtener semilla", ex);
        }
    }

    private string ExtractTokenFromSoapResponse(string soapResponse)
    {
        try
        {
            var doc = XDocument.Parse(soapResponse);
            var tokenElement = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "TOKEN");
            return tokenElement?.Value ?? throw new SiiCommunicationException("No se pudo extraer el token de la respuesta SOAP del SII");
        }
        catch (SiiCommunicationException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SiiCommunicationException("Error al procesar la respuesta SOAP del SII para obtener token", ex);
        }
    }
}