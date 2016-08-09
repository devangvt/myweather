using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Model
{
    public class CityWeather
    {
        public City City { get; set; }
        public Weather CurrentWeather { get; set; }   
        public Guid Id { get; set; }     
    }
}
