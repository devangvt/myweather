using System.Threading.Tasks;
using DevangsWeather.Model;

namespace DevangsWeather.WeatherProviderAdapters
{
    public interface IWeatherProviderAdapter
    {
        Task<City> GetCityByName(string cityName);
        Task<CurrentWeather> GetCurrentWeather(string city);
        Task<WeatherHistoric> GetWeatherForLastWeek(string city, int days = 7);
        Task<WeatherForcast> GetWeatherForNextWeek(string city, int days = 7);
    }
}