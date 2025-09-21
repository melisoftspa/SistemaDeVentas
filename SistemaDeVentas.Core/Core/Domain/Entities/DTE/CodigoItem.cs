using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa el código del item en el detalle.
/// </summary>
[XmlRoot("CdgItem")]
public class CodigoItem
{
    /// <summary>
    /// Tipo de código.
    /// </summary>
    [XmlElement("TpoCodigo")]
    [JsonPropertyName("TpoCodigo")]
    public string TipoCodigo { get; set; } = string.Empty;

    /// <summary>
    /// Valor del código.
    /// </summary>
    [XmlElement("VlrCodigo")]
    [JsonPropertyName("VlrCodigo")]
    public string ValorCodigo { get; set; } = string.Empty;
}