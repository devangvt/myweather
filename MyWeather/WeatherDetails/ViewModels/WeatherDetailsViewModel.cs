using DevangsWeather.Model;
using DevangsWeather.Providers.wwo;
using DevangsWeather.Service;
using DevangsWeather.WeatherProviderAdapters;
using LiveCharts;
using LiveCharts.Wpf;
using log4net;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevangsWeather.Details.ViewModels
{
    public class WeatherDetailsViewModel : BindableBase, IConfirmNavigationRequest
    {
        private readonly IRegionManager regionManager = null;
        private readonly IUnityContainer unityContainer = null;
        private IRegionNavigationService navigationService;
        private readonly IWeatherService service = null;
        //public DelegateCommand<object> GoBackCommand { get; private set; }
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WeatherDetailsViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            Log.Debug("Start init WeatherDetailsViewModel");
            this.regionManager = regionManager;
            this.unityContainer = container;
            service = container.Resolve<IWeatherService>();
            SeriesCollection = new SeriesCollection
            {


            };

            HistoricCollection = new SeriesCollection
            {


            };

            Labels = new[] { "" };
            HistoricLabels = new[] { "" };

            Log.Debug("End init WeatherDetailsViewModel");
        }

        //Series collection for Forecast
        public SeriesCollection SeriesCollection { get; set; }
        //Label for Forecast
        public string[] Labels { get; set; }
        // Y axis formatter
        public Func<double, string> YFormatter { get; set; }

        //Series collection for History
        public SeriesCollection HistoricCollection { get; set; }
        //Label for History
        public string[] HistoricLabels { get; set; }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
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
            Log.Debug("Start Populating Graph for Forecast");
            try
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
                Log.Debug("End Populating Graph for Forecast");
            }
            catch (Exception ex)
            {
                Log.Error("Unable to populate forecast graph data", ex);
            }
        }

        private void PopulateHistoryGraphData()
        {
            try
            {
                Log.Debug("Start Populating Graph for History");
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
                Log.Debug("End Populating Graph for History");
            }
            catch(Exception ex)
            {
                Log.Error("Unable to populate History graph data", ex);
            }
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
            this.regionManager.RegisterViewWithRegion("MainContentRegion", typeof(Views.WeatherDetails));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;

        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //Do nothing
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Log.Debug("NavigatedTo called for WeatherDetailsViewModel");
                
            navigationService = navigationContext.NavigationService;
            var result = navigationContext.Parameters["City"];
            Log.Debug("Navigation Parameters " + result.ToString());
            //Load data.
            Log.Debug("Start Loading Weather");
            LoadCityWeather(result.ToString());
            LoadCityForecast(result.ToString());
            LoadCityHistoric(result.ToString());
            Log.Debug("End Loading Weather");
        }

        private async void LoadCityHistoric(string cityName)
        {
            try
            {
                IsLoadingHistory = true;
                WeatherHistoric w = await service.GetWeatherForLastWeek(cityName);

                WeatherHistory = new ObservableCollection<WeatherTemplate>(w.Historic);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to load historic weather",ex);
            }
            finally
            {
                IsLoadingHistory = false;
            }
        }

        private async void LoadCityForecast(string cityName)
        {
            try
            {
                IsLoadingForecast = true;
                WeatherForcast forecast = await service.GetWeatherForNextWeek(cityName);
                ForecastWeather = forecast;
            }
            catch (Exception ex)
            {
                Log.Error("Failed Loading Forecast Weather", ex);
            }
            finally
            {
                IsLoadingForecast = false;
            }
        }

        private async void LoadCityWeather(string cityName)
        {
            try
            {
                CurrentWeather weather = null;
                IsLoading = true;
                weather = await service.GetCurrentWeather(cityName);
                CurrentWeather = weather;
                CurrentCity = weather.CityName;
            }
            catch (Exception ex)
            {
                Log.Error("Failed Loading Current Weather",ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private string currentCity = null;
        public string CurrentCity
        {
            get { return currentCity; }
            set
            {
                currentCity = value;
                OnPropertyChanged(() => CurrentCity);
            }
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

        private bool isLoadingHistory = false;
        public bool IsLoadingHistory
        {
            get
            {
                return isLoadingHistory;
            }
            set
            {
                isLoadingHistory = value;
                OnPropertyChanged(() => IsLoadingHistory);
            }
        }

        private bool isLoadingForecast = false;
        public bool IsLoadingForecast
        {
            get
            {
                return isLoadingForecast;
            }
            set
            {
                isLoadingForecast = value;
                OnPropertyChanged(() => IsLoadingForecast);
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
