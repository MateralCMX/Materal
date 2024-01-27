namespace Materal.MergeBlock
{
    /// <summary>
    /// 模块文件夹信息
    /// </summary>
    public class ModuleDirectoryInfo : IModuleDirectoryInfo
    {
        /// <summary>
        /// 文件夹信息
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; }
        /// <summary>
        /// 加载上下文
        /// </summary>
        public ModuleLoadContext LoadContext { get; }
        /// <summary>
        /// 模块信息
        /// </summary>
        public List<IModuleInfo> ModuleInfos { get; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="moduleBuilders"></param>
        public ModuleDirectoryInfo(DirectoryInfo directoryInfo, IEnumerable<IModuleBuilder> moduleBuilders)
        {
            DirectoryInfo = directoryInfo;
            LoadContext = new(directoryInfo);
            foreach (FileInfo dllFileInfo in directoryInfo.EnumerateFiles("*.dll"))
            {
                if (!dllFileInfo.Exists) continue;
                Assembly? assembly = LoadContext.LoadFromAssemblyPath(dllFileInfo.FullName);
                if (assembly is null) continue;
                if (!assembly.HasCustomAttribute<MergeBlockAssemblyAttribute>()) continue;
                List<Type> moduleTypes = assembly.GetTypes().Where(IsMergeBlockModule).ToList();
                foreach (Type moduleType in moduleTypes)
                {
                    IModuleInfo? moduleInfo = null;
                    foreach (IModuleBuilder moduleBuilder in moduleBuilders)
                    {
                        moduleInfo = moduleBuilder.GetModuleInfo(this, moduleType);
                        if(moduleInfo is not null) break;
                    }
                    moduleInfo ??= new ModuleInfo(this, moduleType);
                    ModuleInfos.Add(moduleInfo);
                }
            }
        }
        /// <summary>
        /// 是否为MergeBlock模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsMergeBlockModule(Type type) => type.IsClass && !type.IsAbstract && type.IsAssignableTo<IMergeBlockModule>();
    }
}
