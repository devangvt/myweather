using Newtonsoft.Json;

namespace DevangsWeather.OpenWeatherMap.Contracts
{
    public class Main
    {
        public float Temp { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public float TempMin { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public float TempMax { get; set; }
    }
}