using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RC.ConfigClient;

namespace Materal.MergeBlock.ConfigCenter
{
    public abstract class ConfigCenterModule : MergeBlockModule, IMergeBlockModule
    {
        protected abstract string ProjectName { get; }
        protected abstract string[] Namespaces { get; }
        private const string _configKey = "ConfigUrl";
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
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.MvcBuilder?.AddApplicationPart(GetType().Assembly);
            await base.OnConfigServiceAsync(context);
        }
    }
}
