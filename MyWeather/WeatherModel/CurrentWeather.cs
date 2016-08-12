namespace DevangsWeather.Model
{
    public class CurrentWeather
    {
        public int Id { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string LatLong { get; set; }
        public string CollectionTime { get; set; }
        public string Temp_C { get; set; }
        public string Temp_F { get; set; }
        public string WeatherCode { get; set; }
        public string WeatherDesc { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string LocalObsTime { get; set; }
        public bool IsDay { get; set; }
        public string Icon { get; set; }
        public string Message { get; set; }
        public string SmileIcon { get; set; }


    }
}
