using System.Threading.Tasks;
using FluentResults;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Interfaz interna para implementaciones específicas de impresoras térmicas.
/// </summary>
internal interface IThermalPrinter
{
    /// <summary>
    /// Conecta a la impresora.
    /// </summary>
    Task<Result<bool>> ConnectAsync();

    /// <summary>
    /// Desconecta la impresora.
    /// </summary>
    Task<Result<bool>> DisconnectAsync();

    /// <summary>
    /// Imprime datos de texto.
    /// </summary>
    Task<Result<bool>> PrintAsync(string data);

    /// <summary>
    /// Imprime datos binarios (comandos ESC/POS).
    /// </summary>
    Task<Result<bool>> PrintRawAsync(byte[] data);

    /// <summary>
    /// Obtiene el estado de la impresora.
    /// </summary>
    Task<Result<string>> GetStatusAsync();

    /// <summary>
    /// Formatea datos de boleta.
    /// </summary>
    Task<Result<byte[]>> FormatReceiptDataAsync(Sale sale);

    /// <summary>
    /// Formatea datos de factura con DTE.
    /// </summary>
    Task<Result<byte[]>> FormatInvoiceDataAsync(DteDocument dteDocument, string qrCodeData);

    /// <summary>
    /// Corta el papel.
    /// </summary>
    Task<Result<bool>> CutPaperAsync();

    /// <summary>
    /// Inicializa la impresora.
    /// </summary>
    Task<Result<bool>> InitializeAsync();
}