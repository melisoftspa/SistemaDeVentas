using System.Drawing;
using ZXing;
using ZXing.PDF417;

namespace SistemaDeVentas.Infrastructure.Services.DTE;

/// <summary>
/// Servicio para generación de códigos PDF417 compatibles con TED (Timbre Electrónico DTE) del SII chileno.
/// Utiliza ZXing.Net para generar códigos PDF417 con configuración optimizada para DTE.
/// </summary>
public class Pdf417Service : IPdf417Service
{
    private readonly PDF417Writer _writer;

    public Pdf417Service()
    {
        _writer = new PDF417Writer();
    }

    /// <summary>
    /// Genera un código PDF417 a partir de datos binarios.
    /// </summary>
    /// <param name="data">Los datos binarios a codificar.</param>
    /// <returns>Un bitmap del código PDF417.</returns>
    /// <exception cref="ArgumentNullException">Si data es null.</exception>
    /// <exception cref="InvalidOperationException">Si falla la generación del código.</exception>
    public Bitmap GeneratePdf417(byte[] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        try
        {
            var hints = new System.Collections.Generic.Dictionary<EncodeHintType, object>
            {
                { EncodeHintType.ERROR_CORRECTION, "L" },
                { EncodeHintType.CHARACTER_SET, "ISO-8859-1" }
            };

            var matrix = _writer.encode(System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(data), BarcodeFormat.PDF_417, 0, 0, hints);
            var width = matrix.Width;
            var height = matrix.Height;
            var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = matrix[x, y] ? Color.Black : Color.White;
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al generar código PDF417 desde datos binarios.", ex);
        }
    }

    /// <summary>
    /// Genera un código PDF417 a partir de datos textuales.
    /// </summary>
    /// <param name="text">El texto a codificar.</param>
    /// <returns>Un bitmap del código PDF417.</returns>
    /// <exception cref="ArgumentNullException">Si text es null.</exception>
    /// <exception cref="InvalidOperationException">Si falla la generación del código.</exception>
    public Bitmap GeneratePdf417(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        try
        {
            var hints = new System.Collections.Generic.Dictionary<EncodeHintType, object>
            {
                { EncodeHintType.ERROR_CORRECTION, "L" },
                { EncodeHintType.CHARACTER_SET, "UTF-8" }
            };

            var matrix = _writer.encode(text, BarcodeFormat.PDF_417, 0, 0, hints);
            var width = matrix.Width;
            var height = matrix.Height;
            var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = matrix[x, y] ? Color.Black : Color.White;
                    bitmap.SetPixel(x, y, color);
                }
            }

            return bitmap;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al generar código PDF417 desde texto.", ex);
        }
    }
}