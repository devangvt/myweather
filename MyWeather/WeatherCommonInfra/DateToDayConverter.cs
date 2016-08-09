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
    public class DateToDayConverter:IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            String  displayDate = ((DateTime)value).DayOfWeek.ToString().Substring(0,3).ToUpper();
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
