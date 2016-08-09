using DevangsWeather.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.Db
{
    public class CityWeatherDao
    {
        public static void AddCityWeather(CityWeather cityWeather)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cityWeatherCollection = db.GetCollection<CityWeather>("cityweather");                

                var existingRecords = cityWeatherCollection.Find(x => x.City.CityName.Equals(cityWeather.City.CityName));

                if (existingRecords.Count() > 7)
                {
                    var recordTOUpdate = existingRecords.OrderByDescending(x => x.CurrentWeather.Date).FirstOrDefault();
                    recordTOUpdate.CurrentWeather = cityWeather.CurrentWeather;
                    cityWeatherCollection.Update(recordTOUpdate.Id,recordTOUpdate);
                }
                else
                {

                    // Insert new customer document (Id will be auto-incremented)
                    cityWeatherCollection.Insert(cityWeather);
                    
                    // Index document using a document property
                    cityWeatherCollection.EnsureIndex(x => x.CurrentWeather.Date);
                }

                
            }
        }

        //public static void UpdateCityWeather(CityWeather cityWeather)
        //{
        //    using (var db = new LiteDatabase(@"WeatherData.db"))
        //    {
        //        // Get customer collection
        //        var cityWeatherCollection = db.GetCollection<CityWeather>("cityweather");

        //        // Get  collection
                
        //        var recordToUpdate=  cityWeatherCollection.Find(x => x.City.CityName.Equals(cityWeather.City.CityName)).
        //            Where(x=>x.CurrentWeather.Date.Date.Equals(cityWeather.CurrentWeather.Date.Date.Date)).
        //            OrderByDescending(x => x.CurrentWeather.Date).FirstOrDefault();

        //        recordToUpdate.CurrentWeather = cityWeather.CurrentWeather;
        //        cityWeatherCollection.Update(recordToUpdate);
        //    }
        //}

        public static IEnumerable<CityWeather> GetHistoricCityWeather(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cityWeatherCollection = db.GetCollection<CityWeather>("cityweather");
                return cityWeatherCollection.Find(x => x.City.CityName.Equals(cityName));
                ///TODO:Add check to replace older than 7 day.
            }
        }

        public static CityWeather GetCurrentCityWeather(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  collection
                var cityWeatherCollection = db.GetCollection<CityWeather>("cityweather");
                return cityWeatherCollection.Find(x => x.City.CityName.Equals(cityName)).
                    OrderByDescending(x=>x.CurrentWeather.Date).FirstOrDefault();
                ///TODO:Add check to replace older than 7 day.
            }
        }

        
    }
}
