using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Service;
using DevangsWeather.Service.WeatherProviderAdapters;
using DevangsWeather.WeatherProviderAdapters;
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
        private readonly IRegionManager regionManager = null;

        
        public WeatherHomeViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.showDetailsCommand = new DelegateCommand<object>(ShowDetails, CanShowDetails);
            PopulateCities();
        }

        private void PopulateCities()
        {

            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            IList<CityWeather> weather = Task.Run(() => service.FindAllCities()).GetAwaiter().GetResult();
            CityCollection = new ObservableCollection<CityWeather>(weather);
        }

       

        private ObservableCollection<CityWeather> cityCollection;
        public ObservableCollection<CityWeather> CityCollection
        {
            get { return cityCollection; }
            set {
                cityCollection = value;
                OnPropertyChanged(() => CityCollection); 
            }
        }

        private void ShowDetails(object data)
        {
            if (data is CityWeather)             {
                this.regionManager.RequestNavigate("MainContentRegion", "WeatherDetails?City="+ ((CityWeather)data).City.CityName);
            }

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

    }
}
