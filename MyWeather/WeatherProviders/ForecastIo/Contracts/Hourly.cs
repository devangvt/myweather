using System.Collections.Generic;

namespace DevangsWeather.ForecastIo.Contracts
{
    public class Hourly
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public List<HourForecast> Data { get; set; }
    }
}