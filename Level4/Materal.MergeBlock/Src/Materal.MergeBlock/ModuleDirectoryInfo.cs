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
            FileInfo[] moduleDllFileInfos = directoryInfo.GetFiles("*.dll");
            if (autoRemoveAssemblies)
            {
                DirectoryInfo rootDirectoryInfo = new(AppDomain.CurrentDomain.BaseDirectory);
                FileInfo[] rootDllFileInfos = rootDirectoryInfo.GetFiles("*.dll");
                string[] rootDllFileNames = rootDllFileInfos.Select(m => m.Name).ToArray();
                foreach (FileInfo fileInfo in moduleDllFileInfos)
                {
                    if (rootDllFileNames.Contains(fileInfo.Name))
                    {
                        fileInfo.Delete();
                        fileInfo.Refresh();
                    }
                }
                moduleDllFileInfos = moduleDllFileInfos.Where(m => m.Exists).ToArray();
            }
            DirectoryInfo = directoryInfo;            
            LoadContext = directoryInfo.FullName != AppDomain.CurrentDomain.BaseDirectory ? new ModuleLoadContext(directoryInfo) : AssemblyLoadContext.Default;
            foreach (FileInfo dllFileInfo in moduleDllFileInfos)
            {
                if (!dllFileInfo.Exists) continue;
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
        }
    }
}
