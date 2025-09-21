using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa la identificación del documento.
/// </summary>
[XmlRoot("IdDoc")]
public class IdDoc
{
    /// <summary>
    /// Tipo de DTE.
    /// </summary>
    [XmlElement("TipoDTE")]
    [JsonPropertyName("TipoDTE")]
    public TipoDte TipoDTE { get; set; }

    /// <summary>
    /// Folio del documento.
    /// </summary>
    [XmlElement("Folio")]
    [JsonPropertyName("Folio")]
    public int Folio { get; set; }

    /// <summary>
    /// Fecha de emisión.
    /// </summary>
    [XmlElement("FchEmis")]
    [JsonPropertyName("FchEmis")]
    public DateTime FechaEmision { get; set; }

    /// <summary>
    /// Fecha de vencimiento.
    /// </summary>
    [XmlElement("FchVenc")]
    [JsonPropertyName("FchVenc")]
    public DateTime? FechaVencimiento { get; set; }

    /// <summary>
    /// Forma de pago.
    /// </summary>
    [XmlElement("FmaPago")]
    [JsonPropertyName("FmaPago")]
    public int? FormaPago { get; set; }

    /// <summary>
    /// Indicador de traslado.
    /// </summary>
    [XmlElement("IndTraslado")]
    [JsonPropertyName("IndTraslado")]
    public int? IndicadorTraslado { get; set; }

    /// <summary>
    /// Indicador de servicio (para boletas).
    /// </summary>
    [XmlElement("IndServicio")]
    [JsonPropertyName("IndServicio")]
    public int? IndicadorServicio { get; set; }
}