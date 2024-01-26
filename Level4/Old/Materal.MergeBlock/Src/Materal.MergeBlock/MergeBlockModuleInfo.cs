namespace Materal.MergeBlock
{
    /// <summary>
    /// MergeBlock模块信息
    /// </summary>
    public class MergeBlockModuleInfo : IMergeBlockModuleInfo
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName => ModuleAttribute.ModuleName ?? ModuleAssembly.GetName().Name ?? throw new MemberAccessException("获取模块名称失败");
        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly ModuleAssembly { get; }
        /// <summary>
        /// 模块信息
        /// </summary>
        public MergeBlockAssemblyAttribute ModuleAttribute => ModuleAssembly.GetCustomAttribute<MergeBlockAssemblyAttribute>() ?? throw new MergeBlockException("程序集不是MergeBlock模块");
        /// <summary>
        /// 模块类
        /// </summary>
        public Type? ModuleType { get; }
        /// <summary>
        /// 模块
        /// </summary>
        public IMergeBlockModule? Module { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockModuleInfo(Assembly moduleAssembly)
        {
            ModuleAssembly = moduleAssembly;
            ModuleType = ModuleAssembly.GetTypes().FirstOrDefault(type => type.IsClass && !type.IsAbstract && type.IsAssignableTo<IMergeBlockModule>());
            if (ModuleType is null) return;
            Module = ModuleType.Instantiation<IMergeBlockModule>();
        }
    }
}
