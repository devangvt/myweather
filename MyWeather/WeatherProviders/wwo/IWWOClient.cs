using System.Threading.Tasks;
using DevangsWeather.Providers.wwo.Contracts;

namespace DevangsWeather.Providers.wwo
{
    public interface IWWOClient
    {
        Task<WWOSearchResponse> FindCityAsync(string city);

        Task<WWOWeatherResponse> GetCurrentWeather(string city);

        Task<WWOWeatherResponse> GetForecastWeather(string city);

        Task<WWOWeatherResponse> GetHistoricWeather(string city, string fromDate, string toDate);
    }
}