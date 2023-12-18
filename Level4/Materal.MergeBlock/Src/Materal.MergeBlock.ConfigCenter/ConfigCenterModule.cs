using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RC.ConfigClient;

namespace Materal.MergeBlock.ConfigCenter
{
    /// <summary>
    /// 配置中心模块
    /// </summary>
    public abstract class ConfigCenterModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        protected abstract string ProjectName { get; }
        /// <summary>
        /// 命名空间
        /// </summary>
        protected abstract string[] Namespaces { get; }
        private const string _configKey = "ConfigUrl";
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            if (context.Configuration is not IConfigurationBuilder configuration) return;
            string? url = context.Configuration.GetValue(_configKey);
            if (url is null || !url.IsUrl()) throw new MergeBlockException("配置中心地址错误");
            configuration.AddDefaultNameSpace(url, ProjectName);
            foreach (string namespaceName in Namespaces)
            {
                configuration.AddNameSpace(namespaceName);
            }
            await base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            await base.OnConfigServiceAsync(context);
        }
    }
}
