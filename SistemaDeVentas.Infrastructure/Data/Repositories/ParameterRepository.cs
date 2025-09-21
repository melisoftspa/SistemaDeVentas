using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaDeVentas.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Implementación del repositorio de parámetros
    /// </summary>
    public class ParameterRepository : IParameterRepository
    {
        private readonly SalesSystemDbContext _context;

        public ParameterRepository(SalesSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Parameter?> GetByKeyAsync(string key)
        {
            return await _context.Parameters
                .FirstOrDefaultAsync(p => p.Name == key);
        }

        public async Task UpsertAsync(Parameter parameter)
        {
            var existing = await GetByKeyAsync(parameter.Name);
            if (existing != null)
            {
                // Actualizar
                existing.Value = parameter.Value;
                existing.Type = parameter.Type;
                existing.Module = parameter.Module;
                _context.Parameters.Update(existing);
            }
            else
            {
                // Insertar
                parameter.Date = DateTime.Now;
                await _context.Parameters.AddAsync(parameter);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Parameter>> GetAllAsync()
        {
            return await _context.Parameters
                .OrderBy(p => p.Key)
                .ToListAsync();
        }

        public async Task DeleteAsync(string key)
        {
            var parameter = await GetByKeyAsync(key);
            if (parameter != null)
            {
                _context.Parameters.Remove(parameter);
                await _context.SaveChangesAsync();
            }
        }
    }
}