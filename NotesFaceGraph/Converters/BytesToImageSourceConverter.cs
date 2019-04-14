using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace NotesFaceGraph.Converters
{
    public class BytesToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (!(value is byte[] bytes))
                throw new InvalidCastException();

            ImageSource source;
            Stream stream = new MemoryStream(bytes);
            source = ImageSource.FromStream(() => stream);

            return source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
