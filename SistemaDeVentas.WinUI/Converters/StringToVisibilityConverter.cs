using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace SistemaDeVentas.WinUI.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string stringValue)
            {
                return !string.IsNullOrEmpty(stringValue) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}