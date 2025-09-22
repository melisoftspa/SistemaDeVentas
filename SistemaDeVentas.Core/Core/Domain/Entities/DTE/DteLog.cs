using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeVentas.Core.Domain.Entities.DTE;

/// <summary>
/// Representa un log de operaciones DTE.
/// </summary>
public class DteLog
{
    /// <summary>
    /// Identificador único del log.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID de la venta relacionada.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Estado de la operación DTE.
    /// </summary>
    [StringLength(50)]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Mensaje descriptivo del log.
    /// </summary>
    [StringLength(1000)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Código de error si aplica.
    /// </summary>
    [StringLength(100)]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Fecha de creación del log.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Fecha de última actualización.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// ID del folio DTE si aplica.
    /// </summary>
    public int? DteFolio { get; set; }

    /// <summary>
    /// Tipo de documento DTE.
    /// </summary>
    [StringLength(10)]
    public string? DteType { get; set; }

    // Navigation property
    public Sale? Sale { get; set; }
}