using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Infrastructure.Services.DTE;
using SistemaDeVentas.Infrastructure.Services.SII;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class DteIntegrationTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly Mock<ICertificateService> _certificateServiceMock;
    private readonly Mock<ISiiAuthenticationService> _siiAuthMock;
    private readonly Mock<ISiiDteSubmissionService> _siiSubmissionMock;
    private readonly FacturaAfectaBuilder _builder;
    private readonly DigitalSignatureService _signatureService;

    public DteIntegrationTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _certificateServiceMock = new Mock<ICertificateService>();
        _siiAuthMock = new Mock<ISiiAuthenticationService>();
        _siiSubmissionMock = new Mock<ISiiDteSubmissionService>();

        _builder = new FacturaAfectaBuilder(_loggerMock.Object);
        _signatureService = new DigitalSignatureService(_certificateServiceMock.Object);
    }

    [Fact]
    public void FullDteFlow_ValidDocument_Success()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(System.Security.Cryptography.RSA.Create());

        // Act
        var xmlDocument = _builder.BuildXml(dteDocument);
        var signedDocument = _signatureService.SignXmlDocument(xmlDocument, certificate);
        var isValidSignature = _signatureService.VerifyXmlSignature(signedDocument);
        var isValidXml = _builder.ValidateXml(signedDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(signedDocument);
        Assert.True(isValidSignature);
        Assert.True(isValidXml);
    }

    [Fact]
    public void FullDteFlow_InvalidDocument_ThrowsException()
    {
        // Arrange
        var invalidDocument = new DteDocument(); // Documento vacío

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => _builder.BuildXml(invalidDocument));
    }

    [Fact]
    public void SignatureAndValidationFlow_Success()
    {
        // Arrange
        var xmlDocument = XDocument.Parse("<Documento><IdDoc><TipoDTE>33</TipoDTE></IdDoc></Documento>");
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(System.Security.Cryptography.RSA.Create());

        // Act
        var signedDocument = _signatureService.SignXmlDocument(xmlDocument, certificate);
        var isValid = _signatureService.VerifyXmlSignature(signedDocument);

        // Assert
        Assert.True(isValid);
    }

    private DteDocument CreateValidDteDocument()
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaAfecta,
                Folio = 12345,
                FechaEmision = DateTime.Now
            },
            Emisor = new Emisor
            {
                RutEmisor = "11111111-1",
                RazonSocial = "Empresa Emisora S.A.",
                GiroEmisor = "Venta de productos",
                ActividadEconomica = 620100
            },
            Receptor = new Receptor
            {
                RutReceptor = "22222222-2",
                RazonSocialReceptor = "Cliente S.A."
            },
            Totales = new TotalesDte
            {
                MontoNeto = 100000m,
                TasaIVA = 19m,
                IVA = 19000m,
                MontoTotal = 119000m
            },
            Detalles = new List<DetalleDte>
            {
                new DetalleDte
                {
                    NumeroLineaDetalle = 1,
                    NombreItem = "Producto Afecto",
                    CantidadItem = 1m,
                    PrecioItem = 100000m,
                    MontoItem = 100000m
                }
            }
        };
    }

    private System.Security.Cryptography.X509Certificates.X509Certificate2 CreateTestCertificate()
    {
        using var rsa = System.Security.Cryptography.RSA.Create(2048);
        var request = new System.Security.Cryptography.X509Certificates.CertificateRequest(
            "CN=TestCertificate",
            rsa,
            System.Security.Cryptography.HashAlgorithmName.SHA256,
            System.Security.Cryptography.RSASignaturePadding.Pkcs1);

        var certificate = request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1));
        return new System.Security.Cryptography.X509Certificates.X509Certificate2(
            certificate.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Pfx));
    }
}