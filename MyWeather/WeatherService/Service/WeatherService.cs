using System.Collections.Generic;
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
        
        //TODO: Need to suppport disconnected mode. Send cached response when no internet connection.

        
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
            
            WeatherHistoricDao.AddHistoricWeather(historic);
            return historic;
        }

        public async Task<WeatherForcast> GetWeatherForNextWeek(string cityName, int days=7)
        {
            WeatherForcast forecast = await this.weatherAdapter.GetWeatherForNextWeek(cityName);
            forecast.CityName = cityName;
            
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
