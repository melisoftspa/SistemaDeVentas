using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Logging;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Entities.Printer;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Implementación específica para impresora térmica 3nStar RPT008 con comandos ESC/POS.
/// </summary>
public class Rpt008ThermalPrinter : IThermalPrinter
{
    private readonly ThermalPrinterSettings _settings;
    private readonly ILogger _logger;
    private SerialPort? _serialPort;
    private TcpClient? _tcpClient;
    private NetworkStream? _networkStream;
    private bool _isConnected;

    // Comandos ESC/POS comunes
    private static class EscPosCommands
    {
        // Inicialización
        public static readonly byte[] Initialize = { 0x1B, 0x40 }; // ESC @

        // Formato de texto
        public static readonly byte[] BoldOn = { 0x1B, 0x45, 0x01 }; // ESC E 1
        public static readonly byte[] BoldOff = { 0x1B, 0x45, 0x00 }; // ESC E 0
        public static readonly byte[] DoubleHeightOn = { 0x1B, 0x21, 0x10 }; // ESC ! (modo doble altura)
        public static readonly byte[] DoubleWidthOn = { 0x1B, 0x21, 0x20 }; // ESC ! (modo doble ancho)
        public static readonly byte[] NormalSize = { 0x1B, 0x21, 0x00 }; // ESC ! (tamaño normal)

        // Alineación
        public static readonly byte[] AlignLeft = { 0x1B, 0x61, 0x00 }; // ESC a 0
        public static readonly byte[] AlignCenter = { 0x1B, 0x61, 0x01 }; // ESC a 1
        public static readonly byte[] AlignRight = { 0x1B, 0x61, 0x02 }; // ESC a 2

        // Corte de papel
        public static readonly byte[] CutPaper = { 0x1D, 0x56, 0x42, 0x00 }; // GS V B 0 (corte completo)

        // Estado de impresora
        public static readonly byte[] GetStatus = { 0x10, 0x04, 0x01 }; // DLE EOT 1

        // Nueva línea
        public static readonly byte[] NewLine = { 0x0A }; // LF

        // Código QR (simplificado - GS k para códigos de barras)
        public static readonly byte[] QrCodeStart = { 0x1D, 0x6B, 0x71 }; // GS k q (tipo QR)
    }

    public Rpt008ThermalPrinter(ThermalPrinterSettings settings, ILogger logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<Result<bool>> ConnectAsync()
    {
        try
        {
            switch (_settings.ConnectionType)
            {
                case SistemaDeVentas.Core.Domain.Enums.ConnectionType.USB:
                    // Para USB, usar puerto serial virtual
                    return await ConnectSerialAsync();

                case SistemaDeVentas.Core.Domain.Enums.ConnectionType.Serial:
                    return await ConnectSerialAsync();

                case SistemaDeVentas.Core.Domain.Enums.ConnectionType.LAN:
                    return await ConnectNetworkAsync();

                default:
                    return Result.Fail("Tipo de conexión no soportado");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar a la impresora");
            return Result.Fail($"Error de conexión: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DisconnectAsync()
    {
        try
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
            }

            if (_networkStream != null)
            {
                _networkStream.Close();
                _networkStream = null;
            }

            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }

            _isConnected = false;
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al desconectar la impresora");
            return Result.Fail($"Error al desconectar: {ex.Message}");
        }
    }

    public async Task<Result<bool>> PrintAsync(string data)
    {
        if (!_isConnected)
        {
            return Result.Fail("Impresora no conectada");
        }

        try
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            return await SendDataAsync(bytes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al imprimir texto");
            return Result.Fail($"Error al imprimir: {ex.Message}");
        }
    }

    public async Task<Result<bool>> PrintRawAsync(byte[] data)
    {
        if (!_isConnected)
        {
            return Result.Fail("Impresora no conectada");
        }

        try
        {
            return await SendDataAsync(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al imprimir datos binarios");
            return Result.Fail($"Error al imprimir: {ex.Message}");
        }
    }

    public async Task<Result<string>> GetStatusAsync()
    {
        if (!_isConnected)
        {
            return Result.Ok("Desconectado");
        }

        try
        {
            var result = await SendDataAsync(EscPosCommands.GetStatus);
            if (result.IsFailed)
            {
                return Result.Fail("Error al consultar estado");
            }

            // En una implementación real, leeríamos la respuesta
            // Por simplicidad, devolver estado básico
            return Result.Ok("Conectado - Listo");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado");
            return Result.Fail($"Error al obtener estado: {ex.Message}");
        }
    }

    public async Task<Result<byte[]>> FormatReceiptDataAsync(Sale sale)
    {
        try
        {
            var data = new List<byte>();

            // Inicializar impresora
            data.AddRange(EscPosCommands.Initialize);

            // Encabezado centrado
            data.AddRange(EscPosCommands.AlignCenter);
            data.AddRange(EscPosCommands.BoldOn);
            data.AddRange(Encoding.UTF8.GetBytes("BOLETA DE VENTA\n"));
            data.AddRange(EscPosCommands.BoldOff);

            // Información de la venta
            data.AddRange(EscPosCommands.AlignLeft);
            data.AddRange(Encoding.UTF8.GetBytes($"Fecha: {sale.Date:dd/MM/yyyy HH:mm}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"ID Venta: {sale.Id}\n"));
            data.AddRange(Encoding.UTF8.GetBytes(new string('-', _settings.PaperWidth) + "\n"));

            // Detalles de productos
            foreach (var detail in sale.Details)
            {
                var productName = detail.Product?.Name ?? "Producto";
                var quantity = detail.Amount;
                var price = detail.Total;
                var line = $"{productName,-20} {quantity,3} x {price,8:F0}\n";
                data.AddRange(Encoding.UTF8.GetBytes(line));
            }

            data.AddRange(Encoding.UTF8.GetBytes(new string('-', _settings.PaperWidth) + "\n"));

            // Totales
            data.AddRange(EscPosCommands.AlignRight);
            data.AddRange(Encoding.UTF8.GetBytes($"Subtotal: {sale.Subtotal:F0}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"IVA: {sale.TotalTax:F0}\n"));
            data.AddRange(EscPosCommands.BoldOn);
            data.AddRange(Encoding.UTF8.GetBytes($"TOTAL: {sale.Total:F0}\n"));
            data.AddRange(EscPosCommands.BoldOff);

            // Pago
            data.AddRange(EscPosCommands.AlignLeft);
            data.AddRange(Encoding.UTF8.GetBytes($"Pago: {sale.PaymentMethodText}\n"));
            if (sale.Change > 0)
            {
                data.AddRange(Encoding.UTF8.GetBytes($"Vuelto: {sale.Change:F0}\n"));
            }

            // Espacio y corte
            data.AddRange(Encoding.UTF8.GetBytes("\n\n"));
            data.AddRange(EscPosCommands.CutPaper);

            return Result.Ok(data.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al formatear boleta");
            return Result.Fail($"Error al formatear boleta: {ex.Message}");
        }
    }

    public async Task<Result<byte[]>> FormatInvoiceDataAsync(DteDocument dteDocument, string qrCodeData)
    {
        try
        {
            var data = new List<byte>();

            // Inicializar impresora
            data.AddRange(EscPosCommands.Initialize);

            // Encabezado centrado
            data.AddRange(EscPosCommands.AlignCenter);
            data.AddRange(EscPosCommands.BoldOn);
            data.AddRange(Encoding.UTF8.GetBytes("FACTURA ELECTRÓNICA\n"));
            data.AddRange(EscPosCommands.BoldOff);

            // Información del DTE
            data.AddRange(EscPosCommands.AlignLeft);
            data.AddRange(Encoding.UTF8.GetBytes($"Folio: {dteDocument.IdDoc.Folio}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"Fecha: {dteDocument.IdDoc.FechaEmision:dd/MM/yyyy}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"Tipo: {dteDocument.IdDoc.TipoDTE}\n"));
            data.AddRange(Encoding.UTF8.GetBytes(new string('-', _settings.PaperWidth) + "\n"));

            // Emisor
            data.AddRange(Encoding.UTF8.GetBytes($"Emisor: {dteDocument.Emisor.RazonSocial}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"RUT: {dteDocument.Emisor.RutEmisor}\n"));

            // Receptor
            data.AddRange(Encoding.UTF8.GetBytes($"Cliente: {dteDocument.Receptor.RazonSocialReceptor}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"RUT: {dteDocument.Receptor.RutReceptor}\n"));
            data.AddRange(Encoding.UTF8.GetBytes(new string('-', _settings.PaperWidth) + "\n"));

            // Detalles
            foreach (var detalle in dteDocument.Detalles)
            {
                var nombre = detalle.NombreItem.Length > 20 ? detalle.NombreItem.Substring(0, 20) : detalle.NombreItem;
                var cantidad = detalle.CantidadItem ?? 0;
                var precio = detalle.PrecioItem ?? 0;
                var total = detalle.MontoItem ?? 0;
                var line = $"{nombre,-20} {cantidad,3} x {precio,8:F0} = {total,8:F0}\n";
                data.AddRange(Encoding.UTF8.GetBytes(line));
            }

            data.AddRange(Encoding.UTF8.GetBytes(new string('-', _settings.PaperWidth) + "\n"));

            // Totales
            data.AddRange(EscPosCommands.AlignRight);
            data.AddRange(Encoding.UTF8.GetBytes($"Neto: {dteDocument.Totales.MontoNeto:F0}\n"));
            data.AddRange(Encoding.UTF8.GetBytes($"IVA: {dteDocument.Totales.IVA:F0}\n"));
            data.AddRange(EscPosCommands.BoldOn);
            data.AddRange(Encoding.UTF8.GetBytes($"TOTAL: {dteDocument.Totales.MontoTotal:F0}\n"));
            data.AddRange(EscPosCommands.BoldOff);

            // Código QR (simplificado)
            if (!string.IsNullOrEmpty(qrCodeData))
            {
                data.AddRange(EscPosCommands.AlignCenter);
                data.AddRange(Encoding.UTF8.GetBytes("Código QR DTE:\n"));
                // En una implementación real, generar código QR binario
                data.AddRange(Encoding.UTF8.GetBytes("[QR CODE]\n"));
            }

            // Espacio y corte
            data.AddRange(Encoding.UTF8.GetBytes("\n\n"));
            data.AddRange(EscPosCommands.CutPaper);

            return Result.Ok(data.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al formatear factura DTE");
            return Result.Fail($"Error al formatear factura: {ex.Message}");
        }
    }

    public async Task<Result<bool>> CutPaperAsync()
    {
        return await PrintRawAsync(EscPosCommands.CutPaper);
    }

    public async Task<Result<bool>> InitializeAsync()
    {
        return await PrintRawAsync(EscPosCommands.Initialize);
    }

    private async Task<Result<bool>> ConnectSerialAsync()
    {
        try
        {
            _serialPort = new SerialPort(_settings.Port ?? "COM1", _settings.BaudRate)
            {
                ReadTimeout = _settings.TimeoutMilliseconds,
                WriteTimeout = _settings.TimeoutMilliseconds
            };

            _serialPort.Open();
            _isConnected = true;

            _logger.LogInformation("Conexión serial exitosa al puerto {Port}", _settings.Port);
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar por serial");
            return Result.Fail($"Error de conexión serial: {ex.Message}");
        }
    }

    private async Task<Result<bool>> ConnectNetworkAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(_settings.Port))
            {
                return Result.Fail("Dirección IP requerida para conexión LAN");
            }

            var parts = _settings.Port.Split(':');
            var ip = parts[0];
            var port = parts.Length > 1 ? int.Parse(parts[1]) : 9100; // Puerto estándar ESC/POS

            _tcpClient = new TcpClient();
            var connectTask = _tcpClient.ConnectAsync(ip, port);

            if (await Task.WhenAny(connectTask, Task.Delay(_settings.TimeoutMilliseconds)) != connectTask)
            {
                return Result.Fail("Timeout al conectar por red");
            }

            _networkStream = _tcpClient.GetStream();
            _isConnected = true;

            _logger.LogInformation("Conexión de red exitosa a {Ip}:{Port}", ip, port);
            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al conectar por red");
            return Result.Fail($"Error de conexión de red: {ex.Message}");
        }
    }

    private async Task<Result<bool>> SendDataAsync(byte[] data)
    {
        try
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Write(data, 0, data.Length);
            }
            else if (_networkStream != null)
            {
                await _networkStream.WriteAsync(data, 0, data.Length);
                await _networkStream.FlushAsync();
            }
            else
            {
                return Result.Fail("No hay conexión activa");
            }

            return Result.Ok(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al enviar datos");
            return Result.Fail($"Error al enviar datos: {ex.Message}");
        }
    }
}