using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevangsWeather.Providers.wwo.Contracts;
using System.Net.Http;
using Newtonsoft.Json;

namespace DevangsWeather.Providers.wwo
{
    public class WWOClient : IWWOClient
    {
        private string apiKey = null;
        private readonly string baseUrl = "http://api.worldweatheronline.com/premium/v1";
        public async Task<WWOSearchResponse>  FindCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetSearchCityUrl(city));
            return await InvokeSearchAsync(url);
        }

        public async Task<WWOWeatherResponse> GetCurrentWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetCurrentWeatherUrl(city));
            return await GetWeatherAsync(url);
        }

        public async Task<WWOWeatherResponse> GetForecastWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetForecastWeatherUrl(city));
            return await GetWeatherAsync(url);
        }

        public async Task<WWOWeatherResponse> GetHistoricWeather(string city,string fromDate, string toDate)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetHistoricWeatherUrl(city, fromDate,  toDate));
            return await GetWeatherAsync(url);
        }


        private string GetCurrentWeatherUrl(string city)
        {
            var url = $"{baseUrl}/weather.ashx?q={city}&format=json&popular=true&num_of_days=0&&extra=localObsTime&date=today&mca=no&fx24=no&tp=24&showlocaltime=yes";

            return url;
        }

        private string GetForecastWeatherUrl(string city)
        {
            var url = $"{baseUrl}/weather.ashx?q={city}&format=json&num_of_days=7&cc=no&mca=no&fx24=no&tp=24&showlocaltime=yes";

            return url;
        }

        private string GetHistoricWeatherUrl(string city,string fromDate,string toDate)
        {
            var url = $"{baseUrl}/past-weather.ashx?q={city}&format=json&date={fromDate}&enddate={toDate}&tp=24";

            return url;
        }

        private static async Task<WWOSearchResponse> InvokeSearchAsync(string url)
        {
            var message = await new HttpClient().GetAsync(url).ConfigureAwait(false);
            try
            {
                message.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                ///TODO::LogError
                throw;
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            
            return JsonConvert.DeserializeObject<WWOSearchResponse>(await message.Content.ReadAsStringAsync().ConfigureAwait(false),settings);
        }

        private static async Task<WWOWeatherResponse> GetWeatherAsync(string url)
        {
            var message = await new HttpClient().GetAsync(url).ConfigureAwait(false);
            try
            {
                message.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                ///TODO::LogError
                throw;
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;

            return JsonConvert.DeserializeObject<WWOWeatherResponse>(await message.Content.ReadAsStringAsync().ConfigureAwait(false), settings);
        }



        private string GetSearchCityUrl(string city)
        {
            var url = $"{baseUrl}/search.ashx?q={city}&format=json&popular=true&num_of_results=1";

            return url;
        }

        private string ApplyOptionsToQuery(string url)
        {
            var temp = $"{url}&key={this.apiKey}";
            return temp;
        }

        public WWOClient(String apiKey)
        {
            this.apiKey = apiKey;
        }
    }
}
