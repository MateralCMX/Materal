using System.Runtime.Loader;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 模块文件夹信息
    /// </summary>
    public interface IModuleDirectoryInfo
    {
        /// <summary>
        /// 文件夹信息
        /// </summary>
        DirectoryInfo DirectoryInfo { get; }
        /// <summary>
        /// 加载上下文
        /// </summary>
        AssemblyLoadContext LoadContext { get; }
        /// <summary>
        /// 模块信息
        /// </summary>
        List<IModuleInfo> ModuleInfos { get; }
    }
}
