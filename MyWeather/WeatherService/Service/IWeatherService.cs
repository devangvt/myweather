using System;
using System.Collections.Generic;
using DevangsWeather.Model;
using System.Threading.Tasks;

namespace DevangsWeather.Service
{
    public interface IWeatherService
    {
        Task<CityWeather> GetTodaysWeather(String city);
        Task<City> FindCityByName(String cityName);
        Task<IList<CityWeather>> FindAllCities();
        Task<CityForcast> GetWeatherForNextWeek(String city, int days);
        void AddCity(City c);
        Task<IList<CityWeather>> GetWeatherHistory(String city);
    }
}
