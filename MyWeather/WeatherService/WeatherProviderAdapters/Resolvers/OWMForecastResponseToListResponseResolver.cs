using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Providers.OpenWeatherMap.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    class OWMForecastResponseToListResponseResolver : BaseResolver,ITypeConverter<OpenWeatherMapForcastResponse, List<Weather>>
    {
       
        List<Weather> ITypeConverter<OpenWeatherMapForcastResponse, List<Weather>>.Convert(OpenWeatherMapForcastResponse source, List<Weather> destination, ResolutionContext context)
        {
            destination = new List<Weather>();

            foreach (List l in source.list)
            {
                Weather w = new Weather();
                w.BasicTempreture = new BasicTempreture();
                w.BasicTempreture.TempMin = l.Temp.Min;
                w.BasicTempreture.TempMax = l.Temp.Max;
                //w.Message = evaluateMessage(l.Weather.FirstOrDefault().Main);
                w.Description = l.Weather.FirstOrDefault().Description;
                w.Date = Helper.ConverterUtils.ConvertUnixTimeStampToDateTime(l.Dt);
                w.Icon = ConvertToMyicon(l.Weather.Count > 0 ? l.Weather[0].Icon : "unknown"); ;
                w.Id = l.Weather.FirstOrDefault().Id;
                destination.Add(w);
            }

            return destination;
        }
    }
}
