using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Moq;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Infrastructure.Data;
using SistemaDeVentas.Infrastructure.Services.DTE;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class CertificateServiceTests
{
    private readonly Mock<SalesSystemDbContext> _contextMock;
    private readonly CertificateService _service;

    public CertificateServiceTests()
    {
        _contextMock = new Mock<SalesSystemDbContext>();
        _service = new CertificateService(_contextMock.Object);
    }

    [Fact]
    public void LoadCertificateFromFile_FileNotFound_ThrowsFileNotFoundException()
    {
        // Arrange
        var nonExistentFile = "nonexistent.p12";

        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() => _service.LoadCertificateFromFile(nonExistentFile, "password"));
        Assert.Contains("no existe", exception.Message);
    }

    [Fact]
    public void LoadCertificateFromFile_ValidFile_ReturnsCertificate()
    {
        // Arrange
        var certificate = CreateTestCertificate();
        var tempFile = Path.GetTempFileName();
        try
        {
            // Guardar certificado en archivo temporal
            File.WriteAllBytes(tempFile, certificate.Export(X509ContentType.Pfx));

            // Act
            var result = _service.LoadCertificateFromFile(tempFile, "");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(certificate.Subject, result.Subject);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void LoadCertificateFromBytes_InvalidData_ThrowsCryptographicException()
    {
        // Arrange
        var invalidData = new byte[] { 1, 2, 3 };

        // Act & Assert
        var exception = Assert.Throws<CryptographicException>(() => _service.LoadCertificateFromBytes(invalidData, "password"));
        Assert.Contains("Error al cargar", exception.Message);
    }

    [Fact]
    public void LoadCertificateFromBytes_ValidData_ReturnsCertificate()
    {
        // Arrange
        var certificate = CreateTestCertificate();
        var certificateData = certificate.Export(X509ContentType.Pfx);

        // Act
        var result = _service.LoadCertificateFromBytes(certificateData, "");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(certificate.Subject, result.Subject);
    }

    [Fact]
    public void ValidateCertificateForSigning_ExpiredCertificate_ReturnsFalse()
    {
        // Arrange
        var expiredCertificate = CreateTestCertificate(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-1));

        // Act
        var result = _service.ValidateCertificateForSigning(expiredCertificate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateCertificateForSigning_FutureCertificate_ReturnsFalse()
    {
        // Arrange
        var futureCertificate = CreateTestCertificate(DateTime.Now.AddDays(1), DateTime.Now.AddYears(1));

        // Act
        var result = _service.ValidateCertificateForSigning(futureCertificate);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateCertificateForSigning_NoPrivateKey_ReturnsFalse()
    {
        // Arrange
        var certificate = CreateTestCertificate();
        // Crear certificado sin clave privada
        var publicOnlyCert = new X509Certificate2(certificate.RawData);

        // Act
        var result = _service.ValidateCertificateForSigning(publicOnlyCert);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateCertificateForSigning_ValidCertificate_ReturnsTrue()
    {
        // Arrange
        var certificate = CreateTestCertificate();

        // Act
        var result = _service.ValidateCertificateForSigning(certificate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetPrivateKey_NoRsaKey_ThrowsInvalidOperationException()
    {
        // Arrange
        // Crear un certificado con ECDSA en lugar de RSA (si es posible)
        // Para este test, usaremos un certificado sin clave privada
        var certificate = new X509Certificate2(CreateTestCertificate().RawData);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _service.GetPrivateKey(certificate));
        Assert.Contains("clave privada RSA", exception.Message);
    }

    [Fact]
    public void GetPrivateKey_ValidCertificate_ReturnsRsaKey()
    {
        // Arrange
        var certificate = CreateTestCertificate();

        // Act
        var result = _service.GetPrivateKey(certificate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<RSA>(result);
    }

    [Fact]
    public async Task LoadCertificateFromDatabaseAsync_NoCertificateFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var rutEmisor = "11111111-1";
        var ambiente = 0;

        var certificateDataSet = new Mock<DbSet<CertificateData>>();
        certificateDataSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<CertificateData, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CertificateData)null);

        _contextMock.Setup(c => c.CertificateDatas).Returns(certificateDataSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.LoadCertificateFromDatabaseAsync(rutEmisor, ambiente));
        Assert.Contains("No se encontró", exception.Message);
    }

    [Fact]
    public async Task LoadCertificateFromDatabaseAsync_ValidCertificateFound_ReturnsCertificate()
    {
        // Arrange
        var rutEmisor = "11111111-1";
        var ambiente = 0;
        var certificate = CreateTestCertificate();
        var certificateData = new CertificateData
        {
            Id = 1,
            RutEmisor = rutEmisor,
            Ambiente = ambiente,
            Activo = true,
            FechaVencimiento = DateTime.Now.AddYears(1),
            DatosCertificado = certificate.Export(X509ContentType.Pfx),
            PasswordEncriptado = ""
        };

        var certificateDataSet = new Mock<DbSet<CertificateData>>();
        certificateDataSet.Setup(m => m.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<CertificateData, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(certificateData);

        _contextMock.Setup(c => c.CertificateDatas).Returns(certificateDataSet.Object);

        // Act
        var result = await _service.LoadCertificateFromDatabaseAsync(rutEmisor, ambiente);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(certificate.Subject, result.Subject);
    }

    [Fact]
    public async Task SaveCertificateToDatabaseAsync_ValidData_ReturnsId()
    {
        // Arrange
        var certificateData = new CertificateData
        {
            RutEmisor = "11111111-1",
            Ambiente = 0,
            Activo = true,
            FechaVencimiento = DateTime.Now.AddYears(1),
            DatosCertificado = new byte[] { 1, 2, 3 },
            PasswordEncriptado = "password"
        };

        var certificateDataSet = new Mock<DbSet<CertificateData>>();
        _contextMock.Setup(c => c.CertificateDatas).Returns(certificateDataSet.Object);
        _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        var result = await _service.SaveCertificateToDatabaseAsync(certificateData);

        // Assert
        Assert.Equal(certificateData.Id, result);
        certificateDataSet.Verify(m => m.Add(certificateData), Times.Once);
        _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public void ConvertToBouncyCastle_ValidRsaKey_ReturnsParameters()
    {
        // Arrange
        var certificate = CreateTestCertificate();
        var rsa = certificate.GetRSAPrivateKey();

        // Act
        var result = CertificateService.ConvertToBouncyCastle(rsa);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters>(result);
    }

    private X509Certificate2 CreateTestCertificate(DateTime? notBefore = null, DateTime? notAfter = null)
    {
        // Crear un certificado de prueba self-signed
        using var rsa = System.Security.Cryptography.RSA.Create(2048);
        var request = new System.Security.Cryptography.X509Certificates.CertificateRequest(
            "CN=TestCertificate",
            rsa,
            System.Security.Cryptography.HashAlgorithmName.SHA256,
            System.Security.Cryptography.RSASignaturePadding.Pkcs1);

        var certificate = request.CreateSelfSigned(
            notBefore ?? DateTimeOffset.Now.AddDays(-1),
            notAfter ?? DateTimeOffset.Now.AddYears(1));
        return new X509Certificate2(certificate.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Pfx));
    }
}