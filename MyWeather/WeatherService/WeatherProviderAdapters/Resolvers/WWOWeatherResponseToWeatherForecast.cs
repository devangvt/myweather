using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOWeatherResponseToWeatherForecast : BaseResolver, ITypeConverter<WWOWeatherResponse, WeatherForcast>
    {


       
        public WeatherForcast Convert(WWOWeatherResponse source, WeatherForcast destination, ResolutionContext context)
        {
            destination = new WeatherForcast();
            destination.Forecast = new List<WeatherTemplate>();
            foreach (Providers.wwo.Contracts.Weather w in source.data.weather)
            {
                WeatherTemplate template = new WeatherTemplate();
                template.date = w.date;
                
                template.weatherDesc = w.hourly.FirstOrDefault().weatherDesc.FirstOrDefault().value;
                template.maxtempC = w.maxtempC;
                template.mintempC = w.mintempC;
                template.maxtempF = w.maxtempF;
                template.mintempF = w.mintempF;
                template.icon = base.ConvertToMyicon(template.weatherDesc);
                destination.Forecast.Add(template);
            }
            return destination;
        }
    }
}
