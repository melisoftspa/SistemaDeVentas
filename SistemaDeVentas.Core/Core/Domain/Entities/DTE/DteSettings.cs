using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Configuración para el manejo de Documentos Tributarios Electrónicos (DTE).
/// </summary>
public class DteSettings
{
    /// <summary>
    /// Ambiente del SII: 0 para certificación, 1 para producción.
    /// </summary>
    [Range(0, 1, ErrorMessage = "El ambiente debe ser 0 (certificación) o 1 (producción).")]
    public int Ambiente { get; set; }

    /// <summary>
    /// RUT del emisor en formato XX.XXX.XXX-X.
    /// </summary>
    [Required(ErrorMessage = "El RUT del emisor es obligatorio.")]
    [RegularExpression(@"^\d{1,2}\.\d{3}\.\d{3}-[\dKk]$", ErrorMessage = "El RUT debe tener el formato XX.XXX.XXX-X.")]
    public string RutEmisor { get; set; } = string.Empty;

    /// <summary>
    /// Ruta del archivo del certificado digital PKCS#12.
    /// </summary>
    [Required(ErrorMessage = "La ruta del certificado es obligatoria.")]
    public string CertificadoRuta { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del certificado digital.
    /// </summary>
    [Required(ErrorMessage = "La contraseña del certificado es obligatoria.")]
    public string CertificadoPassword { get; set; } = string.Empty;

    /// <summary>
    /// Directorio donde se almacenan los archivos CAF.
    /// </summary>
    public string CafDirectory { get; set; } = string.Empty;

    /// <summary>
    /// URL del servicio de timbrado del SII.
    /// </summary>
    [Required(ErrorMessage = "La URL del servicio de timbrado es obligatoria.")]
    public string ServicioTimbradoUrl { get; set; } = string.Empty;

    /// <summary>
    /// Timeout en segundos para las llamadas al SII.
    /// </summary>
    [Range(1, 300, ErrorMessage = "El timeout debe estar entre 1 y 300 segundos.")]
    public int TimeoutSegundos { get; set; } = 30;

    /// <summary>
    /// Valida que el RUT sea válido según el algoritmo chileno.
    /// </summary>
    /// <returns>True si el RUT es válido.</returns>
    public bool ValidarRut()
    {
        if (string.IsNullOrWhiteSpace(RutEmisor))
            return false;

        // Remover puntos y guión
        var rutLimpio = Regex.Replace(RutEmisor, @"[\.\-]", "");

        if (!Regex.IsMatch(rutLimpio, @"^\d+[0-9Kk]$"))
            return false;

        // Separar número y dígito verificador
        var numero = rutLimpio.Substring(0, rutLimpio.Length - 1);
        var dv = rutLimpio[^1].ToString().ToUpper();

        // Calcular dígito verificador
        var suma = 0;
        var multiplicador = 2;

        for (var i = numero.Length - 1; i >= 0; i--)
        {
            suma += int.Parse(numero[i].ToString()) * multiplicador;
            multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
        }

        var resto = suma % 11;
        var dvCalculado = (11 - resto).ToString();

        if (resto == 1)
            dvCalculado = "K";
        else if (resto == 0)
            dvCalculado = "0";

        return dv == dvCalculado;
    }

    /// <summary>
    /// Obtiene el RUT limpio (sin puntos ni guión) para uso interno.
    /// </summary>
    /// <returns>RUT limpio.</returns>
    public string ObtenerRutLimpio()
    {
        return Regex.Replace(RutEmisor, @"[\.\-]", "");
    }

    /// <summary>
    /// Valida toda la configuración.
    /// </summary>
    /// <returns>Lista de errores de validación.</returns>
    public List<string> Validar()
    {
        var errores = new List<string>();

        if (!ValidarRut())
            errores.Add("El RUT del emisor no es válido.");

        if (Ambiente != 0 && Ambiente != 1)
            errores.Add("El ambiente debe ser 0 (certificación) o 1 (producción).");

        if (string.IsNullOrWhiteSpace(CertificadoRuta))
            errores.Add("La ruta del certificado es obligatoria.");

        if (string.IsNullOrWhiteSpace(CertificadoPassword))
            errores.Add("La contraseña del certificado es obligatoria.");

        if (string.IsNullOrWhiteSpace(ServicioTimbradoUrl))
            errores.Add("La URL del servicio de timbrado es obligatoria.");

        if (TimeoutSegundos < 1 || TimeoutSegundos > 300)
            errores.Add("El timeout debe estar entre 1 y 300 segundos.");

        return errores;
    }
}