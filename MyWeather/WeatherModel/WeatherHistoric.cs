using System.Collections.Generic;

namespace DevangsWeather.Model
{
    public class WeatherHistoric
    {
       public  List<WeatherTemplate> Historic { get; set; }
        public int Id { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
    }
}
