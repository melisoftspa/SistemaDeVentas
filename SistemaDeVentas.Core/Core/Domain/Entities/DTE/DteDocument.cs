using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa un documento tributario electrónico.
/// </summary>
[XmlRoot("Documento")]
public class DteDocument
{
    /// <summary>
    /// Identificación del documento.
    /// </summary>
    [XmlElement("IdDoc")]
    [JsonPropertyName("IdDoc")]
    public IdDoc IdDoc { get; set; } = new();

    /// <summary>
    /// Emisor del documento.
    /// </summary>
    [XmlElement("Emisor")]
    [JsonPropertyName("Emisor")]
    public Emisor Emisor { get; set; } = new();

    /// <summary>
    /// Receptor del documento.
    /// </summary>
    [XmlElement("Receptor")]
    [JsonPropertyName("Receptor")]
    public Receptor Receptor { get; set; } = new();

    /// <summary>
    /// Totales del documento.
    /// </summary>
    [XmlElement("Totales")]
    [JsonPropertyName("Totales")]
    public TotalesDte Totales { get; set; } = new();

    /// <summary>
    /// Detalles del documento.
    /// </summary>
    [XmlElement("Detalle")]
    [JsonPropertyName("Detalle")]
    public List<DetalleDte> Detalles { get; set; } = new();
}