using Materal.APP.Server.Hubs;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

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
            rewriteOptions.Add(new RewriteHomeIndexRequests("/index.html"));
            app.UseRewriter(rewriteOptions);
            string basePath = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            string path = Path.Combine(basePath, "Application/wwwroot");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "",
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });
            _webAPIStartupHelper.Configure(app, env);
        }

        private void WebAPIStartupHelper_OnUseEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
        {
            // ReSharper disable once StringLiteralTypo
            endpointRouteBuilder.MapHub<ServerHub>("/ServerHub");
        }
    }
}
