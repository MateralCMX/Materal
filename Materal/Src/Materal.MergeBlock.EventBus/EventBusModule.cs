using Materal.EventBus.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.EventBus
{
    /// <summary>
    /// 事件总线模块
    /// </summary>
    public class EventBusModule() : MergeBlockModule("事件总线模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is null) throw new MergeBlockException("未找到配置对象");
            IConfigurationSection section = context.Configuration.GetSection("EventBus");
            MergeBlockContext? mergeBlockContext = context.Services.GetSingletonInstance<MergeBlockContext>();
            if (mergeBlockContext is null) return;
            context.Services.AddRabbitMQEventBus(section, [.. mergeBlockContext.MergeBlockAssemblies]);
        }
        /// <summary>
        /// 应用初始化后
        /// </summary>
        /// <param name="context"></param>
        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            context.ServiceProvider.GetRequiredService<IEventBus>();
        }
    }
}
