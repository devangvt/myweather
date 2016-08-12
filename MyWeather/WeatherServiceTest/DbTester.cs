using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevangsWeather.Service.Db;
using System.Collections.Generic;
using DevangsWeather.Model;
using System.Threading.Tasks;

namespace DevangsWeather.WeatherServiceTest
{
    [TestClass]
    public class DbTester
    {
        [TestMethod]
        public void AddCityToDB()
        {
           City c = new City { CityName = "Blr" };
            City c2 = new City { CityName = "Blr2" };
             CityDao.AddCity(c);
             CityDao.AddCity(c2);
        }

        [TestMethod]
        public void AddMultipleCitiesToDB()
        {
            IEnumerable<City> list =  new CityDao().GetCities();
            foreach (City ci in list)
            {
                CurrentWeather c = new CurrentWeather();
                c.CityName = ci.CityName;
                c.CollectionTime = "123";
                c.Humidity = "12";
                c.Pressure = "3";
                c.Temp_C = "23";

                Task.Run(() => new CurrentWeatherDao().AddCurrentWeather(c));
               
            }
        }
    }
}
