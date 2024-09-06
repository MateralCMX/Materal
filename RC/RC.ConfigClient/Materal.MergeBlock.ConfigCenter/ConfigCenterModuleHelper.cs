using Materal.Extensions;
using Materal.Extensions.DependencyInjection;
using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RC.ConfigClient.Extensions;

namespace Materal.MergeBlock.ConfigCenter
{
    /// <summary>
    /// 配置中心模块帮助类
    /// </summary>
    public static class ConfigCenterModuleHelper
    {
        private const string _configKey = "ConfigUrl";
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <param name="projectName"></param>
        /// <param name="namespaces"></param>
        /// <param name="reloadSecondInterval"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        public static void OnConfigServiceBefore(ServiceConfigurationContext context, string projectName, string[] namespaces, int reloadSecondInterval)
        {
            if (context.Configuration is not IConfigurationBuilder configuration) return;
            string? url = context.Configuration.GetConfigItemToString(_configKey);
            if (url is null || !url.IsUrl())
            {
                MateralServices.Logger?.LogWarning($"配置中心地址错误:[{url}]");
                return;
            }
            configuration.AddDefaultNameSpace(url, projectName, reloadSecondInterval).AddNameSpaces(namespaces);
            string namespacesStr = "Application";
            if (namespaces.Length > 0) namespacesStr += $",{string.Join(",", namespaces)}";
            MateralServices.Logger?.LogInformation($"已从[{url}]加载{projectName}[{namespacesStr}]配置");
        }
    }
}
