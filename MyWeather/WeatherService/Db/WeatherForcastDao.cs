using DevangsWeather.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.Db
{
    public class WeatherForcastDao
    {
        public static void AddCityForcast(WeatherForcast forecast)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cityForecastCollection = db.GetCollection<WeatherForcast>("cityforecast");

                // Insert new customer document (Id will be auto-incremented)
                cityForecastCollection.Insert(forecast);

                var existingRecords = cityForecastCollection.Find(x => x.CityId.Equals(forecast.CityId));

                if (existingRecords.Count() > 0)
                {
                    existingRecords.FirstOrDefault().Forecast = forecast.Forecast;
                }
                else
                {
                    // Index document using a document property
                    cityForecastCollection.EnsureIndex(x => x.CityName);
                }

                ///TODO:update if record exists.
            }
        }

        public static WeatherForcast GetCityForcast(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var cityForecastCollection = db.GetCollection<WeatherForcast>("cityforecast");

                return cityForecastCollection.Find(x => x.CityName.Equals(cityName)).FirstOrDefault();

            }
        }

        public static void RemoveForecast(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var cityForecastCollection = db.GetCollection<WeatherForcast>("cityforecast");
                var toDelete = cityForecastCollection.Find(x => x.CityName.Equals(cityName)).FirstOrDefault();
                if (toDelete != null)
                {
                    cityForecastCollection.Delete(toDelete.Id);
                }
            }
        }
    }
}
