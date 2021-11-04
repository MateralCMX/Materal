using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.IntegrationEventHandlers;
using ConfigCenter.Environment.SqliteEFRepository;
using ConfigCenter.IntegrationEvents;
using Materal.APP.Core;
using Materal.APP.Core.ConfigModels;
using Materal.APP.WebAPICore;
using Materal.TFMS.EventBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.Server
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
                AppName = "ConfigCenter.Environment",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/ConfigCenter.Environment.Server.xml",
                    $"{basePath}/ConfigCenter.Environment.PresentationModel.xml",
                    $"{basePath}/ConfigCenter.Environment.DataTransmitModel.xml"
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
            ConfigureConsulServices(services, ServiceType.ConfigCenterEnvironment);
            services.AddConfigCenterEnvironmentServerServices();
            ConfigureTFMSServices(services, ConfigCenterEnvironmentConfig.TFMSConfig);
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
            DBContextHelper<ConfigCenterEnvironmentDBContext> dbContextHelper = ApplicationConfig.GetService<DBContextHelper<ConfigCenterEnvironmentDBContext>>();
            dbContextHelper.Migrate();
            IEventBus eventBus = app.ApplicationServices.GetService<IEventBus>();
            if (eventBus != null)
            {
                Task.Run(async () =>
                {
                    await eventBus.SubscribeAsync<DeleteNamespaceEvent, DeleteNamespaceEventHandler>();
                    await eventBus.SubscribeAsync<DeleteProjectEvent, DeleteProjectEventHandler>();
                    await eventBus.SubscribeAsync<EditNamespaceEvent, EditNamespaceEventHandler>();
                    await eventBus.SubscribeAsync<EditProjectEvent, EditProjectEventHandler>();
                    await eventBus.SubscribeAsync<SyncConfigurationItemEvent, SyncConfigurationItemEventHandler>();
                    eventBus.StartListening();
                });
            }
        }
        /// <summary>
        /// 配置Consul
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceType"></param>
        protected override void ConfigureConsulServices(IServiceCollection services, ServiceType serviceType)
        {
            ConfigCenterEnvironmentConfig.ServiceName = $"{ConfigCenterEnvironmentConfig.EnvironmentConfig.Key}EnvAPI";
            ConsulManage.Init(serviceType, ConfigCenterEnvironmentConfig.ServiceName, ConfigCenterEnvironmentConfig.EnvironmentConfig.Name);
            ConsulManage.RegisterConsul();
        }
    }
}
