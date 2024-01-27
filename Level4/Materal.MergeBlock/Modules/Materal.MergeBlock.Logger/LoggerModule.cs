﻿using Materal.Logger.ConfigModels;
using Materal.Logger.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Materal.MergeBlock.Logger
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggerModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerModule() : base("日志模块", "Logger")
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = context.ServiceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            context.Services.AddMateralLogger(context.Configuration, config => config.AddCustomConfig(nameof(MergeBlockConfig.ApplicationName), mergeBlockConfig.CurrentValue.ApplicationName));
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            await context.ServiceProvider.UseMateralLoggerAsync();
            ILoggerFactory? loggerFactory = context.ServiceProvider.GetService<ILoggerFactory>();
            if (loggerFactory is not null)
            {
                MergeBlockHost.Logger = loggerFactory.CreateLogger("MergeBlock");
            }
        }
    }
}
