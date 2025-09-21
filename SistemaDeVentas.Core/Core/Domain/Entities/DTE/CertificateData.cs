namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa los datos de un certificado digital almacenado en la base de datos.
/// </summary>
public class CertificateData
{
    /// <summary>
    /// Identificador único del certificado.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del certificado.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// RUT del emisor al que pertenece el certificado.
    /// </summary>
    public string RutEmisor { get; set; } = string.Empty;

    /// <summary>
    /// Ambiente del SII (0 = certificación, 1 = producción).
    /// </summary>
    public int Ambiente { get; set; }

    /// <summary>
    /// Datos binarios del certificado PKCS#12.
    /// </summary>
    public byte[] DatosCertificado { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Contraseña del certificado (encriptada).
    /// </summary>
    public string PasswordEncriptado { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de emisión del certificado.
    /// </summary>
    public DateTime FechaEmision { get; set; }

    /// <summary>
    /// Fecha de vencimiento del certificado.
    /// </summary>
    public DateTime FechaVencimiento { get; set; }

    /// <summary>
    /// Indica si el certificado está activo.
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Fecha de creación del registro.
    /// </summary>
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    /// <summary>
    /// Verifica si el certificado está vigente.
    /// </summary>
    /// <returns>True si está vigente.</returns>
    public bool EstaVigente()
    {
        var ahora = DateTime.Now;
        return Activo && ahora >= FechaEmision && ahora <= FechaVencimiento;
    }
}