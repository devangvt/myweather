using DevangsWeather.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.WeatherProviderAdapters
{
    public interface IWeatherProviderAdapter
    {        
        Task<City> GetCityByName(String cityName);
        
        Task<Weather> GetCurrentWeather(String city);
        Task<IList<Weather>> GetWeatherForNextWeek(String city, int days = 7);
        //Dictionary<DateTime, Weather> GetWeatherHistory(String city, int days = 7);
    }
}
