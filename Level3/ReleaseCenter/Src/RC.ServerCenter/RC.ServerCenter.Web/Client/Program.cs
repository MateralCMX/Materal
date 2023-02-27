using Blazored.LocalStorage;
using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RC.Core.HttpClient;
using System.Reflection;

namespace RC.ServerCenter.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            MateralCoreConfig.Configuration = builder.Configuration;
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            IServiceCollection services = builder.Services;
            //builder.Services.AddScoped(sp => new System.Net.Http.HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            services.AddBlazoredLocalStorage();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(factory => factory.GetRequiredService<CustomAuthenticationStateProvider>());
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddMateralUtils();
            services.AddAntDesign();
            services.AddHttpClientService(WebAppConfig.AppName, Assembly.Load("RC.Authority.HttpClient"), Assembly.Load("RC.EnvironmentServer.HttpClient"));
            WebAssemblyHost app = builder.Build();
            MateralServices.Services = app.Services;
            ConfigHttpClient();
            await app.RunAsync();
        }
        private static void ConfigHttpClient()
        {
            HttpClientHelper.GetUrl = (url, appName) =>
            {
                return $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}RC{appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/api/{url}";
            };
            HttpClientHelper.CloseAutoToken();
            HttpClientHelper.GetToken = () =>
            {
                CustomAuthenticationStateProvider authenticationState = MateralServices.GetService<CustomAuthenticationStateProvider>();
                string? token = authenticationState.GetToken();
                return token;
            };
        }
    }
}