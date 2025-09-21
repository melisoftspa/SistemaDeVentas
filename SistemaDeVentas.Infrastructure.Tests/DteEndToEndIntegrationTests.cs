using System.Drawing;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Services.DTE;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class DteEndToEndIntegrationTests
{
    private readonly Mock<ILogger<DteBuilderService>> _loggerMock;
    private readonly Mock<ICertificateService> _certificateServiceMock;
    private readonly Mock<IPdf417Service> _pdf417ServiceMock;
    private readonly Mock<ICafRepository> _cafRepositoryMock;
    private readonly Mock<ISalesDbContext> _dbContextMock;

    private readonly FacturaAfectaBuilder _builder;
    private readonly DigitalSignatureService _signatureService;
    private readonly Pdf417Service _pdf417Service;
    private readonly StampingService _stampingService;

    public DteEndToEndIntegrationTests()
    {
        _loggerMock = new Mock<ILogger<DteBuilderService>>();
        _certificateServiceMock = new Mock<ICertificateService>();
        _pdf417ServiceMock = new Mock<IPdf417Service>();
        _cafRepositoryMock = new Mock<ICafRepository>();
        _dbContextMock = new Mock<ISalesDbContext>();

        _builder = new FacturaAfectaBuilder(_loggerMock.Object);
        _signatureService = new DigitalSignatureService(_certificateServiceMock.Object);
        _pdf417Service = new Pdf417Service();
        _stampingService = new StampingService(null, _signatureService, _pdf417Service); // HttpClient null para tests
    }

    [Fact]
    public void EndToEndFlow_VentaToDteToSignedToStampedToPdf417ToValidated_Success()
    {
        // Arrange
        var sale = CreateTestSale();
        var dteDocument = CreateDteFromSale(sale);
        var certificate = CreateTestCertificate();
        var caf = CreateTestCaf();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(System.Security.Cryptography.RSA.Create());
        _cafRepositoryMock.Setup(r => r.ObtenerPorTipoDocumentoAsync((int)TipoDte.FacturaAfecta, 0, "11111111-1")).ReturnsAsync(caf);

        // Act
        // 1. Construir XML DTE
        var xmlDocument = _builder.BuildXml(dteDocument);

        // 2. Firmar digitalmente
        var signedDocument = _signatureService.SignXmlDocument(xmlDocument, certificate);

        // 3. Timbrar (agregar TED)
        var stampedDocument = _stampingService.RequestElectronicStamp(signedDocument, "11111111-1", 0).Result;

        // 4. Generar PDF417
        var pdf417Bitmap = _stampingService.GeneratePdf417Image(stampedDocument);

        // 5. Validar XML final
        var isValidSignature = _signatureService.VerifyXmlSignature(stampedDocument);
        var isValidXml = _builder.ValidateXml(stampedDocument);

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(signedDocument);
        Assert.NotNull(stampedDocument);
        Assert.NotNull(pdf417Bitmap);
        Assert.IsType<Bitmap>(pdf417Bitmap);
        Assert.True(isValidSignature);
        Assert.True(isValidXml);

        // Verificar que tenga TED
        var tedElement = stampedDocument.Root?.Element("TED");
        Assert.NotNull(tedElement);

        // Verificar que tenga firma
        var signatureElement = stampedDocument.Descendants().FirstOrDefault(e => e.Name.LocalName == "Signature");
        Assert.NotNull(signatureElement);
    }

    [Fact]
    public void BoletaEndToEndFlow_VentaToDteToSignedToStampedToPdf417ToValidated_Success()
    {
        // Arrange
        var sale = CreateTestSale();
        var dteDocument = CreateBoletaFromSale(sale);
        var certificate = CreateTestCertificate();
        var caf = CreateTestCafBoleta();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(certificate)).Returns(true);
        _certificateServiceMock.Setup(c => c.GetPrivateKey(certificate)).Returns(System.Security.Cryptography.RSA.Create());
        _cafRepositoryMock.Setup(r => r.ObtenerPorTipoDocumentoAsync((int)TipoDte.BoletaAfecta, 0, "11111111-1")).ReturnsAsync(caf);

        // Act
        // 1. Construir XML DTE
        var xmlDocument = _builder.BuildXml(dteDocument); // Usar builder base para boletas

        // 2. Firmar digitalmente
        var signedDocument = _signatureService.SignXmlDocument(xmlDocument, certificate);

        // 3. Timbrar (agregar TED)
        var stampedDocument = _stampingService.RequestElectronicStamp(signedDocument, "11111111-1", 0).Result;

        // 4. Generar PDF417
        var pdf417Bitmap = _stampingService.GeneratePdf417Image(stampedDocument);

        // 5. Validar XML final
        var isValidSignature = _signatureService.VerifyXmlSignature(stampedDocument);
        var isValidXml = _builder.ValidateXml(stampedDocument); // Validación básica

        // Assert
        Assert.NotNull(xmlDocument);
        Assert.NotNull(signedDocument);
        Assert.NotNull(stampedDocument);
        Assert.NotNull(pdf417Bitmap);
        Assert.IsType<Bitmap>(pdf417Bitmap);
        Assert.True(isValidSignature);
        Assert.True(isValidXml);

        // Verificar que tenga TED
        var tedElement = stampedDocument.Root?.Element("TED");
        Assert.NotNull(tedElement);

        // Verificar tipo de DTE
        Assert.Equal("39", stampedDocument.Root?.Element("Encabezado")?.Element("IdDoc")?.Element("TipoDTE")?.Value);
    }

    [Fact]
    public void InvalidCertificate_ThrowsException()
    {
        // Arrange
        var dteDocument = CreateValidDteDocument();
        var invalidCertificate = CreateTestCertificate();

        _certificateServiceMock.Setup(c => c.ValidateCertificateForSigning(invalidCertificate)).Returns(false);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            _signatureService.SignXmlDocument(_builder.BuildXml(dteDocument), invalidCertificate));
    }

    private Sale CreateTestSale()
    {
        return new Sale
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Total = 119000.0,
            IdUser = Guid.NewGuid(),
            Details = new List<Detail>
            {
                new Detail
                {
                    Id = Guid.NewGuid(),
                    IdSale = Guid.NewGuid(),
                    IdProduct = Guid.NewGuid(),
                    Amount = 1.0,
                    Price = 100000.0,
                    Total = 100000.0
                }
            }
        };
    }

    private DteDocument CreateDteFromSale(Sale sale)
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.FacturaAfecta,
                Folio = 12345,
                FechaEmision = sale.Date
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
                    NombreItem = "Producto de Venta",
                    CantidadItem = 1m,
                    PrecioItem = 100000m,
                    MontoItem = 100000m
                }
            }
        };
    }

    private DteDocument CreateBoletaFromSale(Sale sale)
    {
        return new DteDocument
        {
            IdDoc = new IdDoc
            {
                TipoDTE = TipoDte.BoletaAfecta,
                Folio = 12346,
                FechaEmision = sale.Date,
                IndicadorServicio = 3 // Boleta de Ventas y Servicios
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
                RutReceptor = "66666666-6" // Consumidor final
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
                    NombreItem = "Producto Boleta",
                    CantidadItem = 1m,
                    PrecioItem = 100000m,
                    MontoItem = 100000m
                }
            }
        };
    }

    private DteDocument CreateValidDteDocument()
    {
        return CreateDteFromSale(CreateTestSale());
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

    private Caf CreateTestCaf()
    {
        return new Caf
        {
            Id = 1,
            TipoDocumento = (int)TipoDte.FacturaAfecta,
            Ambiente = 0,
            RutEmisor = "11111111-1",
            FolioDesde = 1,
            FolioHasta = 1000,
            XmlContent = "<CAF><DA><RE>11111111-1</RE><RS>Test</RS><TD>33</TD><RNG><D>1</D><H>1000</H></RNG><FA>2023-01-01</FA></DA><FRMA>signature</FRMA></CAF>",
            FechaAutorizacion = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddYears(1),
            Activo = true
        };
    }

    private Caf CreateTestCafBoleta()
    {
        return new Caf
        {
            Id = 2,
            TipoDocumento = (int)TipoDte.BoletaAfecta,
            Ambiente = 0,
            RutEmisor = "11111111-1",
            FolioDesde = 1,
            FolioHasta = 1000,
            XmlContent = "<CAF><DA><RE>11111111-1</RE><RS>Test</RS><TD>39</TD><RNG><D>1</D><H>1000</H></RNG><FA>2023-01-01</FA></DA><FRMA>signature</FRMA></CAF>",
            FechaAutorizacion = DateTime.Now,
            FechaVencimiento = DateTime.Now.AddYears(1),
            Activo = true
        };
    }
}