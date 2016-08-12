using System.Collections.Generic;

namespace DevangsWeather.Model
{
    public class WeatherForcast
    {
        public List<WeatherTemplate> Forecast { get; set; }
        public int Id { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
    }
}
