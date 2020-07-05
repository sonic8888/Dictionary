using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyDictionary.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class StateConverter : IValueConverter
    {
        string pathGrey = @"Picture/Cub_grey.png";
        string pathGreen = @"Picture/Cub_green.png";
        string pathGold = @"Picture/Cub_Gold.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            string path = "";
            switch (state)
            {
                case 1:
                    path = pathGrey;
                    break;
                case 2:
                    path = pathGreen;
                    break;
                case 3:
                    path = pathGold;
                    break;
                default:
                    path = pathGrey;
                    break;
            }
            return state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}