using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using System;
using System.Windows;

namespace DevangsWeather.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Devangs Weather App";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private readonly DelegateCommand<object> addCityCommand;
        private readonly DelegateCommand<object> goHomeCommand;
        private readonly DelegateCommand<object> doubleClickCommand;
        private readonly IRegionManager regionManager = null;
        private static bool hide = true;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.addCityCommand = new DelegateCommand<object>(AddCity);
            this.goHomeCommand = new DelegateCommand<object>(GoHome);
            this.doubleClickCommand = new DelegateCommand<object>(DoubleClickHandle);
        }


        private void AddCity(object data)        {
           
            this.regionManager.RequestNavigate("MainContentRegion", "FindAndAddCity");          

        }

        public ICommand AddCityCommand
        {
            get { return this.addCityCommand; }
        }

        public ICommand DoubleClickCommand
        {
            get { return this.doubleClickCommand; }
        }

        private void DoubleClickHandle(object data)
        {
            hide = !hide;
            if (hide)
            {
                Application.Current.MainWindow.Show();
            }
            else
            {
                Application.Current.MainWindow.Hide();
            }

        }

        private void GoHome(object data)
        {
            this.regionManager.RequestNavigate("MainContentRegion", "WeatherHome");

        }

        public ICommand GoHomeCommand
        {
            get { return this.goHomeCommand; }
        }
        
       
    }
}
