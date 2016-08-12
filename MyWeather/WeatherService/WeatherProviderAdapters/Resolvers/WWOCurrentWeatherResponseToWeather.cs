using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System;
using System.Linq;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOCurrentWeatherResponseToCurrentWeather : BaseResolver, ITypeConverter<WWOWeatherResponse, CurrentWeather>
    {


        //public City Convert(WWOSearchResponse source, City destination, ResolutionContext context)
        //{
        //    destination.CityName = source.search_api.result.FirstOrDefault().areaName.FirstOrDefault().value;
        //    destination.Coordinates = new Coordinates
        //    {
        //        Lattitude = float.Parse(source.search_api.result.FirstOrDefault().latitude),
        //        Longitude = float.Parse(source.search_api.result.FirstOrDefault().longitude)
        //    };
        //    destination.Id = new Guid().ToString();
        //    return destination;
        //}
        public CurrentWeather Convert(WWOWeatherResponse source, CurrentWeather destination, ResolutionContext context)
        {
            destination = new CurrentWeather();
            destination.Temp_C = source.data.current_condition.FirstOrDefault().temp_C;
            destination.Temp_F = source.data.current_condition.FirstOrDefault().temp_F;
            destination.Humidity = source.data.current_condition.FirstOrDefault().humidity;
            destination.WeatherDesc  = source.data.current_condition.FirstOrDefault().weatherDesc.FirstOrDefault().value;
            destination.CollectionTime = source.data.current_condition.FirstOrDefault().observation_time;
            destination.WeatherCode = source.data.current_condition.FirstOrDefault().weatherCode;
            destination.Pressure = source.data.current_condition.FirstOrDefault().pressure;
            
            String[] val = localObsToTime(source.data.current_condition.FirstOrDefault().localObsDateTime);
            if (val != null)
            {
                destination.LocalObsTime = val[1] + val[2];
                destination.IsDay = val[2].Contains("AM") ? true : false;
            }
            string iconText = base.ConvertToMyicon(destination.WeatherDesc);
            destination.Icon = iconText.Equals("clear")?(destination.IsDay?iconText+"skyd" : base.ConvertToMyicon(destination.WeatherDesc) + "skyn"):iconText;
            base.EvaluateMessage(ref destination);            
            return destination;
        }

        
    }
}
