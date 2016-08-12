using System;
using System.Globalization;
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
            String  displayDate = ((DateTime)value).DayOfWeek.ToString().Substring(0,3);
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
