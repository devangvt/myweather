using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevangsWeather.Providers.wwo;
using System.Threading.Tasks;
using DevangsWeather.Providers.wwo.Contracts;

namespace DevangsWeather.WeatherServiceTest
{
    [TestClass]
    public class WWOClientTest
    {
        [TestMethod]
        public void MyMethodTest()
        {

            IWWOClient c = new WWOClient("344f392b864143fd805153356161008");
            WWOSearchResponse result =  Task.Run(()=> c.FindCityAsync("Bangalore")).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetCurrentWeather()
        {

            IWWOClient c = new WWOClient("344f392b864143fd805153356161008");
            WWOWeatherResponse result = Task.Run(() => c.GetCurrentWeather("Bangalore City")).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetForecastWeather()
        {

            IWWOClient c = new WWOClient("344f392b864143fd805153356161008");
            WWOWeatherResponse result = Task.Run(() => c.GetForecastWeather("Bangalore City")).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetHistoricWeather()
        {

            IWWOClient c = new WWOClient("344f392b864143fd805153356161008");
            WWOWeatherResponse result = Task.Run(() => c.GetHistoricWeather("Bangalore City","2016-08-2","2016-08-08")).GetAwaiter().GetResult();
        }
    }
}
