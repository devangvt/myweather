using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.WeatherProviderAdapters;
using AutoMapper;
using System.Reflection;
using System.Collections;

namespace DevangsWeather.Service.WeatherProviderAdapters
{   
    public class OpenWeatherMapAdapter : IWeatherProviderAdapter
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);        
        IOpenWeatherMapApiClient openWeatherMapClient = null;

        public Task<City> GetCityByName(string cityName)
        {
            throw new NotImplementedException();
        }

        public Task<CurrentWeather> GetCurrentWeather(string city)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherHistoric> GetWeatherForLastWeek(string city, int days = 7)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherForcast> GetWeatherForNextWeek(string city, int days = 7)
        {
            throw new NotImplementedException();
        }


        //public OpenWeatherMapAdapter(IOpenWeatherMapApiClient openWeatherMapClient)
        //{
        //    logger.Debug("Setting up the OpenWeatherMapApiClient");
        //    this.openWeatherMapClient = openWeatherMapClient;
        //    logger.Debug("initializing Mapper for OpenWeatherMapResponse to Weather");

        //    Mapper.Initialize(cfg => { cfg.CreateMap<OpenWeatherMapForcastResponse, List<Weather>>().
        //        ConvertUsing<OWMForecastResponseToListResponseResolver>();
        //        cfg.CreateMap<OpenWeatherMapResponse, Weather>().ConvertUsing<OpenWeatherMapResponseToWeatherResolver>();
        //    });

        //    Mapper.Configuration.AssertConfigurationIsValid();
        //    logger.Debug("Exit constructor");
        //}

        ///// <summary>
        ///// Method to get the City 
        ///// </summary>
        ///// <param name="cityName"></param>
        ///// <returns></returns>
        //public async Task<City> GetCityByName(string cityName)
        //{
        //    logger.Debug("Enter GetCityByName");
        //    OpenWeatherMapResponse response = await openWeatherMapClient.GetByCityAsync(cityName);
        //    City resultCityTask = new City { CityName = response.Name, Coordinates =
        //        new Coordinates() { Lattitude = response.Coord.Lat, Longitude = response.Coord.Lon },
        //        Id = response.Id.ToString()
        //    };

        //    logger.Debug("Exit GetCityByName");
        //    return resultCityTask;
        //}


        ///// <summary>
        ///// Method to get Current/Todays weather for a City
        ///// </summary>
        ///// <param name="city">Name of City</param>
        ///// <returns>Weather</returns>
        //public async Task<Weather> GetCurrentWeather(String city)
        //{
        //    Weather weather = null;
        //    try
        //    {
        //        OpenWeatherMapResponse response = await openWeatherMapClient.GetByCityAsync(city);
        //        weather = Mapper.Map<Weather>(response);
        //    }
        //    catch(Exception ex)
        //    {
        //        logger.Error("Failed to Get Current Weather for :" + city, ex);
        //    }
        //    return weather; 
        //}


        //public async Task<IList<Weather>> GetWeatherForNextWeek(String city, int days = 7)
        //{
        //   OpenWeatherMapForcastResponse response = await openWeatherMapClient.GetForcastByCity(city,null, days);
        //    return Mapper.Map<List<Weather>> (response);
        //}


    }
}
