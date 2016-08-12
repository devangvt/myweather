using System.Collections.Generic;

namespace DevangsWeather.Providers.wwo.Contracts
{
    public class Weather
    {
        public string date { get; set; }
        public string maxtempC { get; set; }
        public string maxtempF { get; set; }
        public string mintempC { get; set; }
        public string mintempF{ get; set; }
        public List<Hourly> hourly { get; set; }
    }
}
