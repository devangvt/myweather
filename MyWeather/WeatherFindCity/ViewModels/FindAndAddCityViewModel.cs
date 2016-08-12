using DevangsWeather.Model;
using DevangsWeather.Service;
using DevangsWeather.WeatherProviderAdapters;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using DevangsWeather.Providers.wwo;

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

            IWWOClient client = new WWOClient(unityContainer.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            service.AddCity(Result);
            //CurrentWeather weather = Task.Run(() => service.GetCurrentWeather(Result.CityName)).GetAwaiter().GetResult();
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
            if (!string.IsNullOrEmpty(data.ToString()))
            {
                IWWOClient client = new WWOClient(unityContainer.Resolve<String>("apiKey"));
                IWeatherProviderAdapter adapter = new WWOAdapter(client);
                IWeatherService service = new WeatherService(adapter);
                City city = null;
                try
                {
                    city = Task.Run(() => service.GetCityByName(data.ToString())).GetAwaiter().GetResult();
                }
                catch
                {

                }
                
                if (city != null)
                {
                    Result = city;

                }
                else
                {
                    Result = null;
                }
            }
        }

        private bool CanSearchCity(object ignored)
        {
            return true;
        }
    }
}
