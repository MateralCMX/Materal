using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.ConfigCenter
{
    /// <summary>
    /// 配置中心模块
    /// </summary>
    public abstract class ConfigCenterModule(string moduleName) : MergeBlockModule(moduleName), IConfigCenterModule
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public abstract string ProjectName { get; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public abstract string[] Namespaces { get; }
        /// <summary>
        /// 重载配置时间
        /// </summary>
        public virtual int ReloadSecondInterval { get; } = 60;
        /// <inheritdoc/>
        public override void OnPreConfigureServices(ServiceConfigurationContext context)
            => ConfigCenterModuleHelper.OnConfigServiceBefore(context, ProjectName, Namespaces, ReloadSecondInterval);
    }
}
