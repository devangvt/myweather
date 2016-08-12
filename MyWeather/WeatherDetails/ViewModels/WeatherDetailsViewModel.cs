using DevangsWeather.Model;
using DevangsWeather.OpenWeatherMap;
using DevangsWeather.Providers.wwo;
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

            HistoricCollection = new SeriesCollection
            {


            };

            Labels = new[] {"" };
            HistoricLabels = new[] { "" };


        }
    public SeriesCollection SeriesCollection { get; set; }
    public string[] Labels { get; set; }
        


        public Func<double, string> YFormatter { get; set; }


        public SeriesCollection HistoricCollection { get; set; }
        public string[]  HistoricLabels { get; set; }

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

        private CurrentWeather currentWeather = null;
        public CurrentWeather CurrentWeather
        {
            get { return currentWeather; }
            set
            {
                currentWeather = value;
                OnPropertyChanged(() => CurrentWeather);
            }
        }
        private WeatherForcast forecastWeather = null;
        public WeatherForcast ForecastWeather
        {
            get { return forecastWeather; }
            set
            {
                forecastWeather = value;
                OnPropertyChanged(() => ForecastWeather);
                PopulateForceastGraphData(value);
            }
        }

        private void PopulateForceastGraphData(WeatherForcast forecast)
        {
            ChartValues<float> minPlot = new ChartValues<float>();
            ChartValues<float> maxPlot = new ChartValues<float>();
            List<String> dates = new List<string>();
            foreach (WeatherTemplate w in forecast.Forecast)
            {
                minPlot.Add(float.Parse(w.mintempC));
                maxPlot.Add(float.Parse(w.maxtempC));
                dates.Add(w.date);
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

        private void PopulateHistoryGraphData()
        {
            ChartValues<float> minPlot = new ChartValues<float>();
            ChartValues<float> maxPlot = new ChartValues<float>();
            List<String> dates = new List<string>();
            foreach (WeatherTemplate w in WeatherHistory)
            {
                minPlot.Add(float.Parse(w.mintempC));
                maxPlot.Add(float.Parse(w.maxtempC));
                dates.Add(w.date);
            }

            HistoricCollection.Clear();

            HistoricCollection.Add(new LineSeries
            {
                Title = "Min Tempreture",
                Values = minPlot
            });

            HistoricCollection.Add(new LineSeries
            {
                Title = "Max Tempreture",
                Values = maxPlot
            });

            HistoricLabels = dates.ToArray();
        }

        private ObservableCollection<WeatherTemplate> weatherHistory = new ObservableCollection<WeatherTemplate>();
        public ObservableCollection<WeatherTemplate> WeatherHistory
        {
            get { return weatherHistory; }
            set
            {
                weatherHistory = value;
                PopulateHistoryGraphData();
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

            IWWOClient client = new WWOClient(unityContainer.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            WeatherHistoric w = await service.GetWeatherForLastWeek(cityName);
            
            WeatherHistory = new ObservableCollection<WeatherTemplate>(w.Historic);
        }

        private async void LoadCityForecast(string cityName)
        {
            IWWOClient client = new WWOClient(unityContainer.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
           WeatherForcast forecast = await service.GetWeatherForNextWeek(cityName);
            ForecastWeather = forecast;
        }

        private async void LoadCityWeather(string cityName)
        {
            IWWOClient client = new WWOClient(unityContainer.Resolve<String>("apiKey"));
            IWeatherProviderAdapter adapter = new WWOAdapter(client);
            IWeatherService service = new WeatherService(adapter);
            DevangsWeather.Model.CurrentWeather weather = null;
            IsLoading = true;
            weather = await service.GetCurrentWeather(cityName);
            CurrentWeather = weather;
            CurrentCity = weather.CityName;
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
