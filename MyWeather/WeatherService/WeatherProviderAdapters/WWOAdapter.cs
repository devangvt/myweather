using System;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo;
using DevangsWeather.Providers.wwo.Contracts;
using AutoMapper;
using DevangsWeather.Service.WeatherProviderAdapters.Resolvers;

namespace DevangsWeather.WeatherProviderAdapters
{
    public class WWOAdapter : IWeatherProviderAdapter
    {

        private IWWOClient client  = null;
        public WWOAdapter(IWWOClient wwoClient)
        {
            this.client = wwoClient;

            Mapper.Initialize(cfg => {
                cfg.CreateMap<WWOSearchResponse, City>().ConvertUsing<WWOSearcResponseToCityResolver>();
                cfg.CreateMap<WWOWeatherResponse, CurrentWeather>().ConvertUsing<WWOCurrentWeatherResponseToCurrentWeather>();
                cfg.CreateMap<WWOWeatherResponse, WeatherForcast>().ConvertUsing<WWOWeatherResponseToWeatherForecast>();
                cfg.CreateMap<WWOWeatherResponse, WeatherHistoric>().ConvertUsing<WWOWeatherResponseToWeatherHistoric>();
            });
            Mapper.Configuration.AssertConfigurationIsValid();
        }
        public async Task<City> GetCityByName(string cityName)
        {
          WWOSearchResponse response =  await this.client.FindCityAsync(cityName);
            City city =   Mapper.Map<City>(response);
            return city;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city)
        {
            WWOWeatherResponse response = await this.client.GetCurrentWeather(city);
           CurrentWeather weather =  Mapper.Map<CurrentWeather>(response);
            return weather;
            
        }

        public async Task<WeatherForcast> GetWeatherForNextWeek(string city, int days = 7)
        {
            WWOWeatherResponse response = await this.client.GetForecastWeather(city);
            WeatherForcast weather = Mapper.Map<WeatherForcast>(response);
            return weather;
        }

        public async Task<WeatherHistoric> GetWeatherForLastWeek(string city, int days = 7)
        {
            string toDate =  String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-1));
            string fromDate = String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-7));
            WWOWeatherResponse response = await this.client.GetHistoricWeather(city,fromDate,toDate);
            WeatherHistoric weather = Mapper.Map<WeatherHistoric>(response);
            return weather;
        }
    }
}
