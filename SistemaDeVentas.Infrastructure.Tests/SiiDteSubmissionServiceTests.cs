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

public class SiiDteSubmissionServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly SiiDteSubmissionService _service;

    public SiiDteSubmissionServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _service = new SiiDteSubmissionService(_httpClient);
    }

    [Fact]
    public async Task SubmitDteAsync_SuccessfulResponse_ReturnsTrackId()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", "test"));
        var token = "test-token";
        var expectedTrackId = "123456789";
        var soapResponse = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>{expectedTrackId}</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        var result = await _service.SubmitDteAsync(dteEnvelope, token, 0);

        // Assert
        Assert.Equal(expectedTrackId, result);
    }

    [Fact]
    public async Task SubmitDteAsync_HttpRequestException_ThrowsException()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", "test"));
        var token = "test-token";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("Network error"));

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.SubmitDteAsync(dteEnvelope, token, 0));
    }

    [Fact]
    public async Task SubmitDteAsync_InvalidSoapResponse_ThrowsInvalidOperationException()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", "test"));
        var token = "test-token";
        var invalidSoapResponse = "<?xml version=\"1.0\"?><invalid></invalid>";

        SetupHttpResponse(invalidSoapResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.SubmitDteAsync(dteEnvelope, token, 0));
        Assert.Contains("No se pudo extraer", exception.Message);
    }

    [Fact]
    public async Task SubmitSingleDteAsync_ValidDteXml_ReturnsTrackId()
    {
        // Arrange
        var dteXml = "<DTE><Documento><IdDoc><TipoDTE>33</TipoDTE></IdDoc></Documento></DTE>";
        var token = "test-token";
        var expectedTrackId = "987654321";
        var soapResponse = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>{expectedTrackId}</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        var result = await _service.SubmitSingleDteAsync(dteXml, token, 0);

        // Assert
        Assert.Equal(expectedTrackId, result);
    }

    [Fact]
    public async Task SubmitDteAsync_UsesCorrectUrlForProduccion()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", "test"));
        var token = "test-token";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>123456</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.SubmitDteAsync(dteEnvelope, token, 0); // Ambiente producción

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri.ToString().Contains("palena.sii.cl")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task SubmitDteAsync_UsesCorrectUrlForCertificacion()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", "test"));
        var token = "test-token";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>123456</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.SubmitDteAsync(dteEnvelope, token, 1); // Ambiente certificación

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri.ToString().Contains("maullin.sii.cl")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task SubmitDteAsync_SendsCorrectSoapEnvelope()
    {
        // Arrange
        var dteEnvelope = new XDocument(new XElement("EnvioDTE", new XElement("Test", "content")));
        var token = "test-token";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>123456</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.SubmitDteAsync(dteEnvelope, token, 0);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Content.ReadAsStringAsync().Result.Contains("ingresarAceptarDoc") &&
                req.Content.ReadAsStringAsync().Result.Contains("rutSender>0</rutSender>") &&
                req.Content.ReadAsStringAsync().Result.Contains("dvSender>0</dvSender>") &&
                req.Content.ReadAsStringAsync().Result.Contains("<archivo>") &&
                req.Content.ReadAsStringAsync().Result.Contains("<filename>EnvioDTE.xml</filename>") &&
                req.Content.ReadAsStringAsync().Result.Contains("<tipo>Boleta</tipo>") &&
                req.Headers.Authorization.ToString() == $"Bearer {token}"),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task SubmitSingleDteAsync_CreatesCorrectEnvelope()
    {
        // Arrange
        var dteXml = "<DTE><Documento><IdDoc><TipoDTE>33</TipoDTE></IdDoc></Documento></DTE>";
        var token = "test-token";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>123456</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.SubmitSingleDteAsync(dteXml, token, 0);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Content.ReadAsStringAsync().Result.Contains("EnvioDTE") &&
                req.Content.ReadAsStringAsync().Result.Contains("SetDTE") &&
                req.Content.ReadAsStringAsync().Result.Contains("Caratula") &&
                req.Content.ReadAsStringAsync().Result.Contains("RutEmisor") &&
                req.Content.ReadAsStringAsync().Result.Contains("FchResol") &&
                req.Content.ReadAsStringAsync().Result.Contains("NroResol") &&
                req.Content.ReadAsStringAsync().Result.Contains("TmstFirmaEnv") &&
                req.Content.ReadAsStringAsync().Result.Contains(dteXml)),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task SubmitDteAsync_IncludesDteEnvelopeInSoapBody()
    {
        // Arrange
        var dteEnvelope = new XDocument(
            new XElement("EnvioDTE",
                new XAttribute("version", "1.0"),
                new XElement("TestElement", "test content")
            )
        );
        var token = "test-token";
        var soapResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
    <soapenv:Body>
        <ingresarAceptarDocResponse>
            <TRACKID>123456</TRACKID>
        </ingresarAceptarDocResponse>
    </soapenv:Body>
</soapenv:Envelope>";

        SetupHttpResponse(soapResponse);

        // Act
        await _service.SubmitDteAsync(dteEnvelope, token, 0);

        // Assert
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Content.ReadAsStringAsync().Result.Contains("<TestElement>test content</TestElement>") &&
                req.Content.ReadAsStringAsync().Result.Contains("version=\"1.0\"")),
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