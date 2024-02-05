using Materal.MergeBlock.Abstractions;

namespace Materal.MergeBlock.ConfigCenter
{
    /// <summary>
    /// 配置中心模块
    /// </summary>
    public abstract class ConfigCenterModule : MergeBlockModule, IMergeBlockModule, IConfigCenterModule
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
        public override async Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            ConfigCenterModuleHelper.OnConfigServiceBefore(context, ProjectName, Namespaces, ReloadSecondInterval);
            await base.OnConfigServiceBeforeAsync(context);
        }
    }
}
