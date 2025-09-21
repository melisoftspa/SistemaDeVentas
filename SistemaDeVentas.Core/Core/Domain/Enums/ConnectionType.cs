namespace SistemaDeVentas.Core.Domain.Enums;

/// <summary>
/// Enum para los tipos de conexión soportados por las impresoras térmicas.
/// </summary>
public enum ConnectionType
{
    /// <summary>
    /// Conexión USB.
    /// </summary>
    USB,

    /// <summary>
    /// Conexión Serial (RS-232).
    /// </summary>
    Serial,

    /// <summary>
    /// Conexión LAN (Ethernet).
    /// </summary>
    LAN
}