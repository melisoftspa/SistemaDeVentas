using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Moq;
using Moq.Protected;
using SistemaDeVentas.Core.Domain.Exceptions.DTE;
using SistemaDeVentas.Infrastructure.Services.SII;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class SiiAuthenticationServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly SiiAuthenticationService _service;

    public SiiAuthenticationServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _service = new SiiAuthenticationService(_httpClient);
    }

    [Fact]
    public async Task GetSeedAsync_SuccessfulResponse_ReturnsSeed()
    {
        // Arrange
        var expectedSeed = "test-seed-123";
        var soapResponse = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getSeedResponse>
            <SEMILLA>{expectedSeed}</SEMILLA>
        </getSeedResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        var result = await _service.GetSeedAsync(0);

        // Assert
        Assert.Equal(expectedSeed, result);
    }

    [Fact]
    public async Task GetSeedAsync_HttpRequestException_ThrowsSiiCommunicationException()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<SiiCommunicationException>(() => _service.GetSeedAsync(0));
        Assert.Contains("Error al obtener semilla", exception.Message);
    }

    [Fact]
    public async Task GetSeedAsync_InvalidSoapResponse_ThrowsSiiCommunicationException()
    {
        // Arrange
        var invalidSoapResponse = "<?xml version=\"1.0\"?><invalid></invalid>";
        SetupHttpResponse(invalidSoapResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<SiiCommunicationException>(() => _service.GetSeedAsync(0));
        Assert.Contains("procesar la respuesta SOAP", exception.Message);
    }

    [Fact]
    public async Task GetTokenAsync_SuccessfulResponse_ReturnsToken()
    {
        // Arrange
        var signedSeedXml = "<signed-seed>test</signed-seed>";
        var expectedToken = "test-token-123";
        var soapResponse = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getTokenResponse>
            <TOKEN>{expectedToken}</TOKEN>
        </getTokenResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        var result = await _service.GetTokenAsync(signedSeedXml, 0);

        // Assert
        Assert.Equal(expectedToken, result);
    }

    [Fact]
    public async Task GetTokenAsync_HttpRequestException_ThrowsSiiCommunicationException()
    {
        // Arrange
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<SiiCommunicationException>(() => _service.GetTokenAsync("<signed-seed></signed-seed>", 0));
        Assert.Contains("Error al obtener token", exception.Message);
    }

    [Fact]
    public async Task GetTokenAsync_InvalidSoapResponse_ThrowsSiiCommunicationException()
    {
        // Arrange
        var invalidSoapResponse = "<?xml version=\"1.0\"?><invalid></invalid>";
        SetupHttpResponse(invalidSoapResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<SiiCommunicationException>(() => _service.GetTokenAsync("<signed-seed></signed-seed>", 0));
        Assert.Contains("procesar la respuesta SOAP", exception.Message);
    }

    [Fact]
    public async Task ValidateTokenAsync_EmptyToken_ReturnsFalse()
    {
        // Act
        var result = await _service.ValidateTokenAsync("");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateTokenAsync_NullToken_ReturnsFalse()
    {
        // Act
        var result = await _service.ValidateTokenAsync(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ValidateTokenAsync_ValidToken_ReturnsTrue()
    {
        // Act
        var result = await _service.ValidateTokenAsync("valid-token");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetSeedAsync_UsesCorrectUrlForCertificacion()
    {
        // Arrange
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getSeedResponse>
            <SEMILLA>test-seed</SEMILLA>
        </getSeedResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.GetSeedAsync(1); // Ambiente certificación

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri.ToString().Contains("maullin.sii.cl")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetTokenAsync_UsesCorrectUrlForProduccion()
    {
        // Arrange
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getTokenResponse>
            <TOKEN>test-token</TOKEN>
        </getTokenResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.GetTokenAsync("<signed-seed></signed-seed>", 0); // Ambiente producción

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri.ToString().Contains("palena.sii.cl")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetSeedAsync_SendsCorrectSoapEnvelope()
    {
        // Arrange
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getSeedResponse>
            <SEMILLA>test-seed</SEMILLA>
        </getSeedResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.GetSeedAsync(0);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Content.ReadAsStringAsync().Result.Contains("getSeed") &&
                req.Content.ReadAsStringAsync().Result.Contains("http://www.sii.cl/wsdl")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetTokenAsync_SendsCorrectSoapEnvelope()
    {
        // Arrange
        var signedSeedXml = "<signed-seed>test</signed-seed>";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <getTokenResponse>
            <TOKEN>test-token</TOKEN>
        </getTokenResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.GetTokenAsync(signedSeedXml, 0);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Content.ReadAsStringAsync().Result.Contains("getToken") &&
                req.Content.ReadAsStringAsync().Result.Contains(signedSeedXml)),
            ItExpr.IsAny<CancellationToken>());
    }

    private void SetupHttpResponse(string responseContent)
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(responseContent, Encoding.UTF8, "text/xml")
        };

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }
}