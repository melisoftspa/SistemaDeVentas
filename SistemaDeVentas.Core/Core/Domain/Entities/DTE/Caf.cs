using System.Xml.Linq;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa un Código de Autorización de Folios (CAF).
/// </summary>
public class Caf
{
    /// <summary>
    /// Identificador único del CAF.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Tipo de documento tributario.
    /// </summary>
    public int TipoDocumento { get; set; }

    /// <summary>
    /// Folio inicial autorizado.
    /// </summary>
    public int FolioDesde { get; set; }

    /// <summary>
    /// Folio final autorizado.
    /// </summary>
    public int FolioHasta { get; set; }

    /// <summary>
    /// Fecha de autorización del CAF.
    /// </summary>
    public DateTime FechaAutorizacion { get; set; }

    /// <summary>
    /// Fecha de vencimiento del CAF.
    /// </summary>
    public DateTime FechaVencimiento { get; set; }

    /// <summary>
    /// Contenido XML del CAF.
    /// </summary>
    public string XmlContent { get; set; } = string.Empty;

    /// <summary>
    /// Ambiente del SII (0 = certificación, 1 = producción).
    /// </summary>
    public int Ambiente { get; set; }

    /// <summary>
    /// RUT del emisor.
    /// </summary>
    public string RutEmisor { get; set; } = string.Empty;

    /// <summary>
    /// Folio actual utilizado (para tracking).
    /// </summary>
    public int FolioActual { get; set; }

    /// <summary>
    /// Indica si el CAF está activo.
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Verifica si un folio está dentro del rango autorizado.
    /// </summary>
    /// <param name="folio">Folio a verificar.</param>
    /// <returns>True si está en rango.</returns>
    public bool EstaEnRango(int folio)
    {
        return folio >= FolioDesde && folio <= FolioHasta;
    }

    /// <summary>
    /// Verifica si el CAF está vigente en una fecha específica.
    /// </summary>
    /// <param name="fecha">Fecha a verificar.</param>
    /// <returns>True si está vigente.</returns>
    public bool EstaVigente(DateTime fecha)
    {
        return fecha >= FechaAutorizacion && fecha <= FechaVencimiento;
    }

    /// <summary>
    /// Obtiene el siguiente folio disponible.
    /// </summary>
    /// <returns>Siguiente folio.</returns>
    public int ObtenerSiguienteFolio()
    {
        if (FolioActual >= FolioHasta)
            throw new InvalidOperationException("No hay folios disponibles en este CAF.");

        return ++FolioActual;
    }

    /// <summary>
    /// Obtiene la cantidad de folios disponibles.
    /// </summary>
    /// <returns>Cantidad de folios disponibles.</returns>
    public int ObtenerFoliosDisponibles()
    {
        return FolioHasta - FolioActual;
    }

    /// <summary>
    /// Carga el CAF desde contenido XML.
    /// </summary>
    /// <param name="xmlContent">Contenido XML del CAF.</param>
    public void CargarDesdeXml(string xmlContent)
    {
        XmlContent = xmlContent;
        var doc = XDocument.Parse(xmlContent);

        // Extraer datos del XML (simplificado)
        var da = doc.Descendants("DA").FirstOrDefault();
        if (da != null)
        {
            TipoDocumento = int.Parse(da.Element("TD")?.Value ?? "0");
            FolioDesde = int.Parse(da.Element("RNG")?.Element("D")?.Value ?? "0");
            FolioHasta = int.Parse(da.Element("RNG")?.Element("H")?.Value ?? "0");
            FechaAutorizacion = DateTime.Parse(da.Element("FA")?.Value ?? DateTime.MinValue.ToString());
            RutEmisor = da.Element("RE")?.Value ?? string.Empty;
        }
    }
}