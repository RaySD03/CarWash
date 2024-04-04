using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Converters;
using System.Globalization;

namespace CarWash.Models
{
    public class StringToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorTypeConverter converter = new ColorTypeConverter();
            Color color = (Color)(converter.ConvertFromInvariantString((string)value));
            return color;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = "white";
            return colorString;
        }
    }
}
