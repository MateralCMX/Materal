﻿using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace WinUIDemo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? m_window;
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
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App() => InitializeComponent();
        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
