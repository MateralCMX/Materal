using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.SqliteEFRepository;
using ConfigCenter.Hubs.Hubs;
using ConfigCenter.PresentationModel.ConfigCenter;
using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.Enums;
using Materal.APP.HttpManage;
using Materal.APP.WebAPICore;
using Materal.Common;
using Materal.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.Server
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
                AppName = "ConfigCenterEnvironment",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/ConfigCenter.Environment.Server.xml",
                    $"{basePath}/ConfigCenter.Environment.PresentationModel.xml",
                    $"{basePath}/ConfigCenter.Environment.DataTransmitModel.xml"
                }
            };
            _webAPIStartupHelper = new WebAPIStartupHelper(config);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfigCenterEnvironmentServerServices();
            _webAPIStartupHelper.AddServices(services);
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var rewriteOptions = new RewriteOptions();
            rewriteOptions.Add(new RedirectHomeIndexRequests("/swagger/index.html"));
            app.UseRewriter(rewriteOptions);
            _webAPIStartupHelper.Configure(app, env);
            var dbContextHelper = ApplicationData.GetService<DBContextHelper<ConfigCenterEnvironmentDBContext>>();
            dbContextHelper.Migrate();
            Task task = Task.Run(async () =>
            {
                await ConnectConfigCenterAsync();
            });
            Task.WaitAll(task);
        }
        /// <summary>
        /// 连接配置中心
        /// </summary>
        /// <returns></returns>
        private async Task ConnectConfigCenterAsync()
        {
            try
            {
                var serverManage = ApplicationData.GetService<IServerManage>();
                ResultModel<List<ServerListDTO>> resultModel = await serverManage.GetServerListAsync();
                if (resultModel.ResultType == ResultTypeEnum.Success)
                {
                    ServerListDTO server = resultModel.Data.FirstOrDefault(m => m.ServerCategory == ServerCategoryEnum.ConfigCenter);
                    if(server == null) throw new ConfigCenterEnvironmentException("获取配置中心服务失败");
                    ConfigCenterEnvironmentConfig.ConfigCenterUrl = server.Url;
                    ConfigCenterEnvironmentConsoleHelper.WriteLine($"已获取配置中心地址:{ConfigCenterEnvironmentConfig.ConfigCenterUrl}");
                    var configCenterHub = ApplicationData.GetService<IConfigCenterHub>();
                    var registerModel = new RegisterEnvironmentRequestModel
                    {
                        Name = ConfigCenterEnvironmentConfig.ServerInfo.Name,
                        Url = ApplicationConfig.PublicUrl,
                        Key = ConfigCenterEnvironmentConfig.ServerInfo.Key
                    };
                    await configCenterHub.RegisterEnvironment(registerModel);
                }
                else
                {
                    throw new ConfigCenterEnvironmentException("获取配置中心服务失败");
                }
            }
            catch (Exception exception)
            {
                throw new ConfigCenterEnvironmentException("获取配置中心服务失败", exception);
            }
        }
    }
}