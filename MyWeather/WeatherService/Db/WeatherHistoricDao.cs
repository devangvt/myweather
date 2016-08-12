using DevangsWeather.Model;
using LiteDB;
using System.Linq;

namespace DevangsWeather.Service.Db
{
    public class WeatherHistoricDao
    {
        public static void AddHistoricWeather(WeatherHistoric historic)
        {

            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get customer collection
                var cityHistoricCollection = db.GetCollection<WeatherHistoric>("cityhistoric");

                // Insert new customer document (Id will be auto-incremented)
                cityHistoricCollection.Insert(historic);

                var existingRecords = cityHistoricCollection.Find(x => x.CityId.Equals(historic.CityId));

                if (existingRecords.Count() > 0)
                {
                    existingRecords.FirstOrDefault().Historic = historic.Historic;
                    cityHistoricCollection.Update(existingRecords.FirstOrDefault());
                }
                else
                {
                    // Index document using a document property
                    cityHistoricCollection.EnsureIndex(x => x.CityName);
                }

            }
        }

        public static WeatherHistoric GetCityHistoric(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var cityHistoricCollection = db.GetCollection<WeatherHistoric>("cityhistoric");

                return cityHistoricCollection.Find(x => x.CityName.Equals(cityName)).FirstOrDefault();

            }
        }

        public static void RemoveHistoric(string cityName)
        {
            using (var db = new LiteDatabase(@"WeatherData.db"))
            {
                // Get  cityForecastCollection
                var cityHistoricCollection = db.GetCollection<WeatherHistoric>("cityhistoric");
                var toDelete = cityHistoricCollection.Find(x => x.CityName.Equals(cityName)).FirstOrDefault();
                if (toDelete != null)
                {
                    cityHistoricCollection.Delete(toDelete.Id);
                }

            }
        }

    }
}
