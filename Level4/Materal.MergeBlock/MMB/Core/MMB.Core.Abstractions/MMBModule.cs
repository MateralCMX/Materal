using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.ConfigCenter;

namespace MMB.Core.Abstractions
{
    /// <summary>
    /// MMB模块-配置中心
    /// </summary>
    /// <param name="namespaces"></param>
    public abstract class MMBModule(params string[] namespaces) : ConfigCenterModule, IMergeBlockModule
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        protected override string ProjectName => "MMBProject";
        /// <summary>
        /// 命名空间
        /// </summary>
        protected override string[] Namespaces { get; } = namespaces;
    }
}
