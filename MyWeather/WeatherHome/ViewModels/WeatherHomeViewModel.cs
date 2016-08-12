using DevangsWeather.Model;
using DevangsWeather.Providers.wwo;
using DevangsWeather.Service;
using DevangsWeather.WeatherProviderAdapters;
using log4net;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DevangsWeather.Home.ViewModels
{
    public class WeatherHomeViewModel: BindableBase, IConfirmNavigationRequest
    {
        private readonly DelegateCommand<object> showDetailsCommand;
        private readonly DelegateCommand<object> removeCityCommand;
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer container = null;
        private readonly IWeatherService service = null;

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WeatherHomeViewModel(IRegionManager regionManager,IUnityContainer container)
        {
            Log.Debug("Start init WeatherHomeViewModel");
            this.container = container;
            this.regionManager = regionManager;
            service = this.container.Resolve<IWeatherService>();
            this.showDetailsCommand = new DelegateCommand<object>(ShowDetails, CanShowDetails);
            this.removeCityCommand = new DelegateCommand<object>(RemoveCity);
            Log.Debug("Calling Populate Cities");
            Task.Run(()=> PopulateCities());
            Log.Debug("End init WeatherHomeViewModel");
        }

        private void PopulateCities()
        {
            try
            {
                IsLoading = true;
                IList<CurrentWeather> weather = Task.Run(() => service.FindAllCityCurrentWeather()).GetAwaiter().GetResult();
                CityCollection = new ObservableCollection<CurrentWeather>(weather);
                CityListVisibility = CityCollection.Count < 1;
            }
            catch (Exception ex)
            {
                Log.Error("Unable to load cities", ex);
                MessageBox.Show("Unable to load cities");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool cityListVisibility = false;
        public bool CityListVisibility
        {
            get { return cityListVisibility; }
            set
            {
                cityListVisibility = value;
                OnPropertyChanged(() => CityListVisibility);
            }
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
            try
            {
                Task.Run(() => service.RemoveCity(((CurrentWeather)data).CityName)).ContinueWith((x) => { PopulateCities(); });
            }
            catch(Exception ex)
            {
                Log.Error("Unable to remove the city", ex);
                MessageBox.Show("Unable to remove city");
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
            try
            {
                Task.Run(() => PopulateCities());
            }
            catch(Exception ex)
            {
                Log.Error("Failed to populate cities", ex);
                MessageBox.Show("Unable to populate cities");
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           // Not implemented intentionally
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
