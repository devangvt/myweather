namespace DevangsWeather.Providers.wwo.Contracts
{
    public class Result
    {
        public AreaName[] areaName { get; set; }
        public Country[] country { get; set; }
        public Region[] region { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string population { get; set; }

    }
}