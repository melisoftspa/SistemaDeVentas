using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa al receptor de un documento tributario electrónico.
/// </summary>
[XmlRoot("Receptor")]
public class Receptor
{
    /// <summary>
    /// RUT del receptor.
    /// </summary>
    [XmlElement("RUTRecep")]
    [JsonPropertyName("RUTRecep")]
    public string RutReceptor { get; set; } = string.Empty;

    /// <summary>
    /// Razón social del receptor.
    /// </summary>
    [XmlElement("RznSocRecep")]
    [JsonPropertyName("RznSocRecep")]
    public string RazonSocialReceptor { get; set; } = string.Empty;

    /// <summary>
    /// Giro comercial del receptor.
    /// </summary>
    [XmlElement("GiroRecep")]
    [JsonPropertyName("GiroRecep")]
    public string? GiroReceptor { get; set; }

    /// <summary>
    /// Dirección del receptor.
    /// </summary>
    [XmlElement("DirRecep")]
    [JsonPropertyName("DirRecep")]
    public string? DireccionReceptor { get; set; }

    /// <summary>
    /// Comuna del receptor.
    /// </summary>
    [XmlElement("CmnaRecep")]
    [JsonPropertyName("CmnaRecep")]
    public string? ComunaReceptor { get; set; }

    /// <summary>
    /// Ciudad del receptor.
    /// </summary>
    [XmlElement("CiudadRecep")]
    [JsonPropertyName("CiudadRecep")]
    public string? CiudadReceptor { get; set; }

    /// <summary>
    /// Correo electrónico del receptor.
    /// </summary>
    [XmlElement("CorreoRecep")]
    [JsonPropertyName("CorreoRecep")]
    public string? CorreoReceptor { get; set; }

    /// <summary>
    /// Contacto del receptor.
    /// </summary>
    [XmlElement("Contacto")]
    [JsonPropertyName("Contacto")]
    public string? Contacto { get; set; }

    /// <summary>
    /// Código interno del receptor.
    /// </summary>
    [XmlElement("CdgIntRecep")]
    [JsonPropertyName("CdgIntRecep")]
    public string? CodigoInternoReceptor { get; set; }
}