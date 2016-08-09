using DevangsWeather.OpenWeatherMap.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Providers.OpenWeatherMap.Contracts
{
    public class List
    {
        public long Dt { get; set; }
        public Temp Temp { get; set; }
        public float Preassure { get; set; }
        public float Humidity { get; set; }
        public List<Weather> Weather { get; set; }
        public float Speed { get; set; }
        public float Deg { get; set; }
        public float Cloud { get; set; }
    }
}
