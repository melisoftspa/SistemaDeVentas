using SistemaDeVentas.Core.Domain.Entities;
using System.Threading.Tasks;

namespace SistemaDeVentas.Core.Application.Interfaces
{
    /// <summary>
    /// Servicio para gestión de parámetros del sistema
    /// </summary>
    public interface IParameterService
    {
        /// <summary>
        /// Obtiene un parámetro por su clave
        /// </summary>
        Task<Parameter?> GetByKeyAsync(string key);

        /// <summary>
        /// Inserta o actualiza un parámetro
        /// </summary>
        Task UpsertAsync(Parameter parameter);

        /// <summary>
        /// Obtiene todos los parámetros
        /// </summary>
        Task<IEnumerable<Parameter>> GetAllAsync();

        /// <summary>
        /// Elimina un parámetro por su clave
        /// </summary>
        Task DeleteAsync(string key);
    }
}