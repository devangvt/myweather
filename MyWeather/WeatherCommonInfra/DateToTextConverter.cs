using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DevangsWeather.CommonInfra
{
    public class DateToTextConverter:IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            String displayDate = "";
            if (value!=null)
            {
                displayDate = ((DateTime)value).ToShortDateString() + " " + ((DateTime)value).ToShortTimeString();
            }
            return displayDate;

        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            return "";
        }
    }
}
