using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Microsoft.Extensions.Configuration;
using RC.ConfigClient.Extensions;

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
        /// <summary>
        /// 重载配置时间
        /// </summary>
        protected virtual int ReloadSecondInterval { get; } = 60;
        private const string _configKey = "ConfigUrl";
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="depends"></param>
        protected ConfigCenterModule(string description, string[]? depends) : base(description, depends)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="description"></param>
        /// <param name="moduleName"></param>
        /// <param name="depends"></param>
        protected ConfigCenterModule(string description, string? moduleName = null, string[]? depends = null) : base(description, moduleName, depends)
        {
        }
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
            configuration.AddDefaultNameSpace(url, ProjectName, ReloadSecondInterval).AddNameSpaces(Namespaces);
            await base.OnConfigServiceBeforeAsync(context);
        }
    }
}
