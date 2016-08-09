using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevangsWeather.Model;

namespace DevangsWeather.Service.WeatherProviderAdapters
{
    public class ForcastIOAdapter : IWeatherService
    {
        public Task<City> FindCityByName(string cityName)
        {
            throw new NotImplementedException();
        }

        public Weather GetTodaysWeather(City city)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, Weather> GetWeatherForNextWeek(City city, int days)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, Weather> GetWeatherHistory(City city, int days)
        {
            throw new NotImplementedException();
        }

        
    }
}
