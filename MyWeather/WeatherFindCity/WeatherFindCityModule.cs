using DevangsWeather.FindCity.Views;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace DevangsWeather.FindCity
{
    public class WeatherFindCityModule : IModule
    {
        IRegionManager _regionManager;

        public WeatherFindCityModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            this._regionManager.RegisterViewWithRegion("MainContentRegion", typeof(FindAndAddCity));
        }
    }
}