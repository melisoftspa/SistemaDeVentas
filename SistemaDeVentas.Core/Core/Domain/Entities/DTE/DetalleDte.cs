using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa el detalle de un documento tributario electrónico.
/// </summary>
[XmlRoot("Detalle")]
public class DetalleDte
{
    /// <summary>
    /// Número de línea del detalle.
    /// </summary>
    [XmlElement("NroLinDet")]
    [JsonPropertyName("NroLinDet")]
    public int NumeroLineaDetalle { get; set; }

    /// <summary>
    /// Código del item.
    /// </summary>
    [XmlElement("CdgItem")]
    [JsonPropertyName("CdgItem")]
    public CodigoItem? CodigoItem { get; set; }

    /// <summary>
    /// Nombre del item.
    /// </summary>
    [XmlElement("NmbItem")]
    [JsonPropertyName("NmbItem")]
    public string NombreItem { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del item.
    /// </summary>
    [XmlElement("DscItem")]
    [JsonPropertyName("DscItem")]
    public string? DescripcionItem { get; set; }

    /// <summary>
    /// Cantidad del item.
    /// </summary>
    [XmlElement("QtyItem")]
    [JsonPropertyName("QtyItem")]
    public decimal? CantidadItem { get; set; }

    /// <summary>
    /// Unidad de medida del item.
    /// </summary>
    [XmlElement("UnmdItem")]
    [JsonPropertyName("UnmdItem")]
    public string? UnidadMedidaItem { get; set; }

    /// <summary>
    /// Precio del item.
    /// </summary>
    [XmlElement("PrcItem")]
    [JsonPropertyName("PrcItem")]
    public decimal? PrecioItem { get; set; }

    /// <summary>
    /// Monto del item.
    /// </summary>
    [XmlElement("MontoItem")]
    [JsonPropertyName("MontoItem")]
    public decimal? MontoItem { get; set; }

    /// <summary>
    /// Indicador de exención.
    /// </summary>
    [XmlElement("IndExe")]
    [JsonPropertyName("IndExe")]
    public int? IndicadorExencion { get; set; }
}