using Microsoft.Practices.Unity;
using Prism.Unity;

using System.Windows;
using System;
using Prism.Modularity;
using DevangsWeather.App.Views;
using DevangsWeather.Home;
using DevangsWeather.Details;
using DevangsWeather.FindCity;
using System.Configuration;

namespace DevangsWeather.App
{
    class Bootstrapper : UnityBootstrapper
    {
        private string apiKeyWWO = null;
        protected override DependencyObject CreateShell()
        {
            apiKeyWWO = ConfigurationManager.AppSettings["apiKeyWWO"];
            Container.RegisterInstance(typeof(String), "apiKey", apiKeyWWO);
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
           
            base.InitializeModules();            
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            Type weatherHomeModule = typeof(WeatherHomeModule);
            this.ModuleCatalog.AddModule(new ModuleInfo(weatherHomeModule.Name,weatherHomeModule.AssemblyQualifiedName));
            Type weatherDetailsModule = typeof(WeatherDetailsModule);
            this.ModuleCatalog.AddModule(new ModuleInfo(weatherDetailsModule.Name, weatherDetailsModule.AssemblyQualifiedName));
            Type weatherFindCityModule = typeof(WeatherFindCityModule);
            this.ModuleCatalog.AddModule(new ModuleInfo(weatherFindCityModule.Name, weatherFindCityModule.AssemblyQualifiedName));
        }
    }
}
