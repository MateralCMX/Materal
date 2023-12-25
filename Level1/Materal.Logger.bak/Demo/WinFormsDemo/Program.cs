using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormsDemo
{
    public static class Program
    {
        public static IServiceProvider ServiceProvider { get; }

        static Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddMateralLogger();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}