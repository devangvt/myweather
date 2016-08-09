using System;
using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using System.Text.RegularExpressions;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    internal class OpenWeatherMapResponseToWeatherResolver : BaseResolver,ITypeConverter<OpenWeatherMapResponse, Weather>
    {
        public Weather Convert(OpenWeatherMapResponse source, Weather destination, ResolutionContext context)
        {
            destination = new Weather();
            destination.BasicTempreture = new BasicTempreture();
            destination.BasicTempreture.Temp = source.Main.Temp;
            destination.BasicTempreture.TempMin = source.Main.TempMax;
            destination.BasicTempreture.TempMax = source.Main.TempMin;
            destination.BasicTempreture.Pressure = source.Main.Pressure;
            destination.BasicTempreture.Humidity = source.Main.Humidity;
            destination.Id = source.Weather.Count > 0 ? 0 : source.Weather[0].Id;
            destination.Description = source.Weather.Count > 0 ? source.Weather[0].Description : "";
            destination.Message = source.Weather.Count > 0 ? evaluateMessage(source.Weather[0].Main):"";
            destination.Date = Helper.ConverterUtils.ConvertUnixTimeStampToDateTime(source.Dt);
            destination.Icon = ConvertToMyicon(source.Weather.Count > 0 ? source.Weather[0].Icon : "unknown");
            return destination;
        }        
    }
}