using DevangsWeather.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.Db
{
    public class CityDao
    {
        public static void AddCity(City city)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cities = db.GetCollection<City>("city");

                var foundCities = cities.Find(x => x.CityName.Equals(city.CityName));

                if(foundCities.Count() == 0)
                {
                    cities.Insert(city);
                    // Index document using a document property
                    cities.EnsureIndex(x => x.CityName);
                }
            }
        }

        public static City GetCity(String cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cities = db.GetCollection<City>("city");
                return cities.Find(x=>x.CityName.Equals(cityName)).FirstOrDefault();
            }
        }

        public static IEnumerable<City> GetCities()
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
