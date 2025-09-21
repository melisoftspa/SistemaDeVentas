using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Moq;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Infrastructure.Services.DTE;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class DigitalSignatureServiceTests
{
    private readonly Mock<ICertificateService> _certificateServiceMock;
    private readonly DigitalSignatureService _service;

    public DigitalSignatureServiceTests()
    {
        _certificateServiceMock = new Mock<ICertificateService>();
        _service = new DigitalSignatureService(_certificateServiceMock.Object);
    }

    [Fact]
    public void SignXmlDocument_ValidCertificate_ReturnsSignedXml()
    {
        // Arrange
        var xmlDocument = XDocument.Parse("<Documento><IdDoc><TipoDTE>33</TipoDTE></IdDoc></Documento>");
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(certificate.GetRSAPrivateKey());

        // Act
        var result = _service.SignXmlDocument(xmlDocument, certificate);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Signature", result.ToString());
    }

    [Fact]
    public void SignXmlDocument_InvalidCertificate_ThrowsException()
    {
        // Arrange
        var xmlDocument = XDocument.Parse("<Documento></Documento>");
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(false);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _service.SignXmlDocument(xmlDocument, certificate));
    }

    [Fact]
    public void VerifyXmlSignature_ValidSignature_ReturnsTrue()
    {
        // Arrange
        var xmlDocument = XDocument.Parse("<Documento><IdDoc><TipoDTE>33</TipoDTE></IdDoc></Documento>");
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(certificate.GetRSAPrivateKey());

        var signedDocument = _service.SignXmlDocument(xmlDocument, certificate);

        // Act
        var result = _service.VerifyXmlSignature(signedDocument);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyXmlSignature_NoSignature_ReturnsFalse()
    {
        // Arrange
        var xmlDocument = XDocument.Parse("<Documento></Documento>");

        // Act
        var result = _service.VerifyXmlSignature(xmlDocument);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalculateSha256Digest_ValidData_ReturnsDigest()
    {
        // Arrange
        var data = "test data";

        // Act
        var result = _service.CalculateSha256Digest(data);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void SignDataWithRsaSha256_ValidData_ReturnsSignature()
    {
        // Arrange
        var data = "test data";
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(certificate.GetRSAPrivateKey());

        // Act
        var result = _service.SignDataWithRsaSha256(data, certificate);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void VerifyRsaSha256Signature_ValidSignature_ReturnsTrue()
    {
        // Arrange
        var data = "test data";
        var certificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(certificate.GetRSAPrivateKey());

        var signature = _service.SignDataWithRsaSha256(data, certificate);

        // Act
        var result = _service.VerifyRsaSha256Signature(data, signature, certificate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyRsaSha256Signature_InvalidSignature_ReturnsFalse()
    {
        // Arrange
        var data = "test data";
        var invalidSignature = "invalid";
        var certificate = CreateTestCertificate();

        // Act
        var result = _service.VerifyRsaSha256Signature(data, invalidSignature, certificate);

        // Assert
        Assert.False(result);
    }

    private X509Certificate2 CreateTestCertificate()
    {
        // Crear un certificado de prueba self-signed
        using var rsa = System.Security.Cryptography.RSA.Create(2048);
        var request = new System.Security.Cryptography.X509Certificates.CertificateRequest(
            "CN=TestCertificate",
            rsa,
            System.Security.Cryptography.HashAlgorithmName.SHA256,
            System.Security.Cryptography.RSASignaturePadding.Pkcs1);

        var certificate = request.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.Now.AddYears(1));
        return new X509Certificate2(certificate.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Pfx));
    }
}