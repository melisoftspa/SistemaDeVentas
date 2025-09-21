using System.Drawing;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Interfaz para servicios de generación de códigos PDF417 compatibles con TED DTE.
/// </summary>
public interface IPdf417Service
{
    /// <summary>
    /// Genera un código PDF417 a partir de datos binarios.
    /// </summary>
    /// <param name="data">Los datos binarios a codificar.</param>
    /// <returns>Un bitmap del código PDF417.</returns>
    Bitmap GeneratePdf417(byte[] data);

    /// <summary>
    /// Genera un código PDF417 a partir de datos textuales.
    /// </summary>
    /// <param name="text">El texto a codificar.</param>
    /// <returns>Un bitmap del código PDF417.</returns>
    Bitmap GeneratePdf417(string text);
}