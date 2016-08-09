using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Model
{
    public class Weather
    {
        public int Id { get; set; }
        public BasicTempreture BasicTempreture { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
