using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; }
        static App()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddMateralLogger();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
