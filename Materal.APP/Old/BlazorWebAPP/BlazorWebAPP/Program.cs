using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorWebAPP.Common;
using Materal.APP.Common;

namespace BlazorWebAPP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webAssemblyHostBuilder = WebAssemblyHostBuilder.CreateDefault(args);
            ApplicationConfig.SetConfiguration(webAssemblyHostBuilder.Configuration);
            BlazorWebAPPConfig.SetConfiguration(webAssemblyHostBuilder.Configuration);
            webAssemblyHostBuilder.RootComponents.Add<App>("app");
            webAssemblyHostBuilder.Services.AddBlazorWebAPPServices();
            webAssemblyHostBuilder.Services.AddScoped(serviceProvider => new HttpClient { BaseAddress = new Uri(webAssemblyHostBuilder.HostEnvironment.BaseAddress) });
            WebAssemblyHost webAssemblyHost = webAssemblyHostBuilder.Build();
            ApplicationService.ServiceProvider = webAssemblyHost.Services;
            await webAssemblyHost.RunAsync();
        }
    }
}
