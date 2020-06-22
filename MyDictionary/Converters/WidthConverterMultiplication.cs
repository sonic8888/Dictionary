using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyDictionary.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class WidthConverterMultiplication : IValueConverter
    {
        private double width;

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double actualWidth = (double)value;
            return actualWidth * width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
