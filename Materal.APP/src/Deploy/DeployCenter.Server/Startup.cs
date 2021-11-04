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

namespace DeployCenter.Server
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
                AppName = "DeployCenter",
                SwaggerXmlPathArray = new[]
                {
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
            ConfigureConsulServices(services, ServiceType.DeployCenterServer);
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
            rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            app.UseRewriter(rewriteOptions);
        }
    }
}
