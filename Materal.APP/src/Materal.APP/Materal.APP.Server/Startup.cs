using Materal.APP.Server.Hubs;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.APP.Server
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
                AppName = "MateralAPP",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/Materal.APP.Server.xml",
                    $"{basePath}/Materal.APP.DataTransmitModel.xml",
                    $"{basePath}/Materal.APP.PresentationModel.xml"
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
            services.AddMateralAPPServerServices();
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
        }

        private void WebAPIStartupHelper_OnUseEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapHub<ServerHub>("/ServerHub");
        }
    }
}
