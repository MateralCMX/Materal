﻿using Materal.MergeBlock.Abstractions.ConsoleModule;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.ConsoleModule
{
    /// <summary>
    /// MergeBlock控制台程序
    /// </summary>
    public class MergeBlockConsoleProgram : MergeBlockProgram<IMergeBlockConsoleModule, ConsoleModuleInfo, IConsoleConfigServiceContext, IConsoleApplicationContext>
    {
        private HostApplicationBuilder? _builder;
        private IHost? _app;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        public override async Task RunAsync(string[] args, bool autoRemoveAssemblies = true)
        {
            _builder = Host.CreateApplicationBuilder(args);
            _builder.UseMateralServiceProvider();//替换服务提供者工厂
            await ConfigModuleAsync(_builder.Services, _builder.Configuration, autoRemoveAssemblies);
            _app = _builder.Build();
            IConsoleApplicationContext context = await InitModuleAsync(_app.Services);
            await _app.RunAsync();
            await CloseModuleAsync(context);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override ConsoleConfigServiceContext GetConfigServiceContext()
        {
            if (_builder is null) throw new MergeBlockException("未初始化HostBuilder");
            return new(_builder);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override ConsoleApplicationContext GetApplicationContext()
        {
            if (_app is null) throw new MergeBlockException("未初始化Host");
            return new(_app.Services);
        }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override async Task ConfigServiceBeforeAsync(IConsoleConfigServiceContext context)
        {
            context.Services.AddHostedService<MergeBlockHostedService>();
            await base.ConfigServiceBeforeAsync(context);
        }
    }
}
