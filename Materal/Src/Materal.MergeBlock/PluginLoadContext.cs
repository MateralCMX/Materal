using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 插件加载上下文
    /// </summary>
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly string _rootPath;
        private readonly bool _hasMainAssembly;
        /// <summary>
        /// 卸载处理
        /// </summary>
        public event Action<PluginLoadContext>? UnloadingHandler;
        /// <summary>
        /// 加载程序集处理
        /// </summary>
        public static event Func<string, AssemblyName, Assembly?>? AssemblyLoadHandler;
        /// <summary>
        /// 名称
        /// </summary>
        public new string Name { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pluginPath"></param>
        /// <param name="name"></param>
        /// <param name="isCollectible"></param>
        /// <exception cref="MergeBlockException"></exception>
        public PluginLoadContext(string pluginPath, string name, bool isCollectible = true) : base(isCollectible)
        {
            if (string.IsNullOrEmpty(pluginPath)) throw new MergeBlockException("插件路径应该是指向主要程序集的非空完整路径");
            _hasMainAssembly = File.Exists(pluginPath);
            string? rootPath = _hasMainAssembly ? Path.GetDirectoryName(pluginPath) : pluginPath;
            _rootPath = rootPath ?? throw new MergeBlockException("获取插件根目录失败");
            Name = name;
            Unloading += PluginLoadContext_Unloading;
        }
        private void PluginLoadContext_Unloading(AssemblyLoadContext context)
        {
            if (UnloadingHandler is null || context is not PluginLoadContext pluginLoadContext) return;
            UnloadingHandler.Invoke(pluginLoadContext);
        }
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            Assembly? assembly = Default.Assemblies.FirstOrDefault(ass => ass.GetName().Name == assemblyName.Name);
            if (assembly is not null) return assembly;
            string? dllPath = null;
            if (!_hasMainAssembly)
            {
                string path = Path.Combine(_rootPath, assemblyName.Name + ".dll");
                if (File.Exists(path))
                {
                    dllPath = path;
                }
            }
            if (dllPath != null)
            {
                string baseDirectory = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
                    AppContext.BaseDirectory.TrimEnd('\\') :
                    AppContext.BaseDirectory.TrimEnd('/');
                if (Directory.GetParent(dllPath)?.FullName == baseDirectory)
                {
                    MateralServices.Logger?.LogTrace($"尝试从主环境加载[{assemblyName.Name}]");
                    return Default.LoadFromAssemblyPath(dllPath);
                }
                MateralServices.Logger?.LogTrace($"尝试从[{Name}]插件所在环境加载[{assemblyName.Name}]");
                return LoadFromAssemblyPath(dllPath);
            }
            dllPath = Path.Combine(AppContext.BaseDirectory, assemblyName.Name + ".dll");
            if (File.Exists(dllPath))
            {
                MateralServices.Logger?.LogTrace($"尝试从主环境加载[{assemblyName.Name}]");
                return Default.LoadFromAssemblyPath(dllPath);
            }
            MateralServices.Logger?.LogTrace($"尝试从[{Name}]所依赖的插件上下文中加载[{assemblyName.Name}]");
            return AssemblyLoadHandler?.Invoke(Name, assemblyName);
        }
        /// <summary>
        /// 加载非托管DLL
        /// </summary>
        /// <param name="unmanagedDllName"></param>
        /// <returns></returns>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName) => IntPtr.Zero;
    }
}