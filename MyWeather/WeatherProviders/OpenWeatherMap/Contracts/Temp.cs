using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Providers.OpenWeatherMap.Contracts
{
    public class Temp
    {
        public float Day { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
       
        public float Night { get; set; }
        
        public float Eve { get; set; }
        public float Morn { get; set; }
    }
}
