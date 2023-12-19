namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块信息
    /// </summary>
    public interface IMergeBlockModuleInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        string ModuleName { get; }
        /// <summary>
        /// 模块程序集
        /// </summary>
        Assembly ModuleAssembly { get; }
        /// <summary>
        /// 模块特性
        /// </summary>
        MergeBlockAssemblyAttribute ModuleAttribute { get; }
        /// <summary>
        /// 模块类
        /// </summary>
        Type? ModuleType { get; }
        /// <summary>
        /// 模块
        /// </summary>
        IMergeBlockModule? Module { get; }
    }
}
