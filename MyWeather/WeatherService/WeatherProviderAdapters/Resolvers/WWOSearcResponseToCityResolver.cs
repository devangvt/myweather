using AutoMapper;
using DevangsWeather.Model;
using DevangsWeather.Providers.wwo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Service.WeatherProviderAdapters.Resolvers
{
    internal class WWOSearcResponseToCityResolver : BaseResolver, ITypeConverter<WWOSearchResponse, City>
    {
        public City Convert(WWOSearchResponse source, City destination, ResolutionContext context)
        {
            destination = new City();
            destination.CityName = source.search_api.result.FirstOrDefault().areaName.FirstOrDefault().value;
            destination.Coordinates = new Coordinates
            {
                Lattitude = float.Parse(source.search_api.result.FirstOrDefault().latitude),
                Longitude = float.Parse(source.search_api.result.FirstOrDefault().longitude)
            };
            destination.Region = source.search_api.result.FirstOrDefault().region.FirstOrDefault().value;
            destination.Country = source.search_api.result.FirstOrDefault().country.FirstOrDefault().value;

            return destination;
        }
    }
}
