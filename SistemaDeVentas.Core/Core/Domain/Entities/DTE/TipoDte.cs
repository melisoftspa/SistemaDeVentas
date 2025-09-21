using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Enum para los tipos de documentos tributarios electrónicos según estándares del SII chileno.
/// </summary>
public enum TipoDte
{
    [XmlEnum("33")]
    [JsonPropertyName("33")]
    FacturaAfecta = 33,

    [XmlEnum("34")]
    [JsonPropertyName("34")]
    FacturaExenta = 34,

    [XmlEnum("39")]
    [JsonPropertyName("39")]
    BoletaAfecta = 39,

    [XmlEnum("41")]
    [JsonPropertyName("41")]
    BoletaExenta = 41,

    [XmlEnum("43")]
    [JsonPropertyName("43")]
    LiquidacionFactura = 43,

    [XmlEnum("46")]
    [JsonPropertyName("46")]
    FacturaCompra = 46,

    [XmlEnum("52")]
    [JsonPropertyName("52")]
    GuiaDespacho = 52,

    [XmlEnum("56")]
    [JsonPropertyName("56")]
    NotaDebito = 56,

    [XmlEnum("61")]
    [JsonPropertyName("61")]
    NotaCredito = 61,

    [XmlEnum("110")]
    [JsonPropertyName("110")]
    FacturaExportacion = 110,

    [XmlEnum("111")]
    [JsonPropertyName("111")]
    NotaDebitoExportacion = 111,

    [XmlEnum("112")]
    [JsonPropertyName("112")]
    NotaCreditoExportacion = 112
}