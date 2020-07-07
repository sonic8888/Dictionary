using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyDictionary.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class DataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strData = (string)value;

            DateTime dateTime = DateTime.Parse(strData);
            CultureInfo cul  = new CultureInfo("ru-RU");
            return dateTime.ToString("d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
