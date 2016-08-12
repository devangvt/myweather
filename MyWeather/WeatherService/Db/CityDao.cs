using DevangsWeather.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevangsWeather.Service.Db
{
    public class CityDao
    {
        private object locker = new object();
        public void AddCity(City city)
        {
            lock (locker)
            {
                using (var db = new LiteDatabase(@"WeatherData.db"))
                {
                    // Get customer collection
                    var cities = db.GetCollection<City>("city");

                    var foundCities = cities.Find(x => x.CityName.Equals(city.CityName));

                    if (foundCities.Count() == 0)
                    {


                        cities.EnsureIndex(x => x.Id);
                        // Index document using a document property
                        cities.EnsureIndex(x => x.CityName);

                        cities.Insert(city);
                    }
                }
            }
        }

        public  City GetCity(String cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cities = db.GetCollection<City>("city");
                return cities.Find(x=>x.CityName.Equals(cityName)).FirstOrDefault();
            }
        }

        public  void RemoveCity(String cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cities = db.GetCollection<City>("city");
                var cityToDelete = GetCity(cityName);
                cities.Delete(cityToDelete.Id);
            }
        }

        public  IEnumerable<City> GetCities()
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cities = db.GetCollection<City>("city");
                return cities.FindAll();
            }
        }
    }
}
