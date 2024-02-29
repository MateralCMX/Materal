using System.Runtime.Loader;

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
        public AssemblyLoadContext LoadContext { get; }
        /// <summary>
        /// 模块信息
        /// </summary>
        public List<IModuleInfo> ModuleInfos { get; } = [];
        /// <summary>
        /// 主程序集
        /// </summary>
        public List<Assembly> Assemblies { get; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="moduleBuilders"></param>
        /// <param name="autoRemoveAssemblies"></param>
        public ModuleDirectoryInfo(DirectoryInfo directoryInfo, IEnumerable<IModuleBuilder> moduleBuilders, bool autoRemoveAssemblies)
        {
            RemoveRootHasAssemblies(autoRemoveAssemblies, directoryInfo);
            FileInfo[] moduleDllFileInfos = directoryInfo.GetFiles("*.dll");
            DirectoryInfo = directoryInfo;
            LoadContext = directoryInfo.FullName != AppDomain.CurrentDomain.BaseDirectory ? new ModuleLoadContext(directoryInfo) : AssemblyLoadContext.Default;
            foreach (FileInfo dllFileInfo in moduleDllFileInfos)
            {
                if (!dllFileInfo.Exists) continue;
                try
                {
                    Assembly? assembly = LoadContext.LoadFromAssemblyPath(dllFileInfo.FullName);
                    if (assembly is null) continue;
                    Assemblies.Add(assembly);
                    List<Attribute> attributes = assembly.GetCustomAttributes().ToList();
                    if (!assembly.HasCustomAttribute<MergeBlockAssemblyAttribute>()) continue;
                    foreach (Type moduleType in assembly.GetTypes<IMergeBlockModule>())
                    {
                        IModuleInfo? moduleInfo = null;
                        foreach (IModuleBuilder moduleBuilder in moduleBuilders)
                        {
                            moduleInfo = moduleBuilder.GetModuleInfo(this, moduleType);
                            if (moduleInfo is not null) break;
                        }
                        moduleInfo ??= new ModuleInfo(this, moduleType);
                        ModuleInfos.Add(moduleInfo);
                    }
                }
                catch (Exception ex)
                {
                    MergeBlockHost.Logger?.LogDebug(ex, $"加载文件[{dllFileInfo.FullName}]失败");
                }
            }
        }
        /// <summary>
        /// 移除根目录下重复程序集
        /// </summary>
        /// <param name="autoRemoveAssemblies"></param>
        /// <param name="moduleDirectoryInfo"></param>
        private void RemoveRootHasAssemblies(bool autoRemoveAssemblies, DirectoryInfo moduleDirectoryInfo)
        {
            if (!autoRemoveAssemblies) return;
            DirectoryInfo rootDirectoryInfo = new(AppDomain.CurrentDomain.BaseDirectory);
            RemoveRootHasAssemblies(rootDirectoryInfo, moduleDirectoryInfo);
        }
        /// <summary>
        /// 移除根目录下重复程序集
        /// </summary>
        /// <param name="rootDirectoryInfo"></param>
        /// <param name="moduleDirectoryInfo"></param>
        private static void RemoveRootHasAssemblies(DirectoryInfo rootDirectoryInfo, DirectoryInfo moduleDirectoryInfo)
        {
            FileInfo[] rootDllFileInfos = rootDirectoryInfo.GetFiles("*.dll");
            string[] rootDllFileNames = rootDllFileInfos.Select(m => m.Name).ToArray();
            FileInfo[] moduleDllFileInfos = moduleDirectoryInfo.GetFiles("*.dll");
            foreach (FileInfo fileInfo in moduleDllFileInfos)
            {
                if (rootDllFileNames.Contains(fileInfo.Name))
                {
                    fileInfo.Delete();
                    fileInfo.Refresh();
                }
            }
            DirectoryInfo[] rootSubDirectoryInfos = rootDirectoryInfo.GetDirectories();
            DirectoryInfo[] moduleSubDirectoryInfos = moduleDirectoryInfo.GetDirectories();
            foreach (DirectoryInfo rootSubDirectoryInfo in rootSubDirectoryInfos)
            {
                DirectoryInfo? moduleSubDirectoryInfo = moduleSubDirectoryInfos.FirstOrDefault(m => m.Name == rootSubDirectoryInfo.Name);
                if (moduleSubDirectoryInfo is null) continue;
                RemoveRootHasAssemblies(rootSubDirectoryInfo, moduleSubDirectoryInfo);
                if (moduleDirectoryInfo.GetFiles().Length == 0 && moduleDirectoryInfo.GetDirectories().Length == 0) moduleDirectoryInfo.Delete();
            }
        }
    }
}
