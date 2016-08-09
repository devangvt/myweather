using DevangsWeather.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.Db
{
    public class CityForcastDao
    {
        public static void AddCityForcast(CityForcast cityForecast)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cityForecastCollection = db.GetCollection<CityForcast>("cityforecast");

                // Insert new customer document (Id will be auto-incremented)
                cityForecastCollection.Insert(cityForecast);

                var existingRecords = cityForecastCollection.Find(x => x.City.CityName.Equals(cityForecast.City.CityName));

                if (existingRecords.Count() > 0)
                {
                    existingRecords.FirstOrDefault().Forecast = cityForecast.Forecast;
                }
                else
                {
                    // Index document using a document property
                    cityForecastCollection.EnsureIndex(x => x.City.CityName);
                }

                ///TODO:update if record exists.
            }
        }

        public static CityForcast GetCityForcast(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var cityForecastCollection = db.GetCollection<CityForcast>("cityforecast");

                return cityForecastCollection.Find(x => x.City.CityName.Equals(cityName)).FirstOrDefault();

            }
        }
    }
}
