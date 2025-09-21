using SistemaDeVentas.Core.Application.Interfaces;
using System.Xml.Linq;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio para almacenamiento de archivos DTE en el sistema de archivos.
/// </summary>
public class DteFileStorageService : IDteFileStorageService
{
    private readonly string _baseStoragePath;

    public DteFileStorageService()
    {
        // Ruta configurable, por defecto en el directorio de la aplicación
        _baseStoragePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DteStorage");

        // Crear directorio si no existe
        if (!Directory.Exists(_baseStoragePath))
        {
            Directory.CreateDirectory(_baseStoragePath);
        }
    }

    /// <summary>
    /// Constructor con ruta configurable.
    /// </summary>
    /// <param name="baseStoragePath">Ruta base para almacenamiento.</param>
    public DteFileStorageService(string baseStoragePath)
    {
        _baseStoragePath = baseStoragePath ?? throw new ArgumentNullException(nameof(baseStoragePath));

        if (!Directory.Exists(_baseStoragePath))
        {
            Directory.CreateDirectory(_baseStoragePath);
        }
    }

    /// <inheritdoc/>
    public async Task<string> SaveDteXmlAsync(Guid saleId, int folio, XDocument dteXml)
    {
        var fileName = $"DTE_{saleId}_{folio}.xml";
        var filePath = Path.Combine(_baseStoragePath, fileName);

        await File.WriteAllTextAsync(filePath, dteXml.ToString());

        return filePath;
    }

    /// <inheritdoc/>
    public async Task<string> SaveDtePdfAsync(Guid saleId, int folio, byte[] pdfBytes)
    {
        var fileName = $"DTE_{saleId}_{folio}.pdf";
        var filePath = Path.Combine(_baseStoragePath, fileName);

        await File.WriteAllBytesAsync(filePath, pdfBytes);

        return filePath;
    }

    /// <inheritdoc/>
    public string GetStorageDirectory()
    {
        return _baseStoragePath;
    }
}