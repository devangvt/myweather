using DevangsWeather.Home.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;


namespace DevangsWeather.Home
{
    public class WeatherHomeModule : IModule
    {
        IRegionManager _regionManager;
        

        public WeatherHomeModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
            
        }

        public void Initialize()
        {
            this._regionManager.RegisterViewWithRegion("MainContentRegion", typeof(WeatherHome));
        }
    }
}