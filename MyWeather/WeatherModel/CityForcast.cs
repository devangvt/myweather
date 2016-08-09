using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Model
{
    public class CityForcast
    {
        public City City { get; set; }
        public IList<Weather> Forecast { get; set; }
        public Guid Id { get; set; }
    }
}
