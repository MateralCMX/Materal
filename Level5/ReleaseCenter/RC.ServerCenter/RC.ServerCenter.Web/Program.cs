using Blazored.LocalStorage;
using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.HttpClient;
using RC.Authority.PresentationModel.User;
using RC.Core.HttpClient;
using System.Net;
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
            services.AddBlazoredLocalStorage();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(factory => factory.GetRequiredService<CustomAuthenticationStateProvider>());
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddMateralUtils();
            services.AddAntDesign();
            System.Net.Http.HttpClient httpClient = new()
            {
                DefaultRequestVersion = HttpVersion.Version20
            };
            services.AddSingleton(httpClient);
            services.AddHttpClientService(WebAppConfig.AppName, Assembly.Load("RC.ServerCenter.HttpClient"), Assembly.Load("RC.Authority.HttpClient"), Assembly.Load("RC.EnvironmentServer.HttpClient"), Assembly.Load("RC.Deploy.HttpClient"));
            WebAssemblyHost app = builder.Build();
            MateralServices.Services = app.Services;
            ConfigHttpClient();
            await app.RunAsync();
        }
        private static void ConfigHttpClient()
        {
            HttpClientHelper.GetUrl = (url, appName) =>
            {
                return appName switch
                {
                    "Deploy" => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}{RCData.SelectedDeploy}/api/{url}",
                    "EnvironmentServer" => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}{RCData.SelectedEnvironment}/api/{url}",
                    _ => $"{HttpClientConfig.HttpClienUrltConfig.BaseUrl}RC{appName}{HttpClientConfig.HttpClienUrltConfig.Suffix}/api/{url}",
                };
            };
            HttpClientHelper.CloseAutoToken();
            HttpClientHelper.GetToken = () =>
            {
                CustomAuthenticationStateProvider authenticationState = MateralServices.GetRequiredService<CustomAuthenticationStateProvider>();
                string? token = authenticationState.GetToken();
                return token;
            };
        }
    }
}