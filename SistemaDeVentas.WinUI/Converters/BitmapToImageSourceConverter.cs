using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;

namespace SistemaDeVentas.WinUI.Converters
{
    /// <summary>
    /// Converter para transformar System.Drawing.Bitmap a BitmapImage compatible con WinUI 3
    /// </summary>
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Bitmap bitmap)
            {
                return ConvertBitmapToBitmapImage(bitmap);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private static BitmapImage? ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            try
            {
                using var memory = new MemoryStream();
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(memory.AsRandomAccessStream());
                return bitmapImage;
            }
            catch (Exception)
            {
                // En caso de error, devolver null para que no se muestre nada
                return null;
            }
        }
    }
}