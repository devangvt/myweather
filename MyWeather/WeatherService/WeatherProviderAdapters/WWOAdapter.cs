using System;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo;
using DevangsWeather.Providers.wwo.Contracts;
using AutoMapper;
using DevangsWeather.Service.WeatherProviderAdapters.Resolvers;
using System.Reflection;

namespace DevangsWeather.WeatherProviderAdapters
{
    public class WWOAdapter : IWeatherProviderAdapter
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IWWOClient client  = null;
        public WWOAdapter(IWWOClient wwoClient)
        {
            Log.Debug("Initializing WWOClinet");
            this.client = wwoClient;

            Log.Debug("Configuring Mapper for WWOAdapter");
            Mapper.Initialize(cfg => {
                cfg.CreateMap<WWOSearchResponse, City>().ConvertUsing<WWOSearcResponseToCityResolver>();
                cfg.CreateMap<WWOWeatherResponse, CurrentWeather>().ConvertUsing<WWOCurrentWeatherResponseToCurrentWeather>();
                cfg.CreateMap<WWOWeatherResponse, WeatherForcast>().ConvertUsing<WWOWeatherResponseToWeatherForecast>();
                cfg.CreateMap<WWOWeatherResponse, WeatherHistoric>().ConvertUsing<WWOWeatherResponseToWeatherHistoric>();
            });
            Log.Debug("Done configuring Mapper for WWOAdapter");
            try
            {
                Mapper.Configuration.AssertConfigurationIsValid();
            }
            catch(AutoMapperConfigurationException ex)
            {
                Log.Error("Failed to configure mapper for WWOAdapter",ex);
                throw;
            }
        }
        public async Task<City> GetCityByName(string cityName)
        {
            WWOSearchResponse response = null;
            City city = null;
            try
            {
                Log.Debug("Finding City" + cityName);
                response = await this.client.FindCityAsync(cityName);
                Log.Debug("Found City" + cityName);
                city = Mapper.Map<City>(response);
                Log.Debug("Mapped response to" + cityName);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to search city" + cityName, ex);
                throw;
            }
            return city;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city)
        {
            WWOWeatherResponse response = null;
            CurrentWeather weather = null;
            try
            {
                Log.Debug("Getting Current weather for " + city);
                response = await this.client.GetCurrentWeather(city);
                Log.Debug("Got Current weather for " + city);
                weather = Mapper.Map<CurrentWeather>(response);
                Log.Debug("Mapped response to CurrentWeather for city"+city);
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get current weather for" + city, ex);
                throw;
            }
            return weather;
            
        }

        public async Task<WeatherForcast> GetWeatherForNextWeek(string city, int days = 7)
        {
            WWOWeatherResponse response = null;
            WeatherForcast weather = null;
            try
            {
                Log.Debug("Getting Forecast weather for " + city);
                response =  await this.client.GetForecastWeather(city);
                Log.Debug("Got Forecast weather for " + city);
                weather = Mapper.Map<WeatherForcast>(response);
                Log.Debug("Mapped response to WeatherForecast for city" + city);
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get forecast weather for " + city, ex);
                throw;
            }
            return weather;
        }

        public async Task<WeatherHistoric> GetWeatherForLastWeek(string city, int days = 7)
        {
            WeatherHistoric weather = null;
            try
            {
                string toDate = String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
                string fromDate = String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-7));
                Log.Debug("Getting Historic weather for " + city + "for dates" + fromDate+"-"+ toDate);
                WWOWeatherResponse response = await this.client.GetHistoricWeather(city, fromDate, toDate);
                Log.Debug("Got Historic weather for " + city);
                weather = Mapper.Map<WeatherHistoric>(response);
                Log.Debug("Mapped response to WeatherHistoric for city" + city);
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get historic weather for " + city, ex);
                throw;
            }
            return weather;
        }
    }
}
