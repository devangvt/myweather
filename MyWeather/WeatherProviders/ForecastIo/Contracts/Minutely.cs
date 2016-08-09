using System.Collections.Generic;

namespace DevangsWeather.ForecastIo.Contracts
{
    public class Minutely
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public List<MinuteForecast> Data { get; set; }
    }
}