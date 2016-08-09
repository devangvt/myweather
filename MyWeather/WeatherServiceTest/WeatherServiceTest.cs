using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Service;
using DevangsWeather.Model;
using DevangsWeather.Service.WeatherProviderAdapters;
using DevangsWeather.WeatherProviderAdapters;
using System.Threading.Tasks;

namespace WeatherServiceTest
{
    [TestClass]
    public class WeatherServiceTest
    {
        Fake<OpenWeatherMapOptions> openWeatherMapOptionsFake = null;
       

        [TestMethod]
        public void FindCityByName_should_call_weatherProviderAdapter_getCityByName()
        {
            var weatherProviderAdapter = A.Fake<IWeatherProviderAdapter>();
            var weatherService = new WeatherService(weatherProviderAdapter);
            var bangalore = A.Fake<City>();
            bangalore.CityName = "Bangalore";
            A.CallTo(() => weatherProviderAdapter.GetCityByName("Bangalore")).Returns(bangalore);

            Task<City> c =  weatherService.FindCityByName("Bangalore");
            A.CallTo(() => weatherProviderAdapter.GetCityByName("Bangalore")).MustHaveHappened();

        }

        [TestMethod]
        public void FindCityByName_should_contact_provider_api()
        {
            OpenWeatherMapOptions options = new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" };
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(options);
            var weatherProviderAdapter = new OpenWeatherMapAdapter(client);
            var weatherService = new WeatherService(weatherProviderAdapter);
            Task<City> city =  weatherService.FindCityByName("Mumbai");
            Assert.AreEqual(city.Result.CityName, "Mumbai");

            City c = Task.Run(() => weatherService.FindCityByName("Bangalore")).GetAwaiter().GetResult();
            City x = Task.Run(() => weatherService.FindCityByName("Delhi")).GetAwaiter().GetResult(); 
        }
    }
}
