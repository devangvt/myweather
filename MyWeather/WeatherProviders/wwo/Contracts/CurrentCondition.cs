using System.Collections.Generic;

namespace DevangsWeather.Providers.wwo.Contracts
{
    public class CurrentCondition
    {
        public string observation_time { get; set; }
        public string temp_C { get; set; }
        public string temp_F { get; set; }
        public string weatherCode { get; set; }
        public List<WeatherDesc> weatherDesc { get; set; }
        public string humidity { get; set; }
        public string pressure { get; set; }
        public string localObsDateTime { get; set; }

    }
}
