using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using DevangsWeather.Service;
using DevangsWeather.Model;
using DevangsWeather.WeatherProviderAdapters;
using System.Threading.Tasks;
using DevangsWeather.Providers.wwo;
using System.Configuration;

namespace WeatherServiceTest
{
    [TestClass]
    public class WeatherServiceTest
    {
        //Fake<OpenWeatherMapOptions> openWeatherMapOptionsFake = null;
        IWeatherProviderAdapter adapter = null;

        [TestInitialize]
        public void Init()
        {
            string apiKey = ConfigurationManager.AppSettings["apiKey"];
            IWWOClient client = new WWOClient(apiKey);
            adapter = new WWOAdapter(client);
        }

        [TestMethod]
        public void FindCityByName_should_call_weatherProviderAdapter_getCityByName()
        {
            var weatherProviderAdapter = A.Fake<IWeatherProviderAdapter>();
            var weatherService = new WeatherService(weatherProviderAdapter);
            var bangalore = A.Fake<City>();
            bangalore.CityName = "Bangalore";
            A.CallTo(() => weatherProviderAdapter.GetCityByName("Bangalore")).Returns(bangalore);

            Task<City> c =  weatherService.GetCityByName("Bangalore");
            A.CallTo(() => weatherProviderAdapter.GetCityByName("Bangalore")).MustHaveHappened();

        }

        [TestMethod]
        public void FindCityByName_should_contact_provider_api()
        {
           
            var weatherService = new WeatherService(adapter);
           City m = Task.Run(()=> weatherService.GetCityByName("Mumbai")).GetAwaiter().GetResult();
            Assert.AreEqual(m.CityName, "Mumbai");

            City c = Task.Run(() => weatherService.GetCityByName("Bangalore")).GetAwaiter().GetResult();
            Assert.AreEqual(c.CityName, "Bangalore City");
            City x = Task.Run(() => weatherService.GetCityByName("Delhi")).GetAwaiter().GetResult();
            Assert.AreEqual(x.CityName, "Delhi Sabzimandi");
        }

        [TestMethod]
        public void GetWeatherForNextWeek_should_contact_provider_api()
        {

            var weatherService = new WeatherService(adapter);
            WeatherForcast forecast = Task.Run(() => weatherService.GetWeatherForNextWeek("Mumbai")).GetAwaiter().GetResult();
            Assert.AreEqual(forecast.Forecast.Count, 7);
        }


        [TestMethod]
        public void GetWeatherForLastWeek_should_contact_provider_api()
        {

            var weatherService = new WeatherService(adapter);
            WeatherHistoric forecast = Task.Run(() => weatherService.GetWeatherForLastWeek("Mumbai")).GetAwaiter().GetResult();
            Assert.AreEqual(forecast.Historic.Count, 7);
        }



    }
}
