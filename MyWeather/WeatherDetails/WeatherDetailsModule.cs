using DevangsWeather.Details.Views;
using Prism.Modularity;
using Prism.Regions;

namespace DevangsWeather.Details
{
    public class WeatherDetailsModule : IModule
    {
        IRegionManager _regionManager;

        public WeatherDetailsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            this._regionManager.RegisterViewWithRegion("MainContentRegion", typeof(WeatherDetails));
        }
    }
}