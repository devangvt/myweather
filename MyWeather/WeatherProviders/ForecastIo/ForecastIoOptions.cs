using System.Collections.Generic;
using DevangsWeather.ForecastIo.Settings;

namespace DevangsWeather.ForecastIo
{
    public class ForecastIoOptions
    {
        public string BaseUrl { get; set; } = "https://api.forecast.io/forecast";
        public string ApiKey { get; set; } = null;
        public Unit Unit { get; set; } = Unit.Auto;
        public List<Block> ExcludeBlocks { get; set; } = null;
        public bool ExtendHourlyData { get; set; } = false;
        public SupportedLanguage Language { get; set; } = SupportedLanguage.English;
    }
}