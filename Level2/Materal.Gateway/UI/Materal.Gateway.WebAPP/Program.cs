using Materal.Abstractions;
using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension;
using Materal.Logger;
using Microsoft.AspNetCore.Components.Authorization;

namespace Materal.Gateway.WebAPP
{
    /// <summary>
    /// Ӧ�ó���������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot")
            });
            ApplicationConfig.Configuration = builder.Configuration;
            #region ���������ļ�
            builder.Configuration
                .AddJsonFile("Ocelot.json", false, true); //����Ocelot����
            #endregion
            #region DI
            IServiceCollection services = builder.Services;
            services.AddMateralLogger(builder.Configuration);
            #region Blazor
            services.AddAntDesign();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(factory => factory.GetRequiredService<CustomAuthenticationStateProvider>());
            #endregion
            services.AddSwaggerForOcelot(builder.Configuration);
            services.AddOcelotGateway();
            services.AddEndpointsApiExplorer();
            #endregion
            WebApplication app = builder.Build();
            MateralServices.Services = app.Services;
            #region WebApplication
            await app.UseOcelotGatewayAsync(true);
            builder.Host.UseDefaultServiceProvider(configure =>
            {
                configure.ValidateScopes = false;
            });
            app.Map("/admin", application =>
            {
                application.UseStaticFiles();
                application.UseRouting();
                application.UseAuthorization();
                application.UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
            });
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            #endregion
            await app.RunAsync();
        }
    }
}