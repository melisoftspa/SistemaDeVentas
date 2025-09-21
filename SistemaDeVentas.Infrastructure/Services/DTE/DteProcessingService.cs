using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SistemaDeVentas.Core.Application.Services.DTE;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio completo para procesamiento de DTE: construcción, firma y timbrado.
/// </summary>
public class DteProcessingService : DteBuilderService, IDteProcessingService
{
    private readonly IDigitalSignatureService _signatureService;
    private readonly IStampingService _stampingService;
    private readonly ICertificateService _certificateService;
    private readonly IOptions<DteSettings> _dteSettings;
    private readonly ICafRepository _cafRepository;

    public DteProcessingService(
        ILogger<DteBuilderService> logger,
        IDigitalSignatureService signatureService,
        IStampingService stampingService,
        ICertificateService certificateService,
        IOptions<DteSettings> dteSettings,
        ICafRepository cafRepository)
        : base(logger)
    {
        _signatureService = signatureService;
        _stampingService = stampingService;
        _certificateService = certificateService;
        _dteSettings = dteSettings;
        _cafRepository = cafRepository;
    }

    /// <summary>
    /// Construye, firma y timbra un documento DTE completo usando la configuración.
    /// </summary>
    public async Task<XDocument> BuildSignAndStampDteWithSettings(DteDocument dteDocument, int tipoDocumento)
    {
        var settings = _dteSettings.Value;

        // Validar configuración
        var errores = settings.Validar();
        if (errores.Any())
        {
            throw new InvalidOperationException($"Configuración DTE inválida: {string.Join(", ", errores)}");
        }

        // Obtener certificado desde configuración
        var certificate = await _certificateService.LoadCertificateFromDatabaseAsync(settings.ObtenerRutLimpio(), settings.Ambiente);

        // Obtener CAF
        var caf = await _cafRepository.ObtenerPorTipoDocumentoAsync(tipoDocumento, settings.Ambiente, settings.ObtenerRutLimpio());
        if (caf == null)
        {
            throw new InvalidOperationException($"No se encontró CAF válido para tipo documento {tipoDocumento} en ambiente {settings.Ambiente}");
        }

        // Asignar folio del CAF
        dteDocument.IdDoc.Folio = caf.ObtenerSiguienteFolio();

        // Procesar DTE
        var result = await BuildSignAndStampDte(dteDocument, certificate, settings.ObtenerRutLimpio(), settings.Ambiente);

        // Actualizar folio en BD
        await _cafRepository.ActualizarFolioActualAsync(caf.Id, caf.FolioActual);

        return result;
    }

    /// <summary>
    /// Construye, firma y timbra un documento DTE completo.
    /// </summary>
    public async Task<XDocument> BuildSignAndStampDte(DteDocument dteDocument, X509Certificate2 certificate, string rutEmisor, int ambiente = 0)
    {
        // 1. Construir XML
        var xmlDocument = BuildXml(dteDocument);

        // 2. Firmar digitalmente
        var signedDocument = BuildAndSignDte(dteDocument, certificate);

        // 3. Timbrar electrónicamente
        var stampedDocument = await StampSignedDte(signedDocument, rutEmisor, ambiente);

        return stampedDocument;
    }

    /// <summary>
    /// Construye y firma un documento DTE.
    /// </summary>
    public XDocument BuildAndSignDte(DteDocument dteDocument, X509Certificate2 certificate)
    {
        // Construir XML
        var xmlDocument = BuildXml(dteDocument);

        // Firmar digitalmente
        var signedDocument = _signatureService.SignXmlDocument(xmlDocument, certificate);

        return signedDocument;
    }

    /// <summary>
    /// Timbra un documento DTE ya firmado.
    /// </summary>
    public async Task<XDocument> StampSignedDte(XDocument signedXmlDocument, string rutEmisor, int ambiente = 0)
    {
        var stampedDocument = await _stampingService.RequestElectronicStamp(signedXmlDocument, rutEmisor, ambiente);
        return stampedDocument;
    }

    /// <summary>
    /// Valida un documento DTE completo (estructura, firma y timbre).
    /// </summary>
    public bool ValidateCompleteDte(XDocument xmlDocument)
    {
        // 1. Validar estructura básica
        if (!ValidateXml(xmlDocument))
        {
            return false;
        }

        // 2. Validar firma digital
        if (!_signatureService.VerifyXmlSignature(xmlDocument))
        {
            return false;
        }

        // 3. Validar timbre electrónico
        if (!_stampingService.VerifyElectronicStamp(xmlDocument))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Valida el XML generado contra los esquemas del SII (override para incluir validaciones adicionales).
    /// </summary>
    public override bool ValidateXml(XDocument xmlDocument)
    {
        // Validación base
        if (!base.ValidateXml(xmlDocument))
        {
            return false;
        }

        // Validaciones adicionales para documento firmado/timbrado
        var root = xmlDocument.Root;
        if (root == null)
        {
            return false;
        }

        // Verificar que tenga firma si es un documento procesado
        var signature = root.Descendants().FirstOrDefault(e => e.Name.LocalName == "Signature");
        if (signature != null)
        {
            // Es un documento firmado, verificar firma
            if (!_signatureService.VerifyXmlSignature(xmlDocument))
            {
                return false;
            }
        }

        // Verificar TED si existe
        var ted = _stampingService.ExtractTED(xmlDocument);
        if (ted != null)
        {
            if (!_stampingService.VerifyElectronicStamp(xmlDocument))
            {
                return false;
            }
        }

        return true;
    }
}