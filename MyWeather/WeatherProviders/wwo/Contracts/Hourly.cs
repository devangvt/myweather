namespace DevangsWeather.Providers.wwo.Contracts
{
    public class Hourly
    {
        public string tempC { get; set; }
        public string tempF { get; set; }
        public WeatherDesc[] weatherDesc { get; set; }
        public string weatherCode { get; set; }
    }
}
