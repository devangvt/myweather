using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Providers.wwo.Contracts
{
    public class Data
    {
        public List<CurrentCondition> current_condition { get; set; }
        public List<Weather> weather { get; set; }
        //public List<Request> request { get; set; }
    }
}
