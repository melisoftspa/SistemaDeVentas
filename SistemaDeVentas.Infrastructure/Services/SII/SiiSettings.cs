namespace SistemaDeVentas.Infrastructure.Services.SII;

/// <summary>
/// Configuraciones para los servicios del SII.
/// </summary>
public class SiiSettings
{
    /// <summary>
    /// Ambiente de certificación.
    /// </summary>
    public const int AmbienteCertificacion = 1;

    /// <summary>
    /// Ambiente de producción.
    /// </summary>
    public const int AmbienteProduccion = 0;

    /// <summary>
    /// URLs base para servicios del SII.
    /// </summary>
    public static class Urls
    {
        /// <summary>
        /// URL base para certificación.
        /// </summary>
        public const string Certificacion = "https://palena.sii.cl";

        /// <summary>
        /// URL base para producción.
        /// </summary>
        public const string Produccion = "https://maullin.sii.cl";

        /// <summary>
        /// Servicio de semillas.
        /// </summary>
        public const string CrSeed = "/DTEWS/CrSeed.jws";

        /// <summary>
        /// Servicio de tokens.
        /// </summary>
        public const string GetTokenFromSeed = "/DTEWS/GetTokenFromSeed.jws";

        /// <summary>
        /// Servicio de recepción de DTE.
        /// </summary>
        public const string RecibeDTE = "/DTEWS/RecibeDTE.jws";

        /// <summary>
        /// Servicio de consulta de estado.
        /// </summary>
        public const string QueryEstDte = "/DTEWS/QueryEstDte.jws";

        /// <summary>
        /// Servicio de timbrado electrónico.
        /// </summary>
        public const string Timbrado = "/recursos/v1/timbrado";
    }

    /// <summary>
    /// Obtiene la URL completa para un servicio según el ambiente.
    /// </summary>
    /// <param name="ambiente">Ambiente (0=producción, 1=certificación).</param>
    /// <param name="servicePath">Ruta del servicio.</param>
    /// <returns>URL completa.</returns>
    public static string GetServiceUrl(int ambiente, string servicePath)
    {
        var baseUrl = ambiente == AmbienteCertificacion ? Urls.Certificacion : Urls.Produccion;
        return baseUrl + servicePath;
    }

    /// <summary>
    /// Timeout por defecto para llamadas al SII (en segundos).
    /// </summary>
    public const int DefaultTimeoutSeconds = 30;

    /// <summary>
    /// Indica si está en modo simulado (para desarrollo/testing).
    /// </summary>
    public static bool IsSimulatedMode { get; set; } = false;
}