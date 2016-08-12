using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOWeatherResponseToWeatherHistoric : BaseResolver, ITypeConverter<WWOWeatherResponse, WeatherHistoric>
    {


       
        public WeatherHistoric Convert(WWOWeatherResponse source, WeatherHistoric destination, ResolutionContext context)
        {
            destination = new WeatherHistoric();
            destination.Historic = new List<WeatherTemplate>();
            foreach (Providers.wwo.Contracts.Weather w in source.data.weather)
            {
                WeatherTemplate template = new WeatherTemplate();
                template.date = w.date;
                template.weatherDesc = w.hourly.FirstOrDefault().weatherDesc.FirstOrDefault().value;
                template.maxtempC = w.maxtempC;
                template.mintempC = w.mintempC;
                template.maxtempF = w.maxtempF;
                template.icon = base.ConvertToMyicon(template.weatherDesc);
                destination.Historic.Add(template);
            }
            return destination;
        }
    }
}
