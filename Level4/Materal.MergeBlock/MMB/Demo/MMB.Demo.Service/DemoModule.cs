using Microsoft.Extensions.DependencyInjection;
using MMB.Core.Abstractions;
using MMB.Demo.Abstractions;
using MMB.Demo.Service.Services;

namespace MMB.Demo.Service
{
    /// <summary>
    /// Demo模块
    /// </summary>
    public class DemoModule : MMBModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoModule() : base("Demo") { }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            await base.OnConfigServiceAsync(context);
        }
    }
}
