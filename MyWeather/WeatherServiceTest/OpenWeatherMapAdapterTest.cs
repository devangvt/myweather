using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Service.WeatherProviderAdapters;
using System.Threading.Tasks;
using DevangsWeather.Model;
using FakeItEasy;
using System.Collections.Generic;

namespace DevangsWeather.WeatherServiceTest
{
    [TestClass]
    public class OpenWeatherMapAdapterTest
    {
        //[TestMethod]
        //public void GetCityByName_should_return_value_from_api()
        //{
        //    OpenWeatherMapOptions options = new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" };
        //    IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(options);
        //    var weatherProviderAdapter = new OpenWeatherMapAdapter(client);
        //    var city = Task.Run(() => weatherProviderAdapter.GetCityByName("Mumbai")).GetAwaiter().GetResult();
        //    Assert.AreEqual(city.CityName, "Mumbai");
        //}

        //[TestMethod]
        //public void GetCurrentWeather_should_return_weather_for_city()
        //{
        //    OpenWeatherMapOptions options = new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" };
        //    IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(options);
        //    var weatherProviderAdapter = new OpenWeatherMapAdapter(client);
        //    var city = Task.Run(()=> weatherProviderAdapter.GetCityByName("London")).GetAwaiter().GetResult();
        //    Assert.AreEqual(city.CityName, "London");

        //    var weather = Task.Run(()=> weatherProviderAdapter.GetCurrentWeather(city.CityName)).GetAwaiter().GetResult();
        //    Assert.IsNotNull(weather);
        //}

        //[TestMethod]
        //public void GetCityByName_should_call_openWeatherMapClient()
        //{
        //    var optionsFake = A.Fake<OpenWeatherMapOptions>();
        //    optionsFake.ApiKey = "fakeKey";
        //    var clientFake = A.Fake<IOpenWeatherMapApiClient>();
            
        //    var weatherProviderAdapter = new OpenWeatherMapAdapter(clientFake);
        //    var responseFake = A.Fake<OpenWeatherMapResponse>();
        //    responseFake.Name = "Bangalore";
        //    responseFake.Id = 123;
            
        //    responseFake.Coord = new OpenWeatherMap.Contracts.Coord { Lat = 123432, Lon = 222333 };
        //    A.CallTo(() => clientFake.GetByCityAsync("Bangalore",null)).Returns(responseFake);
        //    var resp = Task.Run(()=>weatherProviderAdapter.GetCityByName("Bangalore")).GetAwaiter().GetResult();

        //    A.CallTo(() => clientFake.GetByCityAsync("Bangalore", null)).MustHaveHappened();

        //}

        //[TestMethod]
        //public void GetWeatherForNextWeek_should_return_sevenday_weather_for_city()
        //{

        //    //OpenWeatherMapOptions options = new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" };
        //    //IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(options);
        //    //var weatherProviderAdapter = new OpenWeatherMapAdapter(client);
        //    //Task<City> city = weatherProviderAdapter.GetCityByName("Mumbai");

        //    //var result = Task.Run(() => weatherProviderAdapter.GetWeatherForNextWeek(city.Result.CityName)).GetAwaiter().GetResult();
        //    //Assert.AreEqual(result.Count, 7);
        //}
    }
}
