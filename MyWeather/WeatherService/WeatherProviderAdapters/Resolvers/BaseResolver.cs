using DevangsWeather.Model;
using System;
using System.Linq;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    public class  BaseResolver
    {
        private readonly string PleasantWeatherMsg = "Weather is quite pleasant";
        private readonly string ChillyWeatherMsg = "Weather is to chilly, get your sweathers out!";
        private readonly string SadSmileyIcon = "sad";
        private readonly string SmileyIcon = "smile";
        private readonly string HotWeatherMsg = "Oh!..Its too hot!";
        private readonly string Unknown = "unknown";
        private readonly string[] keys = new string[] { "rain", "sunny", "thunderstorm", "snow", "cloudy", "clear", "haze", "mist" };

        protected void EvaluateMessage(ref CurrentWeather currentWeather )
        {
            ///TODO check null & invalid parsing.
            if(double.Parse(currentWeather.Temp_C) > 10 && double.Parse(currentWeather.Temp_C) < 30)
            {
                currentWeather.Message = PleasantWeatherMsg;
                currentWeather.SmileIcon = SmileyIcon;
            }
            if (double.Parse(currentWeather.Temp_C) < 10 )
            {
                currentWeather.Message = ChillyWeatherMsg;
                currentWeather.SmileIcon = SadSmileyIcon;
            }
            if (double.Parse(currentWeather.Temp_C) > 30)
            {
                currentWeather.Message = HotWeatherMsg;
                currentWeather.SmileIcon = SadSmileyIcon;
            }
           
        }

        protected String[] localObsToTime(string local)
        {
            if (!String.IsNullOrEmpty(local))
            {
                char[] arr = new char[1] { ' ' };
                string[] split = local.Split(arr);
                if (split.Length == 3)
                {
                    return split;
                }
            }
            return null ;
                                
        }

        protected String ConvertToMyicon(string v)
        {   
            var result =  keys.FirstOrDefault<string>(s => v.ToLower().Contains(s));

            if(result == null)
            {
                return Unknown;
            }
            return result;          
          
        }
    }
}