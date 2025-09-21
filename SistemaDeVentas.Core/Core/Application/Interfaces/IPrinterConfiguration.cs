using System.Collections.Generic;
using System.Threading.Tasks;
using FluentResults;
using SistemaDeVentas.Core.Domain.Entities.Printer;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    public interface IPrinterConfiguration
    {
        /// <summary>
        /// Obtiene la configuración de la impresora especificada por ID.
        /// </summary>
        /// <param name="printerId">ID de la impresora.</param>
        /// <returns>Configuración de la impresora.</returns>
        Task<Result<PrinterConfig>> GetConfigurationAsync(string printerId);

        /// <summary>
        /// Guarda la configuración de la impresora.
        /// </summary>
        /// <param name="config">Configuración a guardar.</param>
        /// <returns>Resultado de la operación de guardado.</returns>
        Task<Result<bool>> SaveConfigurationAsync(PrinterConfig config);

        /// <summary>
        /// Obtiene todas las configuraciones de impresoras.
        /// </summary>
        /// <returns>Lista de configuraciones de impresoras.</returns>
        Task<Result<List<PrinterConfig>>> GetAllConfigurationsAsync();

        /// <summary>
        /// Lista las impresoras disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de nombres de impresoras disponibles.</returns>
        Task<Result<List<string>>> ListAvailablePrintersAsync();

        /// <summary>
        /// Valida una configuración de impresora.
        /// </summary>
        /// <param name="config">Configuración a validar.</param>
        /// <returns>Resultado de la validación.</returns>
        Task<Result<bool>> ValidateConfigurationAsync(PrinterConfig config);
    }
}