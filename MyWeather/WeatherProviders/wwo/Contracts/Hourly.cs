using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Providers.wwo.Contracts
{
    public class Hourly
    {
        public string tempC { get; set; }
        public string tempF { get; set; }
        public WeatherDesc[] weatherDesc { get; set; }
        public string weatherCode { get; set; }
    }
}
