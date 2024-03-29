﻿using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.BaseCore.WebAPI
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
            _titleChangeTtimer.Change(TimeSpan.Zero, TimeSpan.FromMinutes(1));//立即启动，间隔1分钟
            configService?.Invoke(builder.Services);
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