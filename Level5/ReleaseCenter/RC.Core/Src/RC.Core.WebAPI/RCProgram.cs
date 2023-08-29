using Materal.BaseCore.WebAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RC.Core.WebAPI
{
    public class RCProgram : BaseProgram
    {
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="args"></param>
        /// <param name="configService"></param>
        /// <param name="consulTag"></param>
        /// <returns></returns>
        public static WebApplication RCStart(string[] args, Action<IServiceCollection>? configService, string consulTag) => RCStart(args, configService, null, null, consulTag);
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="args"></param>
        /// <param name="configService"></param>
        /// <param name="configAppAction"></param>
        /// <param name="consulTag"></param>
        /// <returns></returns>
        public static WebApplication RCStart(string[] args, Action<IServiceCollection>? configService, Action<WebApplication>? configAppAction, Action<WebApplicationBuilder>? configBuilder, string consulTag)
        {
            WebApplication app = Start(args, config =>
            {
                config.AddJsonFile("RCConfig.json", false, true);
            }, configService, configApp =>
            {
                configAppAction?.Invoke(configApp);
            }, configBuilder, consulTag);
            return app;
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigService(IServiceCollection services) { }
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="app"></param>
        public virtual void ConfigApp(WebApplication app) { }
        /// <summary>
        /// 配置构建器
        /// </summary>
        /// <param name="builder"></param>
        public virtual void ConfigBuilder(WebApplicationBuilder builder) { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public virtual Task InitAsync(string[] args, IServiceProvider services, WebApplication app) => Task.CompletedTask;
    }
}
