using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevangsWeather.Service.Db;
using System.Collections.Generic;
using DevangsWeather.Model;

namespace DevangsWeather.WeatherServiceTest
{
    [TestClass]
    public class DbTester
    {
        [TestMethod]
        public void TestMethod1()
        {
          IEnumerable<City> list =  CityDao.GetCities();
        }
    }
}
