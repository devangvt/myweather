using DevangsWeather.Model;
using LiteDB;
using System.Linq;

namespace DevangsWeather.Service.Db
{
    public class CurrentWeatherDao
    {
        private object locker = new object();
        public  void AddCurrentWeather(CurrentWeather weather)
        {
            lock (locker)
            {
                using (var db = new LiteDatabase(@"WeatherData.db"))
                {
                    using (db.BeginTrans())
                    {
                        // Get customer collection
                        var currentWeatherCollection = db.GetCollection<CurrentWeather>("currentweather");



                        var existingRecords = currentWeatherCollection.Find(x => x.CityName.Equals(weather.CityName));

                        if (existingRecords.Count() > 0)
                        {
                            var existing = existingRecords.FirstOrDefault();
                            existing.CityName = weather.CityName;
                            existing.CollectionTime = weather.CollectionTime;
                            existing.Humidity = weather.Humidity;
                            existing.Pressure = weather.Pressure;
                            existing.Temp_C = weather.Temp_C;
                            existing.Temp_F = weather.Temp_F;
                            existing.WeatherCode = weather.WeatherCode;
                            existing.WeatherDesc = weather.WeatherDesc;
                            //Update old
                            currentWeatherCollection.Update(existing);
                        }
                        else
                        {
                            // Index document using a document property

                            // Insert new weather document (Id will be auto-incremented)
                            currentWeatherCollection.Insert(weather);
                            currentWeatherCollection.EnsureIndex(x => x.CityName);
                        }

                    }
                }
            }
        }

        public  CurrentWeather GetCurrentWeather(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var currentWeatherCollection = db.GetCollection<CurrentWeather>("currentweather");

                return currentWeatherCollection.Find(x => x.CityName.Equals(cityName)).FirstOrDefault();

            }
        }
    }
}
