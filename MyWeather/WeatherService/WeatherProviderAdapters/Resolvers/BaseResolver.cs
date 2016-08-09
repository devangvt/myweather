using DevangsWeather.Model;
using System;
using System.Text.RegularExpressions;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    public class  BaseResolver
    {
        ///TODO:Business logic for message
        protected String evaluateMessage(string message)
        {
            return "Hi";
        }

        protected String ConvertToMyicon(string v)
        {
            if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^2").Value))
            {
                return "thunderstorm";
            }
            if (v.Equals("01d"))
            {
                return "clearskyd";
            }
            if (v.Equals("01n"))
            {
                return "clearskyn";
            }
            
            if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^3").Value))
            {
                return "showerrain";
            }
            if (v.Equals("500") || v.Equals("501") || v.Equals("502") || v.Equals("503") || v.Equals("504") || v.Equals("10d"))
            {
                return "raind";
            }
            if (v.Equals("803") || v.Equals("804") || v.Equals("04d") || v.Equals("04n"))
            {
                return "brokenclouds";
            }
            if (v.Equals("801")|| v.Equals("02d") || v.Equals("02n"))
            {
                return "fewclouds";
            }
            if (v.Equals("03d"))
            {
                return "scattercloudsd";
            }
            if (v.Equals("03n"))
            {
                return "scattercloudsn";
            }
            if ( v.Equals("10n") || v.Equals("02n") || v.Equals("511") || v.Equals("520") || v.Equals("521") || v.Equals("522") || v.Equals("531"))
            {
                return "rainn";
            }
            if (!String.IsNullOrWhiteSpace(Regex.Match(v, "^6").Value))
            {
                return "snow";
            }
            return "unknown";
        }
    }
}