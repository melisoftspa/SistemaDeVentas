using System.Security.Cryptography.X509Certificates;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Interfaz para servicios de manejo de certificados digitales.
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Carga un certificado PKCS#12 desde un archivo.
    /// </summary>
    /// <param name="filePath">Ruta del archivo del certificado.</param>
    /// <param name="password">Contraseña del certificado.</param>
    /// <returns>El certificado cargado.</returns>
    X509Certificate2 LoadCertificateFromFile(string filePath, string password);

    /// <summary>
    /// Carga un certificado PKCS#12 desde bytes.
    /// </summary>
    /// <param name="certificateData">Datos del certificado en bytes.</param>
    /// <param name="password">Contraseña del certificado.</param>
    /// <returns>El certificado cargado.</returns>
    X509Certificate2 LoadCertificateFromBytes(byte[] certificateData, string password);

    /// <summary>
    /// Carga un certificado desde la base de datos.
    /// </summary>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <param name="ambiente">Ambiente del SII.</param>
    /// <returns>El certificado cargado.</returns>
    Task<X509Certificate2> LoadCertificateFromDatabaseAsync(string rutEmisor, int ambiente);

    /// <summary>
    /// Guarda un certificado en la base de datos.
    /// </summary>
    /// <param name="certificateData">Datos del certificado a guardar.</param>
    /// <returns>ID del certificado guardado.</returns>
    Task<int> SaveCertificateToDatabaseAsync(CertificateData certificateData);

    /// <summary>
    /// Valida un certificado para uso en firma digital.
    /// </summary>
    /// <param name="certificate">El certificado a validar.</param>
    /// <returns>True si es válido para firma digital.</returns>
    bool ValidateCertificateForSigning(X509Certificate2 certificate);

    /// <summary>
    /// Obtiene la clave privada RSA del certificado.
    /// </summary>
    /// <param name="certificate">El certificado.</param>
    /// <returns>La clave privada RSA.</returns>
    System.Security.Cryptography.RSA GetPrivateKey(X509Certificate2 certificate);
}