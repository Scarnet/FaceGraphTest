using System;
using System.Globalization;
using Xamarin.Forms;

namespace NotesFaceGraph.Converters
{
    public class BytesToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (!(value is byte[] bytes))
                throw new InvalidCastException();

            if (bytes.Length <= 2)
                return false;

            return true;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new byte[0];
        }
    }
}
