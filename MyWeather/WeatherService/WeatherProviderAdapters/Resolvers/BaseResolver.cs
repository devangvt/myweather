using DevangsWeather.Model;
using System;
using System.Text.RegularExpressions;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    public class  BaseResolver
    {
        
        protected void EvaluateMessage(ref CurrentWeather currentWeather )
        {
            ///TODO check null & invalid parsing.
            if(double.Parse(currentWeather.Temp_C) > 10 && double.Parse(currentWeather.Temp_C) < 30)
            {
                currentWeather.Message = "Weather is quite pleasant";
                currentWeather.SmileIcon = "smile";
            }
            if (double.Parse(currentWeather.Temp_C) < 10 )
            {
                currentWeather.Message = "Weather is to chilly, get your sweathers out!";
                currentWeather.SmileIcon = "sad";
            }
            if (double.Parse(currentWeather.Temp_C) > 30)
            {
                currentWeather.Message = "Its too hot!";
                currentWeather.SmileIcon = "sad";
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
            if (v.ToLower().Contains("rain")){
                return "rain";
            }
            if (v.ToLower().Contains("sunny"))
            {
                return "sunny";
            }
            if (v.ToLower().Contains("thunderstom"))
            {
                return "thunderstorm";
            }

            if (v.ToLower().ToLower().Contains("snow"))
            {
                return "snow";
            }

            if (v.ToLower().Contains("cloudy"))
            {
                return "cloudy";
            }

            if (v.ToLower().Contains("clear"))
            {
                return "clear";
            }
            if (v.ToLower().Contains("haze"))
            {
                return "haze";
            }

            //if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^2").Value))
            //{
            //    return "thunderstorm";
            //}
            //if (v.Equals("01d"))
            //{
            //    return "clearskyd";
            //}
            //if (v.Equals("01n"))
            //{
            //    return "clearskyn";
            //}

            //if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^3").Value))
            //{
            //    return "showerrain";
            //}
            //if (v.Equals("500") || v.Equals("501") || v.Equals("502") || v.Equals("503") || v.Equals("504") || v.Equals("10d"))
            //{
            //    return "raind";
            //}
            //if (v.Equals("803") || v.Equals("804") || v.Equals("04d") || v.Equals("04n"))
            //{
            //    return "brokenclouds";
            //}
            //if (v.Equals("801")|| v.Equals("02d") || v.Equals("02n"))
            //{
            //    return "fewclouds";
            //}
            //if (v.Equals("03d"))
            //{
            //    return "scattercloudsd";
            //}
            //if (v.Equals("03n"))
            //{
            //    return "scattercloudsn";
            //}
            //if ( v.Equals("10n") || v.Equals("02n") || v.Equals("511") || v.Equals("520") || v.Equals("521") || v.Equals("522") || v.Equals("531"))
            //{
            //    return "rainn";
            //}
            //if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^6").Value))
            //{
            //    return "snow";
            //}
            return "unknown";
        }
    }
}