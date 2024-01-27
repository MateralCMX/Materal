namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 模块文件夹信息
    /// </summary>
    public interface IModuleDirectoryInfo
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        List<IModuleInfo> ModuleInfos { get; }
    }
}
