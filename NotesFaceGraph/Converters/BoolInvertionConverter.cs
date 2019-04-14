using System;
using System.Globalization;
using Xamarin.Forms;

namespace NotesFaceGraph.Converters
{
    public class BoolInvertionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                throw new InvalidCastException();

            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool boolValue))
                throw new InvalidCastException();

            return !boolValue;

        }
    }
}
