using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Service;
using DevangsWeather.Service.WeatherProviderAdapters;
using DevangsWeather.WeatherProviderAdapters;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows.Input;
using System;

namespace DevangsWeather.FindCity.ViewModels
{
    public class FindAndAddCityViewModel : BindableBase
    {
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer unityContainer = null;
        private readonly DelegateCommand<object> searchCityCommand;
        private readonly DelegateCommand<object> addCityCommand;


        public FindAndAddCityViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.unityContainer = container;
            this.searchCityCommand = new DelegateCommand<object>(SearchCity, CanSearchCity);
            this.addCityCommand = new DelegateCommand<object>(AddCity, CanAddCity);
        }

        private bool CanAddCity(object arg)
        {
            return true;
        }

        public ICommand AddCityCommand
        {
            get { return this.addCityCommand; }
        }
        private void AddCity(object obj)
        {
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            service.AddCity(Result);
            CityWeather weather = Task.Run(() => service.GetTodaysWeather(Result.CityName)).GetAwaiter().GetResult();
            this.regionManager.RequestNavigate("MainContentRegion", "WeatherHome");
        }

        public ICommand SearchCommand
        {
            get { return this.searchCityCommand; }
        }

        private City result;
        public City Result
        {
            get { return result; }
            set {
                result =value ;
                OnPropertyChanged(() => Result);
            }
        }

        private void SearchCity(object data)
        {
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            City city = Task.Run(() => service.FindCityByName(data.ToString())).GetAwaiter().GetResult();
            if(city!= null)
            {
                Result = city;

            }else
            {
                Result = null;
            }
        }

        private bool CanSearchCity(object ignored)
        {
            return true;
        }
    }
}
