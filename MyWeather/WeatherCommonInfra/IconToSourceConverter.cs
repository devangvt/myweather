using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DevangsWeather.CommonInfra
{
    public class IconToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value != null)
                switch (value.ToString().ToLower())
                {
                    case "thunderstorm":
                        return "/WeatherHome;component/Resources/thunderstorm.png";
                    case "rain":
                        return "/WeatherHome;component/Resources/showerrain.png";
                    case "raind":
                        return "/WeatherHome;component/Resources/raind.png";
                    case "haze":
                        return "/WeatherHome;component/Resources/haze.png";
                    case "cloudy":
                        return "/WeatherHome;component/Resources/brokenclouds.png";
                    case "rainn":
                        return "/WeatherHome;component/Resources/rainn.png";
                    case "fewclouds":
                        return "/WeatherHome;component/Resources/fewclouds.png";
                    case "snow":
                        return "/WeatherHome;component/Resources/snow.png";
                    case "sunny":
                        return "/WeatherHome;component/Resources/clearskyd.png";
                    case "unknown":
                        return "/WeatherHome;component/Resources/unknown.png";
                    case "scattercloudsn":
                        return "/WeatherHome;component/Resources/scattercloudsn.png";
                    case "scattercloudsd":
                        return "/WeatherHome;component/Resources/scattercloudsd.png";
                    case "clearskyd":
                        return "/WeatherHome;component/Resources/clearskyd.png";
                    case "clearskyn":
                        return "/WeatherHome;component/Resources/clearskyn.png";



                }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
