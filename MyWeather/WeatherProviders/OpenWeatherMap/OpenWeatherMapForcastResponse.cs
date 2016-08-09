using DevangsWeather.Providers.OpenWeatherMap.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.OpenWeatherMap
{
    public class OpenWeatherMapForcastResponse
    {
        public City City { get; set; }
        public string Cod { get; set; }
        public int Count { get; set; }
        public List<List> list { get; set; }
    }
}
