using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotePad.Converters
{
    public class PinClickedEvenArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PinClickedEventArgs args = value as PinClickedEventArgs;
            return args;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
