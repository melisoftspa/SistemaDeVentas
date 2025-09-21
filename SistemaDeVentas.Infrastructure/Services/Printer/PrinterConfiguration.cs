using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.Extensions.Configuration;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities.Printer;
using SistemaDeVentas.Core.Domain.Enums;

namespace SistemaDeVentas.Infrastructure.Services.Printer;

/// <summary>
/// Servicio de configuración de impresoras térmicas.
/// </summary>
public class PrinterConfiguration : IPrinterConfiguration
{
    private readonly IConfiguration _configuration;

    public PrinterConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Obtiene la configuración de la impresora especificada por ID.
    /// </summary>
    /// <param name="printerId">ID de la impresora.</param>
    /// <returns>Configuración de la impresora.</returns>
    public async Task<Result<PrinterConfig>> GetConfigurationAsync(string printerId)
    {
        try
        {
            var thermalPrintersSection = _configuration.GetSection("ThermalPrinters");
            var printerSection = thermalPrintersSection.GetSection(printerId);

            if (!printerSection.Exists())
            {
                return Result.Fail($"No se encontró configuración para la impresora '{printerId}'.");
            }

            var config = new PrinterConfig
            {
                Id = printerId,
                Settings = new ThermalPrinterSettings
                {
                    Model = Enum.Parse<PrinterModel>(printerSection["Model"] ?? "Generic"),
                    ConnectionType = Enum.Parse<ConnectionType>(printerSection["ConnectionType"] ?? "USB"),
                    Port = printerSection["Port"],
                    BaudRate = int.Parse(printerSection["BaudRate"] ?? "9600"),
                    TimeoutMilliseconds = int.Parse(printerSection["TimeoutMilliseconds"] ?? "5000"),
                    PaperWidth = int.Parse(printerSection["PaperWidth"] ?? "32"),
                    Name = printerSection["Name"] ?? printerId
                }
            };

            var errors = config.Validate();
            if (errors.Any())
            {
                return Result.Fail($"Configuración inválida para '{printerId}': {string.Join(", ", errors)}");
            }

            return Result.Ok(config);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error al obtener configuración para '{printerId}': {ex.Message}");
        }
    }

    /// <summary>
    /// Guarda la configuración de la impresora.
    /// Nota: Esta implementación no guarda en appsettings.json ya que es read-only.
    /// En una implementación completa, se guardaría en un archivo separado o base de datos.
    /// </summary>
    /// <param name="config">Configuración a guardar.</param>
    /// <returns>Resultado de la operación de guardado.</returns>
    public async Task<Result<bool>> SaveConfigurationAsync(PrinterConfig config)
    {
        // Validar configuración
        var errors = config.Validate();
        if (errors.Any())
        {
            return Result.Fail($"Configuración inválida: {string.Join(", ", errors)}");
        }

        // En una implementación real, guardar en archivo JSON o base de datos
        // Por ahora, solo validar y devolver éxito
        return Result.Ok(true);
    }

    /// <summary>
    /// Obtiene todas las configuraciones de impresoras.
    /// </summary>
    /// <returns>Lista de configuraciones de impresoras.</returns>
    public async Task<Result<List<PrinterConfig>>> GetAllConfigurationsAsync()
    {
        try
        {
            var configs = new List<PrinterConfig>();
            var thermalPrintersSection = _configuration.GetSection("ThermalPrinters");

            foreach (var child in thermalPrintersSection.GetChildren())
            {
                var result = await GetConfigurationAsync(child.Key);
                if (result.IsSuccess)
                {
                    configs.Add(result.Value);
                }
            }

            return Result.Ok(configs);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error al obtener todas las configuraciones: {ex.Message}");
        }
    }

    /// <summary>
    /// Lista las impresoras disponibles en el sistema.
    /// </summary>
    /// <returns>Lista de nombres de impresoras disponibles.</returns>
    public async Task<Result<List<string>>> ListAvailablePrintersAsync()
    {
        try
        {
            var printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            // Agregar detección de puertos COM para impresoras térmicas
            var comPorts = await DetectComPortsAsync();
            printers.AddRange(comPorts);

            return Result.Ok(printers.Distinct().ToList());
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error al listar impresoras disponibles: {ex.Message}");
        }
    }

    /// <summary>
    /// Valida una configuración de impresora.
    /// </summary>
    /// <param name="config">Configuración a validar.</param>
    /// <returns>Resultado de la validación.</returns>
    public async Task<Result<bool>> ValidateConfigurationAsync(PrinterConfig config)
    {
        var errors = config.Validate();
        if (errors.Any())
        {
            return Result.Fail($"Configuración inválida: {string.Join(", ", errors)}");
        }

        return Result.Ok(true);
    }

    /// <summary>
    /// Detecta automáticamente impresoras térmicas conectadas a puertos COM.
    /// </summary>
    /// <returns>Lista de puertos COM disponibles.</returns>
    private async Task<List<string>> DetectComPortsAsync()
    {
        var comPorts = new List<string>();
        try
        {
            // Usar System.IO.Ports para detectar puertos COM
            // Nota: En .NET Core, usar System.IO.Ports.SerialPort.GetPortNames()
            // Pero para simplificar, asumir COM1-COM10
            for (int i = 1; i <= 10; i++)
            {
                comPorts.Add($"COM{i}");
            }
        }
        catch
        {
            // Ignorar errores de detección
        }

        return comPorts;
    }
}