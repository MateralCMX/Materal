using System;
using System.IO;
using Materal.Gateway.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Config;
using NLog.Web;

namespace Materal.Gateway.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            ApplicationConfig.Init(configuration);
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureNLogServices();
            Config config = new Config();
            services.AddIdentityServer()
                .AddInMemoryApiResources(config.ApiResources)
                .AddInMemoryClients(config.Clients)
                .AddInMemoryApiScopes(config.ApiScopes)
                .AddDeveloperSigningCredential();
            ConfigureCorsServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            ConfigureCors(app);
        }
        #region 私有方法
        #region NLog
        /// <summary>
        /// 配置NLog服务
        /// </summary>
        private void ConfigureNLogServices()
        {
            string nLogConfigPatch = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NLog.config");
            NLogBuilder.ConfigureNLog(nLogConfigPatch).GetCurrentClassLogger();
            InstallationContext nLogInstallationContext = new InstallationContext();
            LogManager.Configuration.Install(nLogInstallationContext);
            LogManager.Configuration.Variables["MaxLogFileSaveDays"] = ApplicationConfig.NLogConfig.MaxLogFileSaveDays;
        }
        #endregion
        #region Cors
        /// <summary>
        /// 配置Cors服务
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        }
        /// <summary>
        /// 配置Cors
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
        }
        #endregion
        #endregion
    }
}
