using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System.Linq;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOSearcResponseToCityResolver : BaseResolver, ITypeConverter<WWOSearchResponse, City>
    {
        public City Convert(WWOSearchResponse source, City destination, ResolutionContext context)
        {
            
            if (source.search_api.result.Count() > 0)
            {
                var result = source.search_api.result.FirstOrDefault();
                destination = new City();
                destination.CityName = result.areaName.FirstOrDefault().value;
                destination.Coordinates = new Coordinates
                {
                    Lattitude = float.Parse(result.latitude),
                    Longitude = float.Parse(result.longitude)
                };
                destination.Region = result.region.FirstOrDefault().value;
                destination.Country = result.country.FirstOrDefault().value;
            }
            return destination;
        }
    }
}
