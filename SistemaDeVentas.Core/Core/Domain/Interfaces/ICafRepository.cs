using SistemaDeVentas.Core.Domain.Entities.DTE;

namespace SistemaDeVentas.Core.Domain.Interfaces;

/// <summary>
/// Interfaz para el repositorio de CAF (Códigos de Autorización de Folios).
/// </summary>
public interface ICafRepository
{
    /// <summary>
    /// Obtiene un CAF por tipo de documento y ambiente.
    /// </summary>
    /// <param name="tipoDocumento">Tipo de documento.</param>
    /// <param name="ambiente">Ambiente del SII.</param>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <returns>CAF encontrado o null.</returns>
    Task<Caf?> ObtenerPorTipoDocumentoAsync(int tipoDocumento, int ambiente, string rutEmisor);

    /// <summary>
    /// Obtiene un CAF específico por ID.
    /// </summary>
    /// <param name="id">ID del CAF.</param>
    /// <returns>CAF encontrado o null.</returns>
    Task<Caf?> ObtenerPorIdAsync(Guid id);

    /// <summary>
    /// Obtiene todos los CAF activos para un emisor.
    /// </summary>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <returns>Lista de CAF activos.</returns>
    Task<IEnumerable<Caf>> ObtenerActivosPorEmisorAsync(string rutEmisor);

    /// <summary>
    /// Guarda un CAF en el repositorio.
    /// </summary>
    /// <param name="caf">CAF a guardar.</param>
    /// <returns>ID del CAF guardado.</returns>
    Task<Guid> GuardarAsync(Caf caf);

    /// <summary>
    /// Actualiza el folio actual de un CAF.
    /// </summary>
    /// <param name="id">ID del CAF.</param>
    /// <param name="folioActual">Nuevo folio actual.</param>
    Task ActualizarFolioActualAsync(Guid id, int folioActual);

    /// <summary>
    /// Desactiva un CAF.
    /// </summary>
    /// <param name="id">ID del CAF.</param>
    Task DesactivarAsync(Guid id);

    /// <summary>
    /// Verifica si existe un CAF para el tipo de documento y ambiente.
    /// </summary>
    /// <param name="tipoDocumento">Tipo de documento.</param>
    /// <param name="ambiente">Ambiente.</param>
    /// <param name="rutEmisor">RUT del emisor.</param>
    /// <returns>True si existe.</returns>
    Task<bool> ExisteAsync(int tipoDocumento, int ambiente, string rutEmisor);
}