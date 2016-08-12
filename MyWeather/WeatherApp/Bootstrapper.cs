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
using DevangsWeather.Providers.wwo;
using DevangsWeather.WeatherProviderAdapters;
using DevangsWeather.Service;
using log4net;

namespace DevangsWeather.App
{
    class Bootstrapper : UnityBootstrapper
    {
        private string apiKeyWWO = null;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DependencyObject CreateShell()
        {

            //Configure Log4Net
            log4net.Config.XmlConfigurator.Configure();
            //Get the apikey for using with WWO.
            apiKeyWWO = ConfigurationManager.AppSettings["apiKeyWWO"];

            //Register Weather service with Container
            Container.RegisterInstance(typeof(String), "apiKey", apiKeyWWO);
            Log.Debug("Registered api key in Container");
            
            IWWOClient client = new WWOClient(apiKeyWWO);            
            IWeatherProviderAdapter adapter = new WWOAdapter(client);            
            IWeatherService service = new WeatherService(adapter);
            Container.RegisterInstance(typeof(IWeatherService), service);
            Log.Debug("Registered weather service in Container");
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
