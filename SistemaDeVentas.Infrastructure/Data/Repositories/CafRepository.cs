using System.Linq;
using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities.DTE;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Data;

namespace SistemaDeVentas.Infrastructure.Data.Repositories;

/// <summary>
/// Repositorio para manejo de CAF (Códigos de Autorización de Folios).
/// </summary>
public class CafRepository : ICafRepository
{
    private readonly SalesSystemDbContext _context;

    public CafRepository(SalesSystemDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Caf?> ObtenerPorTipoDocumentoAsync(int tipoDocumento, int ambiente, string rutEmisor)
    {
        return await _context.Cafs
            .Where(c => c.TipoDocumento == tipoDocumento &&
                       c.Ambiente == ambiente &&
                       c.RutEmisor == rutEmisor &&
                       c.Activo &&
                       c.FechaVencimiento > DateTime.Now)
            .OrderByDescending(c => c.FechaVencimiento)
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<Caf?> ObtenerPorIdAsync(Guid id)
    {
        return await _context.Cafs.FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Caf>> ObtenerActivosPorEmisorAsync(string rutEmisor)
    {
        return await _context.Cafs
            .Where(c => c.RutEmisor == rutEmisor && c.Activo && c.FechaVencimiento > DateTime.Now)
            .OrderBy(c => c.TipoDocumento)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Guid> GuardarAsync(Caf caf)
    {
        if (caf.Id == Guid.Empty)
        {
            _context.Cafs.Add(caf);
        }
        else
        {
            _context.Cafs.Update(caf);
        }

        await _context.SaveChangesAsync();
        return caf.Id;
    }

    /// <inheritdoc/>
    public async Task ActualizarFolioActualAsync(Guid id, int folioActual)
    {
        var caf = await _context.Cafs.FindAsync(id);
        if (caf != null)
        {
            caf.FolioActual = folioActual;
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task DesactivarAsync(Guid id)
    {
        var caf = await _context.Cafs.FindAsync(id);
        if (caf != null)
        {
            caf.Activo = false;
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> ExisteAsync(int tipoDocumento, int ambiente, string rutEmisor)
    {
        return await _context.Cafs
            .AnyAsync(c => c.TipoDocumento == tipoDocumento &&
                          c.Ambiente == ambiente &&
                          c.RutEmisor == rutEmisor &&
                          c.Activo);
    }
}