using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Providers.wwo;
using DevangsWeather.Service;
using DevangsWeather.Service.WeatherProviderAdapters;
using DevangsWeather.WeatherProviderAdapters;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevangsWeather.Home.ViewModels
{
    public class WeatherHomeViewModel: BindableBase, IConfirmNavigationRequest
    {
        private readonly DelegateCommand<object> showDetailsCommand;
        private readonly DelegateCommand<object> removeCityCommand;
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer container = null;



        public WeatherHomeViewModel(IRegionManager regionManager,IUnityContainer container)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.showDetailsCommand = new DelegateCommand<object>(ShowDetails, CanShowDetails);
            this.removeCityCommand = new DelegateCommand<object>(RemoveCity);
            Task.Run(()=> PopulateCities());
        }

        private void PopulateCities()
        {
            IsLoading = true;
            IWWOClient client = new WWOClient(container.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            IList<CurrentWeather> weather = Task.Run(() => service.FindAllCityCurrentWeather()).GetAwaiter().GetResult();
            CityCollection = new ObservableCollection<CurrentWeather>(weather);
            IsLoading = false;
        }


        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set {
                isLoading = value;
                OnPropertyChanged(() => IsLoading);
            }
        }
        private ObservableCollection<CurrentWeather> cityCollection;
        public ObservableCollection<CurrentWeather> CityCollection
        {
            get { return cityCollection; }
            set {
                cityCollection = value;
                OnPropertyChanged(() => CityCollection); 
            }
        }

        private void ShowDetails(object data)
        {
            if (data is CurrentWeather)             {
                this.regionManager.RequestNavigate("MainContentRegion", "WeatherDetails?City="+ ((CurrentWeather)data).CityName);
            }

        }

        private void RemoveCity(object data)
        {
            IWWOClient client = new WWOClient(container.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            Task.Run(() => service.RemoveCity(((CurrentWeather)data).CityName)).ContinueWith((x)=> { PopulateCities(); });
        }

        private bool CanShowDetails(object ignored)
        {
            return true;
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PopulateCities();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           // throw new NotImplementedException();
        }

        public ICommand ShowDetailsCommand
        {
            get { return this.showDetailsCommand; }
        }

        public ICommand RemoveCityCommand
        {
            get { return this.removeCityCommand; }
        }

    }
}
