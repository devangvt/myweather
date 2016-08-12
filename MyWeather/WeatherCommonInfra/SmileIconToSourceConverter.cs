using System;
using System.Windows.Data;

namespace DevangsWeather.CommonInfra
{
    public class SmileIconToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value != null)
                switch (value.ToString().ToLower())
                {
                    case "sad":
                        return "/WeatherHome;component/Resources/sad.png";
                    case "smile":
                        return "/WeatherHome;component/Resources/smile.png";

                }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
