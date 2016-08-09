using System;
using System.Threading.Tasks;

namespace DevangsWeather.ForecastIo
{
    public interface IForecastIoApiClient
    {
        Task<ForecastIoResponse> GetByCoordinatesAsync(float latitude, float longitude, DateTime? time = null);
    }
}