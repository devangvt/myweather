using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.WeatherProviderAdapters;
using DevangsWeather.Service.Db;
using System.Reflection;
using DevangsWeather.Service.WeatherProviderAdapters;

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

        public async Task<IList<CurrentWeather>> FindAllCityCurrentWeather()
        {
            List<CurrentWeather> currentWeathers = new List<CurrentWeather>();
           foreach(City c in new CityDao().GetCities())
            {
                currentWeathers.Add(await GetCurrentWeather(c.CityName));
            }
            return currentWeathers;
        }

        //public async Task<City> FindCityByName(string cityName)
        //{
        //    City city = await weatherAdapter.GetCityByName(cityName);

        //    return city;
        //}

        //public async Task<IList<CityWeather>> FindAllCities()
        //{
        //    IList<CityWeather> citiesAndWeathers = new List<CityWeather>();

        //   IEnumerable<City> cities = CityDao.GetCities();
        //    foreach (City c in cities)
        //    {
        //       citiesAndWeathers.Add(CityWeatherDao.GetCurrentCityWeather(c.CityName));
        //    }
        //    return citiesAndWeathers;
        //}

        //public async Task<CityWeather> GetTodaysWeather(String cityName)
        //{
        //    City c = await FindCityByName(cityName);
        //    Weather weather = await weatherAdapter.GetCurrentWeather(cityName);
        //    CityWeather cw = new CityWeather() { City = c, CurrentWeather = weather };
        //    cw.Id = new Guid();
        //    CityWeatherDao.AddCityWeather(cw);
        //    return cw;
        //}

        //public void AddCity(City c)
        //{
        //    CityDao.AddCity(c);
        //}



        //public Task<IList<CityWeather>> GetWeatherHistory(String city)
        //{
        //    IEnumerable<CityWeather> weatherHistory = CityWeatherDao.GetHistoricCityWeather(city);

        //    Task<IList<CityWeather>> task = new Task<IList<CityWeather>>(()=>CityWeatherDao.GetHistoricCityWeather(city).ToList());
        //    task.Start();
        //    return task;
        //}

        //public async Task<CityForcast> GetWeatherForNextWeek(string cityName, int days)
        //{
        //   // IList<Weather> forecast = await weatherAdapter.GetWeatherForNextWeek(cityName);
        //   //City city = CityDao.GetCity(cityName);
        //   // CityForcast cityForecast = new CityForcast();
        //   // cityForecast.City = city;
        //   // cityForecast.Forecast = forecast;
        //   // cityForecast.Id =  new Guid();
        //   // CityForcastDao.AddCityForcast(cityForecast);
        //   // return cityForecast;
        //}

        public async Task<City> GetCityByName(string cityName)
        {
            City city = await weatherAdapter.GetCityByName(cityName);

            return city;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city)
        {

            CurrentWeather currentWeather = await this.weatherAdapter.GetCurrentWeather(city);
            currentWeather.CityName = city;
            //new CurrentWeatherDao().AddCurrentWeather(currentWeather);
            //CityForcastDao.AddCityForcast(cityForecast);
            return currentWeather;

        }

        public async Task<WeatherHistoric> GetWeatherForLastWeek(string city,int days=7)
        {
            WeatherHistoric historic = await this.weatherAdapter.GetWeatherForLastWeek(city);
            historic.CityName = city;
            //CityForcastDao.AddCityForcast(cityForecast);
            WeatherHistoricDao.AddHistoricWeather(historic);
            return historic;
        }

        public async Task<WeatherForcast> GetWeatherForNextWeek(string cityName, int days=7)
        {
            WeatherForcast forecast = await this.weatherAdapter.GetWeatherForNextWeek(cityName);
            forecast.CityName = cityName;
            //CityForcastDao.AddCityForcast(cityForecast);
            WeatherForcastDao.AddCityForcast(forecast);
            return forecast;

        }

        public async Task<bool> RemoveCity(string cityName)
        {
            var result = await Task<bool>.Run(() => {
                WeatherHistoricDao.RemoveHistoric(cityName);
                WeatherForcastDao.RemoveForecast(cityName);
                new CityDao().RemoveCity(cityName);
                return true;
            });

            return result ;
        }

        public  bool AddCity(City c)
        {
            new CityDao().AddCity(c);            
            return true;

        }
    }
}
