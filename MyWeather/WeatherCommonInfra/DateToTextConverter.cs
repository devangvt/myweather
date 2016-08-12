using System;
using System.Globalization;
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
