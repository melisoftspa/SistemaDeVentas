using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa los totales de un documento tributario electrónico.
/// </summary>
[XmlRoot("Totales")]
public class TotalesDte
{
    /// <summary>
    /// Monto neto.
    /// </summary>
    [XmlElement("MntNeto")]
    [JsonPropertyName("MntNeto")]
    public decimal? MontoNeto { get; set; }

    /// <summary>
    /// Monto exento.
    /// </summary>
    [XmlElement("MntExe")]
    [JsonPropertyName("MntExe")]
    public decimal? MontoExento { get; set; }

    /// <summary>
    /// Tasa IVA.
    /// </summary>
    [XmlElement("TasaIVA")]
    [JsonPropertyName("TasaIVA")]
    public decimal? TasaIVA { get; set; }

    /// <summary>
    /// IVA.
    /// </summary>
    [XmlElement("IVA")]
    [JsonPropertyName("IVA")]
    public decimal? IVA { get; set; }

    /// <summary>
    /// Monto total.
    /// </summary>
    [XmlElement("MntTotal")]
    [JsonPropertyName("MntTotal")]
    public decimal MontoTotal { get; set; }
}