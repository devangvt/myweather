using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.WeatherProviderAdapters;
using DevangsWeather.Service.Db;
using System.Reflection;

namespace DevangsWeather.Service
{
    public class WeatherService : IWeatherService
    {
        private IWeatherProviderAdapter weatherAdapter;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public WeatherService(IWeatherProviderAdapter adapter)
        {
            weatherAdapter = adapter;
        }

        public async Task<City> FindCityByName(string cityName)
        {
            City city = await weatherAdapter.GetCityByName(cityName);
          
            return city;
        }

        public async Task<IList<CityWeather>> FindAllCities()
        {
            IList<CityWeather> citiesAndWeathers = new List<CityWeather>();

           IEnumerable<City> cities = CityDao.GetCities();
            foreach (City c in cities)
            {
               citiesAndWeathers.Add(CityWeatherDao.GetCurrentCityWeather(c.CityName));
            }
            return citiesAndWeathers;
        }

        public async Task<CityWeather> GetTodaysWeather(String cityName)
        {
            City c = await FindCityByName(cityName);
            Weather weather = await weatherAdapter.GetCurrentWeather(cityName);
            CityWeather cw = new CityWeather() { City = c, CurrentWeather = weather };
            cw.Id = new Guid();
            CityWeatherDao.AddCityWeather(cw);
            return cw;
        }

        public void AddCity(City c)
        {
            CityDao.AddCity(c);
        }

       

        public Task<IList<CityWeather>> GetWeatherHistory(String city)
        {
            IEnumerable<CityWeather> weatherHistory = CityWeatherDao.GetHistoricCityWeather(city);
           
            Task<IList<CityWeather>> task = new Task<IList<CityWeather>>(()=>CityWeatherDao.GetHistoricCityWeather(city).ToList());
            task.Start();
            return task;
        }

        public async Task<CityForcast> GetWeatherForNextWeek(string cityName, int days)
        {
            IList<Weather> forecast = await weatherAdapter.GetWeatherForNextWeek(cityName);
           City city = CityDao.GetCity(cityName);
            CityForcast cityForecast = new CityForcast();
            cityForecast.City = city;
            cityForecast.Forecast = forecast;
            cityForecast.Id =  new Guid();
            CityForcastDao.AddCityForcast(cityForecast);
            return cityForecast;
        }
    }
}
