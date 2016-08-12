using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Model
{
    public class City
    {
       
        public String CityName { get; set; }
        public Coordinates Coordinates { get; set; }
        public String Tag { get; set; }
        public String Country { get; set; }
        public String Region { get; set; }
        public int Id { get; set; }

    }
}
