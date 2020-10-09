using Deploy.Services;
using Deploy.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Hubs.Hubs;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Linq;
using Deploy.Common;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http.Features;

namespace Deploy.Server
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
                AppName = "Deploy",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/Deploy.Server.xml",
                    $"{basePath}/Deploy.PresentationModel.xml",
                    $"{basePath}/Deploy.DataTransmitModel.xml"
                }
            };
            _webAPIStartupHelper = new WebAPIStartupHelper(config);
        }
        /// <summary>
        /// ≈‰÷√∑˛ŒÒ
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDeployServerServices();
            services.Configure<FormOptions>(config =>
            {
                config.MultipartBodyLengthLimit = long.MaxValue;
            });
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
            rewriteOptions.Add(new RewriteHomeIndexRequests("/Portal/index.html"));
            app.UseRewriter(rewriteOptions);
            string basePath = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.Use(async (httpContext, next) =>
            {
                if (httpContext.Request.Path.HasValue)
                {
                    if (DeployConfig.ApplicationNameWhiteList.Any(item => httpContext.Request.Path.Value.StartsWith($"/{item}")))
                    {
                        await next();
                    }
                    else
                    {
                        var applicationInfoService = ApplicationData.GetService<IApplicationInfoService>();
                        string[] paths = httpContext.Request.Path.Value.Split("/");
                        if (paths.Length >= 2 && applicationInfoService.IsRuningApplication(paths[1]))
                        {
                            await next();
                        }
                        else
                        {
                            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        }
                    }
                }
            });

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".json"] = "application/json";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                },
                RequestPath = "",
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/octet-stream"
            });
            _webAPIStartupHelper.Configure(app, env);
            var dbContextHelper = ApplicationData.GetService<DBContextHelper<DeployDBContext>>();
            dbContextHelper.Migrate();
            ApplicationData.GetService<IServerHub>();
        }
    }
}