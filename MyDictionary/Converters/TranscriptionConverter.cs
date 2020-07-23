using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MyDictionary.Tools;

namespace MyDictionary.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TranscriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string transcription = (string)value;
            return MyTools.WrapTranscription(transcription);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
