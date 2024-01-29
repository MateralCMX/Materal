using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// MergeBlockWeb程序
    /// </summary>
    public class MergeBlockWebProgram : MergeBlockProgram<MergeBlockWebModule, WebModuleInfo, WebConfigServiceContext, WebApplicationContext>
    {
        private WebApplicationBuilder? _builder;
        private WebApplication? _app;
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public override async Task RunAsync(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
            _builder.Host.UseServiceProviderFactory(new WebModuleMateralServiceProviderFactory());//替换服务提供者工厂
            await ConfigModuleAsync(_builder.Services, _builder.Configuration);
            _app = _builder.Build();
            await InitModuleAsync(_app.Services);
            await _app.RunAsync();
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override WebConfigServiceContext GetConfigServiceContext()
        {
            if (_builder is null) throw new MergeBlockException("未初始化WebApplicationBuilder");
            return new(_builder);
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected override WebApplicationContext GetApplicationContext()
        {
            if (_app is null) throw new MergeBlockException("未初始化WebApplication");
            return new(_app.Services);
        }
    }
}
