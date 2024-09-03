namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock上下文
    /// </summary>
    public class MergeBlockContext
    {
        /// <summary>
        /// MergeBlock程序集
        /// </summary>
        public IList<Assembly> MergeBlockAssemblies { get; set; } = [];
        /// <summary>
        /// 模块描述
        /// </summary>
        public IList<ModuleDescriptor> ModuleDescriptors { get; set; } = [];
        /// <summary>
        /// 插件
        /// </summary>
        public IList<IPlugin> Plugins { get; set; } = [];
    }
}
