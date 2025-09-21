using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Core.Application.Services
{
    /// <summary>
    /// Implementación del servicio de parámetros
    /// </summary>
    public class ParameterService : IParameterService
    {
        private readonly IParameterRepository _parameterRepository;

        public ParameterService(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository ?? throw new ArgumentNullException(nameof(parameterRepository));
        }

        public async Task<Parameter?> GetByKeyAsync(string key)
        {
            return await _parameterRepository.GetByKeyAsync(key);
        }

        public async Task UpsertAsync(Parameter parameter)
        {
            await _parameterRepository.UpsertAsync(parameter);
        }

        public async Task<IEnumerable<Parameter>> GetAllAsync()
        {
            return await _parameterRepository.GetAllAsync();
        }

        public async Task DeleteAsync(string key)
        {
            await _parameterRepository.DeleteAsync(key);
        }
    }
}