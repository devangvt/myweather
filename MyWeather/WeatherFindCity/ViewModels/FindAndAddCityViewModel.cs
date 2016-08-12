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
using log4net;
using System.Windows;

namespace DevangsWeather.FindCity.ViewModels
{
    public class FindAndAddCityViewModel : BindableBase, IConfirmNavigationRequest
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer unityContainer = null;
        private readonly DelegateCommand<object> searchCityCommand;
        private readonly DelegateCommand<object> addCityCommand;

        private readonly IWeatherService service = null;
        public FindAndAddCityViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            Log.Debug("Start Init FindAndAddCityViewModel");
            this.regionManager = regionManager;
            this.unityContainer = container;
            this.searchCityCommand = new DelegateCommand<object>(SearchCity, CanSearchCity);
            this.addCityCommand = new DelegateCommand<object>(AddCity, CanAddCity);
            this.service = container.Resolve<IWeatherService>();
            Log.Debug("End Init FindAndAddCityViewModel");
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
            if (obj != null)
            {
                
                try
                {
                    // Add city and navigate   
                    service.AddCity(Result);
                    this.regionManager.RequestNavigate("MainContentRegion", "WeatherHome");
                }
                catch(Exception ex)
                {
                    Log.Error("Unable to addCity and navigate to WeatherHome", ex);
                    MessageBox.Show("Failed to Add City");
                }
            }
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
                if(value != null)
                {
                    SearchSuccess = true;
                }
                OnPropertyChanged(() => Result);
            }
        }

        private bool searchSuccess = false;
        public bool SearchSuccess
        {
            get { return searchSuccess; }
            set
            {
                searchSuccess = value;
                OnPropertyChanged(() => SearchSuccess);
            }
        }

        private void SearchCity(object data)
        {
            if (!string.IsNullOrEmpty(data.ToString()))
            {
           
                City city = null;
                try
                {
                    city = Task.Run(() => service.GetCityByName(data.ToString())).GetAwaiter().GetResult();
                }
                catch(Exception ex)
                {
                    Log.Error("Unable to search city", ex);
                    MessageBox.Show("Unable to search city");
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

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //Clear old result;
            Result = null;
            SearchSuccess = false;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //Do nothing left intentionally.
        }
    }
}
