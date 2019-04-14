using System;
using System.Globalization;
using Xamarin.Forms;

namespace NotesFaceGraph.Converters
{
    public class NullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            if (!(value is string strValue))
                throw new InvalidCastException();

            return !string.IsNullOrWhiteSpace(strValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
