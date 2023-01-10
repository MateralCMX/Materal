using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RC.Core.Common;
using RC.Core.WebAPI.Common;

namespace RC.Core.WebAPI
{
    /// <summary>
    /// 基础主程序
    /// </summary>
    public class BaseProgram
    {
        private readonly static Timer _titleChangeTtimer = new(TitleTimerRun);
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="args"></param>
        /// <param name="configService"></param>
        /// <param name="configApp"></param>
        /// <param name="consulTag"></param>
        /// <returns></returns>
        protected static WebApplication Start(string[] args, Action<IServiceCollection>? configService, Action<WebApplication>? configApp, string consulTag)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory,
                WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Application")
            });
            builder.Configuration.AddJsonFile("RCConfig.json", false, true);
            RCConfig.Configuration = builder.Configuration;
            _titleChangeTtimer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));//立即启动，间隔1分钟
            configService?.Invoke(builder.Services);
            builder.Host.UseDefaultServiceProvider(configure =>
            {
                configure.ValidateScopes = false;
            });
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
        /// <summary>
        /// 标题计时器运行
        /// </summary>
        /// <param name="state"></param>
        private static void TitleTimerRun(object? state)
        {
            Console.Title = WebAPIConfig.AppTitle;
        }
    }
}