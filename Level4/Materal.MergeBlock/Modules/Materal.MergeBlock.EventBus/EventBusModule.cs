﻿using Materal.EventBus.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.MergeBlock.EventBus
{
    /// <summary>
    /// 事件总线模块
    /// </summary>
    public class EventBusModule : MergeBlockModule, IMergeBlockModule
    {
        private readonly List<Type> _eventHandlerTypes = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventBusModule() : base("事件总线模块", "EventBus")
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            Assembly[] allModuleAssemblies = MergeBlockHost.ModuleInfos.Select(m => m.ModuleType.Assembly).Distinct().ToArray();
            context.Services.AddRabbitMQEventBus(context.Configuration.GetSection("EventBus"), allModuleAssemblies);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAfterAsync(IApplicationContext context)
        {
            context.ServiceProvider.UseEventBus();
            await base.OnApplicationInitAfterAsync(context);
        }
    }
}
