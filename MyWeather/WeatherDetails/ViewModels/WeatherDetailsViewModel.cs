using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Service;
using DevangsWeather.Service.WeatherProviderAdapters;
using DevangsWeather.WeatherProviderAdapters;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevangsWeather.Details.ViewModels
{
    public class WeatherDetailsViewModel : BindableBase, IConfirmNavigationRequest
    {
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer unityContainer = null;
        private IRegionNavigationService navigationService;
        private readonly InteractionRequest<Confirmation> confirmExitInteractionRequest;
        private string sendState = "Normal";

        public DelegateCommand<object> GoBackCommand { get; private set; }

        public WeatherDetailsViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            this.regionManager = regionManager;
            this.unityContainer = container;

            SeriesCollection = new SeriesCollection
            {
                

            };

            Labels = new[] {"" };
            

        }
    public SeriesCollection SeriesCollection { get; set; }
    public string[] Labels { get; set; }
    public Func<double, string> YFormatter { get; set; }

    public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            //if (this.sendState == "Normal")
            //{
            //    this.confirmExitInteractionRequest.Raise(
            //        new Confirmation { Content = "", Title = "" },
            //        c => { continuationCallback(c.Confirmed); });
            //}
            //else
            //{

                continuationCallback(true);
            //}
        }

        private CityWeather currentWeather = null;
        public CityWeather CurrentWeather
        {
            get { return currentWeather; }
            set
            {
                currentWeather = value;
                OnPropertyChanged(() => CurrentWeather);
            }
        }
        private CityForcast forecastWeather = null;
        public CityForcast ForecastWeather
        {
            get { return forecastWeather; }
            set
            {
                forecastWeather = value;
                OnPropertyChanged(() => ForecastWeather);
                PopulateGraphData(value);
            }
        }

        private void PopulateGraphData(CityForcast forecast)
        {
            ChartValues<float> minPlot = new ChartValues<float>();
            ChartValues<float> maxPlot = new ChartValues<float>();
            List<String> dates = new List<string>();
            foreach (Weather w in forecast.Forecast)
            {
                minPlot.Add(w.BasicTempreture.TempMin);
                maxPlot.Add(w.BasicTempreture.TempMax);
                dates.Add(w.Date.Date.ToShortDateString());
            }

            SeriesCollection.Clear();

            SeriesCollection.Add(new LineSeries
            {
                Title = "Min Tempreture",
                Values = minPlot
            });

            SeriesCollection.Add(new LineSeries
            {
                Title = "Max Tempreture",
                Values = maxPlot
            });

            Labels = dates.ToArray();
        }

        private ObservableCollection<CityWeather> weatherHistory = new ObservableCollection<CityWeather>();
        public ObservableCollection<CityWeather> WeatherHistory
        {
            get { return weatherHistory; }
            set
            {
                weatherHistory = value;
                OnPropertyChanged(() => WeatherHistory);
            }
        }

        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion("MainContentRegion", typeof(DevangsWeather.Details.Views.WeatherDetails));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
           // throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            navigationService = navigationContext.NavigationService;
            var result = navigationContext.Parameters["City"];

            //Load data.
            
            LoadCityWeather(result.ToString());
            LoadCityForecast(result.ToString());
            LoadCityHistoric(result.ToString());
        }

        private async void LoadCityHistoric(string cityName)
        {
            
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            IList<CityWeather> w = await service.GetWeatherHistory(cityName);
            
            WeatherHistory = new ObservableCollection<CityWeather>(w);
        }

        private async void LoadCityForecast(string cityName)
        {
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
           CityForcast forecast = await service.GetWeatherForNextWeek(cityName,7);
            ForecastWeather = forecast;
        }

        private async void LoadCityWeather(string cityName)
        {
            IOpenWeatherMapApiClient client = new OpenWeatherMapApiClient(new OpenWeatherMapOptions() { ApiKey = "b92f7c085494459336fc2fb33654f2f6" });
            IWeatherProviderAdapter adapter = new OpenWeatherMapAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            CityWeather weather = null;
            IsLoading = true;
            weather = await service.GetTodaysWeather(cityName);
            CurrentWeather = weather;
            CurrentCity = weather.City.CityName;
            IsLoading = false;
        }

        private string currentCity = null;
        public string CurrentCity
        {
            get { return currentCity; }
            set {  currentCity =value;
                OnPropertyChanged(() => CurrentCity); }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged(() => IsLoading);
            }
        }

        private void GoBack(object commandArg)
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private bool CanGoBack(object commandArg)
        {
            return navigationService.Journal.CanGoBack;
        }
    }
}
