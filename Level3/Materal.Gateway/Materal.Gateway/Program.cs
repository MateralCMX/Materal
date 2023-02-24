using Materal.Abstractions;
using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension;
using Materal.Logger;
using Microsoft.AspNetCore.Components.Authorization;

namespace Materal.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot")
            });
            #region 加载配置文件
            builder.Configuration
                .AddJsonFile("MateralLogger.json", false, true) //加载MateralLogger配置
                .AddJsonFile("Ocelot.json", false, true); //加载Ocelot配置
            #endregion
            #region DI
            IServiceCollection services = builder.Services;
            #region 日志
            services.AddMateralLogger();
            #endregion
            #region Blazor
            services.AddAntDesign();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<CustomAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(factory => factory.GetRequiredService<CustomAuthenticationStateProvider>());
            #endregion
            #region Swagger
            services.AddSwaggerForOcelot(builder.Configuration);
            #endregion
            #region 网关
            services.AddOcelotGatewayAsync();
            #endregion
            services.AddEndpointsApiExplorer();
            #endregion
            builder.Host.UseDefaultServiceProvider(configure =>
            {
                configure.ValidateScopes = false;
            });
            WebApplication app = builder.Build();
            #region WebApplication
            MateralServices.Services = app.Services;
            ApplicationConfig.Configuration = builder.Configuration;
            app.UseMateralLogger(null, ApplicationConfig.Configuration);
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
            await app.UseOcelotGateway();
            #endregion
            await app.RunAsync();
        }
    }
}