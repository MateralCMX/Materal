using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.Abstractions.WebModule.ControllerHttpHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.Core.Abstractions;

namespace RC.Core.Application
{
    /// <summary>
    /// RC模块
    /// </summary>
    public abstract class RCModule : MergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        protected RCModule(string description, string[]? depends) : base(description, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        protected RCModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RCConfig.json");
            context.Configuration.AddJsonFile(configFilePath, true, true);
            Type moduleType = GetType();
            configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            context.Configuration.AddJsonFile(configFilePath, true, true);
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddSingleton<IControllerHttpHelper, RCControllerHttpHelper>();
            await base.OnConfigServiceAsync(context);
        }
    }
}