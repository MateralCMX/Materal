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
        protected override string ProjectName => "MMBProject";
        protected override string[] Namespaces { get; } = namespaces;
    }
}
