using ConfigCenter.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Hubs.Hubs;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using ConfigCenter.Server.Hubs;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Rewrite;

namespace ConfigCenter.Server
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly WebAPIStartupHelper _webAPIStartupHelper;
        /// <summary>
        /// Startup
        /// </summary>
        public Startup()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            var config = new WebAPIStartupConfig
            {
                AppName = "ConfigCenter",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/ConfigCenter.Server.xml",
                    $"{basePath}/ConfigCenter.PresentationModel.xml",
                    $"{basePath}/ConfigCenter.DataTransmitModel.xml"
                }
            };
            _webAPIStartupHelper = new WebAPIStartupHelper(config);
            _webAPIStartupHelper.OnUseEndpoints += WebAPIStartupHelper_OnUseEndpoints;
        }
        /// <summary>
        /// ≈‰÷√∑˛ŒÒ
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddConfigCenterServerServices();
            _webAPIStartupHelper.AddServices(services);
        }
        /// <summary>
        /// ≈‰÷√
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var rewriteOptions = new RewriteOptions();
            rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            app.UseRewriter(rewriteOptions);
            _webAPIStartupHelper.Configure(app, env);
            var dbContextHelper = ApplicationData.GetService<DBContextHelper<ConfigCenterDBContext>>();
            dbContextHelper.Migrate();
            ApplicationData.GetService<IServerHub>();
        }
        private void WebAPIStartupHelper_OnUseEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
        {
            // ReSharper disable once StringLiteralTypo
            endpointRouteBuilder.MapHub<ConfigCenterHub>("/ConfigCenterHub");
        }
    }
}