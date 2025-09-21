using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Enum para la operación de los documentos en libros.
/// </summary>
public enum OperacionDocumento
{
    [XmlEnum("S")]
    [JsonPropertyName("Suma")]
    Suma = 0,

    [XmlEnum("R")]
    [JsonPropertyName("Resta")]
    Resta = 1
}