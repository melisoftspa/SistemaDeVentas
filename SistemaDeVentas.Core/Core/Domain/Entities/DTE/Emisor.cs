using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa al emisor de un documento tributario electrónico.
/// </summary>
[XmlRoot("Emisor")]
public class Emisor
{
    /// <summary>
    /// RUT del emisor.
    /// </summary>
    [XmlElement("RUTEmisor")]
    [JsonPropertyName("RUTEmisor")]
    public string RutEmisor { get; set; } = string.Empty;

    /// <summary>
    /// Razón social del emisor.
    /// </summary>
    [XmlElement("RznSoc")]
    [JsonPropertyName("RznSoc")]
    public string RazonSocial { get; set; } = string.Empty;

    /// <summary>
    /// Giro comercial del emisor.
    /// </summary>
    [XmlElement("GiroEmis")]
    [JsonPropertyName("GiroEmis")]
    public string GiroEmisor { get; set; } = string.Empty;

    /// <summary>
    /// Código de actividad económica del emisor.
    /// </summary>
    [XmlElement("Acteco")]
    [JsonPropertyName("Acteco")]
    public int ActividadEconomica { get; set; }

    /// <summary>
    /// Código de sucursal del SII.
    /// </summary>
    [XmlElement("CdgSIISucur")]
    [JsonPropertyName("CdgSIISucur")]
    public int? CodigoSucursalSII { get; set; }

    /// <summary>
    /// Dirección de origen.
    /// </summary>
    [XmlElement("DirOrigen")]
    [JsonPropertyName("DirOrigen")]
    public string? DireccionOrigen { get; set; }

    /// <summary>
    /// Comuna de origen.
    /// </summary>
    [XmlElement("CmnaOrigen")]
    [JsonPropertyName("CmnaOrigen")]
    public string? ComunaOrigen { get; set; }

    /// <summary>
    /// Ciudad de origen.
    /// </summary>
    [XmlElement("CiudadOrigen")]
    [JsonPropertyName("CiudadOrigen")]
    public string? CiudadOrigen { get; set; }

    /// <summary>
    /// Correo electrónico del emisor.
    /// </summary>
    [XmlElement("CorreoEmisor")]
    [JsonPropertyName("CorreoEmisor")]
    public string? CorreoEmisor { get; set; }

    /// <summary>
    /// Teléfono del emisor.
    /// </summary>
    [XmlElement("Telefono")]
    [JsonPropertyName("Telefono")]
    public string? Telefono { get; set; }
}