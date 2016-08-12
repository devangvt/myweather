using System.Collections.Generic;
using System.Threading.Tasks;
using DevangsWeather.Model;
using DevangsWeather.WeatherProviderAdapters;
using DevangsWeather.Service.Db;
using System.Reflection;
using System;

namespace DevangsWeather.Service
{
    public class WeatherService : IWeatherService
    {
        private IWeatherProviderAdapter weatherAdapter;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        //TODO: Need to suppport disconnected mode. Send cached response when no internet connection.

        
        public WeatherService(IWeatherProviderAdapter adapter)
        {
            Log.Debug("Init WeatherService Ctr");
            weatherAdapter = adapter;
        }

        public async Task<IList<CurrentWeather>> FindAllCityCurrentWeather()
        {
            List<CurrentWeather> currentWeathers = null;
            try
            {
                currentWeathers = new List<CurrentWeather>();
                foreach (City c in new CityDao().GetCities())
                {
                    Log.Debug("Attempting to get weather for "+c.CityName);
                    currentWeathers.Add(await GetCurrentWeather(c.CityName));
                    Log.Debug("Done getting weather for " + c.CityName);
                }
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get weather for all cities", ex);
                throw;
            }
            return currentWeathers;
        }


        public async Task<City> GetCityByName(string cityName)
        {

            City city = null;
            try
            {
                city = await weatherAdapter.GetCityByName(cityName);
            }
            catch(Exception ex)
            {
                Log.Error("Failed to find city"+ cityName, ex);
                throw;
            }
            return city;
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city)
        {
            CurrentWeather currentWeather = null;
            try
            {
                Log.Debug("Attempt to get currentWeather for " + city);
                currentWeather = await this.weatherAdapter.GetCurrentWeather(city);
                Log.Debug("Done getting currentWeather for " + city);
                currentWeather.CityName = city;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to get currentweather for" + city, ex);
                throw;
            }
            return currentWeather;
        }

        public async Task<WeatherHistoric> GetWeatherForLastWeek(string city,int days=7)
        {
            WeatherHistoric historic = null;
            try
            {
                Log.Debug("Attempt to get historic weather for " + city);
                historic = await this.weatherAdapter.GetWeatherForLastWeek(city);
                Log.Debug("Done getting historic weather for " + city);
                historic.CityName = city;
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get Historic weather for" + city, ex);
                throw;
            }
            return historic;
        }

        public async Task<WeatherForcast> GetWeatherForNextWeek(string cityName, int days=7)
        {
            WeatherForcast forecast = null;
            try
            {
                Log.Debug("Attempt to get forecast weather for " + cityName);
                forecast = await this.weatherAdapter.GetWeatherForNextWeek(cityName);
                Log.Debug("Done getting forecast weather for " + cityName);
                forecast.CityName = cityName;
            }
            catch(Exception ex)
            {
                Log.Error("Failed to get forecast weather for" + cityName, ex);
                throw;
            }
            return forecast;

        }

        public async Task<bool> RemoveCity(string cityName)
        {
            bool result = false;
            try
            {
                Log.Debug("Removing City" + cityName);
                result = await Task<bool>.Run(() =>
                {
                    new CityDao().RemoveCity(cityName);
                    return true;
                });
                
            }
            catch (Exception ex)
            {
                Log.Error("Failed removing City" + cityName, ex);
                throw;
            }
            return result;
            
        }

        public  bool AddCity(City c)
        {
            try
            {
                CityDao.AddCity(c);
                return true;
            }
            catch(Exception ex)
            {
                Log.Debug("Failed to add city" + c.CityName, ex);
            }
            return false;

        }
    }
}
