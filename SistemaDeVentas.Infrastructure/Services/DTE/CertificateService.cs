using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Infrastructure.Data;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio para manejo de certificados digitales PKCS#12.
/// </summary>
public class CertificateService : ICertificateService
{
    private readonly SalesSystemDbContext _context;

    public CertificateService(SalesSystemDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Carga un certificado PKCS#12 desde un archivo.
    /// </summary>
    /// <param name="filePath">Ruta del archivo del certificado.</param>
    /// <param name="password">Contraseña del certificado.</param>
    /// <returns>El certificado cargado.</returns>
    /// <exception cref="FileNotFoundException">Si el archivo no existe.</exception>
    /// <exception cref="CryptographicException">Si hay error al cargar el certificado.</exception>
    public X509Certificate2 LoadCertificateFromFile(string filePath, string password)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("El archivo del certificado no existe.", filePath);
        }

        try
        {
            var certificate = new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable);
            return certificate;
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Error al cargar el certificado desde archivo.", ex);
        }
    }

    /// <summary>
    /// Carga un certificado PKCS#12 desde bytes.
    /// </summary>
    /// <param name="certificateData">Datos del certificado en bytes.</param>
    /// <param name="password">Contraseña del certificado.</param>
    /// <returns>El certificado cargado.</returns>
    /// <exception cref="CryptographicException">Si hay error al cargar el certificado.</exception>
    public X509Certificate2 LoadCertificateFromBytes(byte[] certificateData, string password)
    {
        try
        {
            var certificate = new X509Certificate2(certificateData, password, X509KeyStorageFlags.Exportable);
            return certificate;
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Error al cargar el certificado desde bytes.", ex);
        }
    }

    /// <summary>
    /// Valida un certificado para uso en firma digital.
    /// </summary>
    /// <param name="certificate">El certificado a validar.</param>
    /// <returns>True si es válido para firma digital.</returns>
    public bool ValidateCertificateForSigning(X509Certificate2 certificate)
    {
        // Verificar que no haya expirado
        if (DateTime.Now < certificate.NotBefore || DateTime.Now > certificate.NotAfter)
        {
            return false;
        }

        // Verificar que tenga clave privada
        if (!certificate.HasPrivateKey)
        {
            return false;
        }

        // Verificar que sea para firma digital (KeyUsage)
        foreach (var extension in certificate.Extensions)
        {
            if (extension is X509KeyUsageExtension keyUsage)
            {
                // Debe tener DigitalSignature o NonRepudiation
                if ((keyUsage.KeyUsages & X509KeyUsageFlags.DigitalSignature) != X509KeyUsageFlags.DigitalSignature &&
                    (keyUsage.KeyUsages & X509KeyUsageFlags.NonRepudiation) != X509KeyUsageFlags.NonRepudiation)
                {
                    return false;
                }
                break;
            }
        }

        // Verificar que sea RSA (común para certificados chilenos)
        using var rsa = certificate.GetRSAPrivateKey();
        if (rsa == null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Obtiene la clave privada RSA del certificado.
    /// </summary>
    /// <param name="certificate">El certificado.</param>
    /// <returns>La clave privada RSA.</returns>
    /// <exception cref="InvalidOperationException">Si no tiene clave privada RSA.</exception>
    public RSA GetPrivateKey(X509Certificate2 certificate)
    {
        var rsa = certificate.GetRSAPrivateKey();
        if (rsa == null)
        {
            throw new InvalidOperationException("El certificado no contiene una clave privada RSA válida.");
        }
        return rsa;
    }

    /// <summary>
    /// Carga un certificado desde la base de datos.
    /// </summary>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <param name="ambiente">Ambiente del SII.</param>
    /// <returns>El certificado cargado.</returns>
    /// <exception cref="InvalidOperationException">Si no se encuentra un certificado válido.</exception>
    public async Task<X509Certificate2> LoadCertificateFromDatabaseAsync(string rutEmisor, int ambiente)
    {
        var certificateData = await _context.CertificateDatas
            .Where(c => c.RutEmisor == rutEmisor && c.Ambiente == ambiente && c.Activo && c.EstaVigente())
            .OrderByDescending(c => c.FechaVencimiento)
            .FirstOrDefaultAsync();

        if (certificateData == null)
        {
            throw new InvalidOperationException($"No se encontró un certificado válido para RUT {rutEmisor} en ambiente {ambiente}.");
        }

        // Nota: En producción, la contraseña debería estar encriptada
        return LoadCertificateFromBytes(certificateData.DatosCertificado, certificateData.PasswordEncriptado);
    }

    /// <summary>
    /// Guarda un certificado en la base de datos.
    /// </summary>
    /// <param name="certificateData">Datos del certificado a guardar.</param>
    /// <returns>ID del certificado guardado.</returns>
    public async Task<int> SaveCertificateToDatabaseAsync(CertificateData certificateData)
    {
        _context.CertificateDatas.Add(certificateData);
        await _context.SaveChangesAsync();
        return certificateData.Id;
    }

    /// <summary>
    /// Convierte una clave RSA de .NET a parámetros BouncyCastle.
    /// </summary>
    /// <param name="rsa">La clave RSA.</param>
    /// <returns>Los parámetros de la clave privada.</returns>
    public static RsaPrivateCrtKeyParameters ConvertToBouncyCastle(RSA rsa)
    {
        var parameters = rsa.ExportParameters(true);
        return new RsaPrivateCrtKeyParameters(
            new Org.BouncyCastle.Math.BigInteger(1, parameters.Modulus),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.Exponent),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.D),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.P),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.Q),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.DP),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.DQ),
            new Org.BouncyCastle.Math.BigInteger(1, parameters.InverseQ)
        );
    }
}