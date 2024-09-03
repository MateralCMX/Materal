using Newtonsoft.Json;
using System.Runtime.Loader;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 插件基类
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 插件根目录
        /// </summary>
        string RootPath { get; }
        /// <summary>
        /// 插件类型
        /// </summary>
        PluginType PluginType { get; }
        /// <summary>
        /// 启动模块名称，包含程序集名称(如果为空且<see cref="P:Materal.MergeBlock.Abstractions.IPluginBase.PluginType" /> = <seealso cref="F:Materal.MergeBlock.Abstractions.PluginType.Module" />则扫描其中的<see cref="T:Materal.MergeBlock.Abstractions.IMergeBlockModule" />)
        /// </summary>
        string? StartModule { get; }
        /// <summary>
        /// 启动模块类型
        /// </summary>
        [JsonIgnore]
        Type? StartModuleType { get; }
        /// <summary>
        /// 递归扫描子目录
        /// </summary>
        [JsonIgnore]
        int RecursivelyScan { get; set; }
        /// <summary>
        /// 程序集列表
        /// </summary>
        [JsonIgnore]
        List<Assembly> Assemblies { get; }
        /// <summary>
        /// 模块类型列表
        /// </summary>
        [JsonIgnore]
        Dictionary<Type, ModuleDescriptor> Modules { get; }
        /// <summary>
        /// 依赖的插件集合
        /// </summary>
        List<string> DependentPlugins { get; set; }
        /// <summary>
        /// 是否可回收
        /// </summary>
        bool IsCollectible { get; }
        /// <summary>
        /// 加载上下文
        /// </summary>
        [JsonIgnore]
        AssemblyLoadContext? LoadContext { get; }
        /// <summary>
        /// 确保插件
        /// </summary>
        void EnsurePlugin();
    }
}
