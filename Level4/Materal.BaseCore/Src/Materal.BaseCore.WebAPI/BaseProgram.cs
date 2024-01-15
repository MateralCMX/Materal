using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Materal.BaseCore.WebAPI
{
    /// <summary>
    /// 基础主程序
    /// </summary>
    public class BaseProgram
    {
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="args"></param>
        /// <param name="initConfig">初始化配置</param>
        /// <param name="configService"></param>
        /// <param name="configApp"></param>
        /// <param name="consulTag"></param>
        /// <returns></returns>
        protected static WebApplication Start(string[] args, Action<ConfigurationManager> initConfig, Action<IServiceCollection>? configService, Action<WebApplication>? configApp, Action<WebApplicationBuilder>? configBuilder, string consulTag)
        {
            WebApplicationOptions applicationOptions = new()
            {
                Args = args,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory
            };
            return Start(applicationOptions, initConfig, configService, configApp, configBuilder, consulTag);
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="applicationOptions"></param>
        /// <param name="initConfig">初始化配置</param>
        /// <param name="configService"></param>
        /// <param name="configApp"></param>
        /// <param name="configBuilder"></param>
        /// <param name="consulTag"></param>
        /// <returns></returns>
        protected static WebApplication Start(WebApplicationOptions applicationOptions, Action<ConfigurationManager> initConfig, Action<IServiceCollection>? configService, Action<WebApplication>? configApp, Action<WebApplicationBuilder>? configBuilder, string consulTag)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            WebApplicationBuilder builder = WebApplication.CreateBuilder(applicationOptions);
            configBuilder?.Invoke(builder);
            initConfig?.Invoke(builder.Configuration);
            MateralCoreConfig.Configuration = builder.Configuration;
            configService?.Invoke(builder.Services);
            builder.Host.UseServiceProviderFactory(new MateralServiceProviderFactory());//使用AOP
            WebApplication app = builder.Build();
            configApp?.Invoke(app);
            app.WebApplicationConfig(consulTag);
            return app;
        }
        /// <summary>
        /// 程序退出时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (WebAPIConfig.ConsulConfig.Enable)
            {
                ConsulManager.UnregisterConsul();
            }
        }
    }
}