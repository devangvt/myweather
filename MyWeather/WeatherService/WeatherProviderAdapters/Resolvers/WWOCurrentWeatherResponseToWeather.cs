using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System;
using System.Linq;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOCurrentWeatherResponseToCurrentWeather : BaseResolver, ITypeConverter<WWOWeatherResponse, CurrentWeather>
    {
        public CurrentWeather Convert(WWOWeatherResponse source, CurrentWeather destination, ResolutionContext context)
        {
            if (source.data.current_condition.Count > 0)
            {
                var currentCondition = source.data.current_condition.FirstOrDefault();
                destination = new CurrentWeather();
                destination.Temp_C = currentCondition.temp_C;
                destination.Temp_F = currentCondition.temp_F;
                destination.Humidity = currentCondition.humidity;
                destination.WeatherDesc = currentCondition.weatherDesc.FirstOrDefault().value;
                destination.CollectionTime = currentCondition.observation_time;
                destination.WeatherCode = currentCondition.weatherCode;
                destination.Pressure = currentCondition.pressure;

                String[] val = localObsToTime(currentCondition.localObsDateTime);
                if (val != null)
                {
                    destination.LocalObsTime = val[1] + val[2];
                    destination.IsDay = val[2].Contains("AM") ? true : false;
                }
                string iconText = base.ConvertToMyicon(destination.WeatherDesc);
                destination.Icon = iconText.Equals("clear") ? (destination.IsDay ? iconText + "skyd" : base.ConvertToMyicon(destination.WeatherDesc) + "skyn") : iconText;
                base.EvaluateMessage(ref destination);
            }        
            return destination;
        }

        
    }
}
