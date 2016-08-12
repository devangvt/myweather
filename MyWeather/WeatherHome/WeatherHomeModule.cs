using DevangsWeather.Home.Views;
using Prism.Modularity;
using Prism.Regions;


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