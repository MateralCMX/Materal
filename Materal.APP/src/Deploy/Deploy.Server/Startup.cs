using Deploy.Common;
using Deploy.SqliteEFRepository;
using Materal.APP.Core;
using Materal.APP.Core.ConfigModels;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using Deploy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace Deploy.Server
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup : MateralAPPWebAPIStartup
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            WebAPIStartupConfig config = new WebAPIStartupConfig
            {
                AppName = "Deploy",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/Deploy.Server.xml",
                    $"{basePath}/Deploy.PresentationModel.xml",
                    $"{basePath}/Deploy.DataTransmitModel.xml"
                }
            };
            Init(config);
        }
        /// <summary>
        /// 配置服务容器
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            ConfigureConsulServices(services, ServiceType.DeployServer);
            services.AddDeployServerServices();
            services.Configure<FormOptions>(config =>
            {
                config.MultipartBodyLengthLimit = long.MaxValue;
            });
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="en"></param>
        /// <param name="lifetime"></param>
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment en, IHostApplicationLifetime lifetime)
        {
            base.Configure(app, en, lifetime);
            RewriteOptions rewriteOptions = new RewriteOptions();
            if (DeployConfig.RewriteConfig.Enable)
            {
                rewriteOptions.Add(new RewriteHomeIndexRequests(DeployConfig.RewriteConfig.Address));
            }
            else if (ApplicationConfig.EnableSwagger)
            {
                rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            }
            if(rewriteOptions.Rules.Count > 0)
            {
                app.UseRewriter(rewriteOptions);
            }
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(basePath, "Application");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            app.Use(async (httpContext, next) =>
            {
                if (httpContext.Request.Path.HasValue && !string.IsNullOrWhiteSpace(httpContext.Request.Path.Value))
                {
                    if (DeployConfig.ApplicationNameWhiteList.Any(item => httpContext.Request.Path.Value.StartsWith($"/{item}")))
                    {
                        await next();
                    }
                    else
                    {
                        var applicationInfoService = ApplicationConfig.GetService<IApplicationInfoService>();
                        string[] paths = httpContext.Request.Path.Value.Split("/");
                        if (paths.Length >= 2 && applicationInfoService.IsRunningApplication(paths[1]))
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

            DBContextHelper<DeployDBContext> dbContextHelper = ApplicationConfig.GetService<DBContextHelper<DeployDBContext>>();
            dbContextHelper.Migrate();
        }
        /// <summary>
        /// 配置Consul
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceType"></param>
        protected override void ConfigureConsulServices(IServiceCollection services, ServiceType serviceType)
        {
            DeployConfig.ServiceName = $"{DeployConfig.DeployServerConfig.Key}DepAPI";
            ConsulManage.Init(serviceType, DeployConfig.ServiceName, DeployConfig.DeployServerConfig.Name);
            ConsulManage.RegisterConsul();
        }
    }
}
