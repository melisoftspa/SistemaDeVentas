using System.Threading.Tasks;
using FluentResults;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    public interface IThermalPrinterService
    {
        /// <summary>
        /// Conecta a la impresora térmica especificada.
        /// </summary>
        /// <param name="printerName">Nombre de la impresora.</param>
        /// <returns>Resultado de la operación de conexión.</returns>
        Task<Result<bool>> ConnectAsync(string printerName);

        /// <summary>
        /// Desconecta la impresora térmica actual.
        /// </summary>
        /// <returns>Resultado de la operación de desconexión.</returns>
        Task<Result<bool>> DisconnectAsync();

        /// <summary>
        /// Imprime los datos especificados en la impresora térmica (encolado).
        /// </summary>
        /// <param name="data">Datos a imprimir.</param>
        /// <param name="priority">Prioridad del trabajo de impresión.</param>
        /// <returns>ID del trabajo de impresión encolado.</returns>
        Task<Result<Guid>> PrintAsync(string data, PrintJobPriority priority = PrintJobPriority.Normal);

        /// <summary>
        /// Imprime datos binarios (comandos ESC/POS) en la impresora térmica (encolado).
        /// </summary>
        /// <param name="data">Datos binarios a imprimir.</param>
        /// <param name="priority">Prioridad del trabajo de impresión.</param>
        /// <returns>ID del trabajo de impresión encolado.</returns>
        Task<Result<Guid>> PrintRawAsync(byte[] data, PrintJobPriority priority = PrintJobPriority.Normal);

        /// <summary>
        /// Obtiene el estado actual de la impresora térmica.
        /// </summary>
        /// <returns>Estado de la impresora como cadena.</returns>
        Task<Result<string>> GetStatusAsync();

        /// <summary>
        /// Verifica si el modelo de impresora es compatible.
        /// </summary>
        /// <param name="printerModel">Modelo de la impresora.</param>
        /// <returns>Resultado de la verificación de compatibilidad.</returns>
        Task<Result<bool>> IsCompatibleAsync(string printerModel);

        /// <summary>
        /// Formatea los datos de una boleta de venta para impresión térmica.
        /// </summary>
        /// <param name="sale">Venta a formatear.</param>
        /// <returns>Datos formateados para impresión.</returns>
        Task<Result<byte[]>> FormatReceiptDataAsync(Sale sale);

        /// <summary>
        /// Formatea los datos de una factura con DTE para impresión térmica.
        /// </summary>
        /// <param name="dteDocument">Documento DTE a formatear.</param>
        /// <param name="qrCodeData">Datos para el código QR.</param>
        /// <returns>Datos formateados para impresión.</returns>
        Task<Result<byte[]>> FormatInvoiceDataAsync(DteDocument dteDocument, string qrCodeData);

        /// <summary>
        /// Imprime una boleta de venta (encolado con prioridad normal).
        /// </summary>
        /// <param name="sale">Venta a imprimir.</param>
        /// <returns>ID del trabajo de impresión encolado.</returns>
        Task<Result<Guid>> PrintReceiptAsync(Sale sale);

        /// <summary>
        /// Imprime una factura con DTE (encolado con prioridad alta).
        /// </summary>
        /// <param name="dteDocument">Documento DTE a imprimir.</param>
        /// <param name="qrCodeData">Datos para el código QR.</param>
        /// <returns>ID del trabajo de impresión encolado.</returns>
        Task<Result<Guid>> PrintInvoiceAsync(DteDocument dteDocument, string qrCodeData);

        /// <summary>
        /// Ejecuta un corte de papel.
        /// </summary>
        /// <returns>Resultado de la operación de corte.</returns>
        Task<Result<bool>> CutPaperAsync();

        /// <summary>
        /// Inicializa la impresora con comandos ESC/POS.
        /// </summary>
        /// <returns>Resultado de la inicialización.</returns>
        Task<Result<bool>> InitializeAsync();
    }
}