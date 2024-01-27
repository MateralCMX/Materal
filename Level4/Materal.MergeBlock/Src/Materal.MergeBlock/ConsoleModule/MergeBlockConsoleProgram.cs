using Materal.MergeBlock.Abstractions.ConsoleModule;
using Materal.MergeBlock.NormalModule;
using Materal.MergeBlock.WebModule;
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
        /// <returns></returns>
        public override async Task RunAsync(string[] args)
        {
            _builder = Host.CreateApplicationBuilder(args);
            _builder.Services.AddSingleton<IModuleBuilder, ConsoleModuleBuilder>();
            _builder.Services.AddSingleton<IModuleBuilder, WebModuleBuilder>();
            _builder.Services.AddSingleton<IModuleBuilder, NormalModuleBuilder>();
            await ConfigModuleAsync(_builder.Services, _builder.Configuration);
            _app = _builder.Build();
            await InitModuleAsync();
            await _app.RunAsync();
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
    }
}
