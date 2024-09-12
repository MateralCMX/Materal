using System.Runtime.CompilerServices;
using System.Runtime.Loader;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 插件
    /// </summary>
    public class Plugin() : IPlugin
    {
        internal static readonly string[] ExcludedAssemblies =
        [
          "Materal.MergeBlock",
          "Materal.MergeBlock.Abstractions"
        ];
        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;
        /// <inheritdoc/>
        public AssemblyLoadContext? LoadContext { get; private set; }
        /// <inheritdoc/>
        public string RootPath { get; set; } = string.Empty;
        /// <inheritdoc/>
        public PluginType PluginType { get; set; } = PluginType.Service | PluginType.Module;
        /// <inheritdoc/>
        public string? StartModule { get; set; }
        /// <inheritdoc/>
        public Type? StartModuleType { get; set; }
        /// <inheritdoc/>
        public int RecursivelyScan { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public List<string> AssemblyNames { get; private set; } = [];
        /// <inheritdoc/>
        public List<Assembly> Assemblies { get; } = [];
        /// <inheritdoc/>
        public Dictionary<Type, ModuleDescriptor> Modules { get; } = [];
        /// <inheritdoc/>
        public List<string> DependentPlugins { get; set; } = [];
        /// <inheritdoc/>
        public bool IsCollectible { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public Plugin(string name, string rootPath, string? startModule = null, bool isCollectible = false) : this()
        {
            Name = name;
            RootPath = rootPath;
            StartModule = startModule;
            IsCollectible = isCollectible;
        }
        /// <inheritdoc/>
        public void EnsurePlugin()
        {
            if (string.IsNullOrWhiteSpace(RootPath))
            {
                RootPath = Path.GetFullPath(AppContext.BaseDirectory);
            }
            if (!string.IsNullOrWhiteSpace(StartModule))
            {
                //ProjectName.ModuleName.ModuleClassName
                string startModule = StartModule;
                if (startModule.ToLower().EndsWith(".dll"))
                {
                    startModule = startModule[..^4];
                }
                string rootPath = Path.Combine(RootPath, $"{startModule}.dll");
                LoadContext = new PluginLoadContext(rootPath, Name, IsCollectible);
                string name = startModule.Split('.').Last();
                string assemblyName = startModule[..^(name.Length + 1)];
                Assembly assembly = LoadContext.LoadFromAssemblyName(new AssemblyName(assemblyName));
                Assemblies.Add(assembly);
                StartModuleType = assembly.GetType(name);
            }
            else
            {
                LoadContext = new PluginLoadContext(RootPath, Name, IsCollectible);
                LoadAssemblies();
            }
        }
        /// <summary>
        /// 加载程序集
        /// </summary>
        private void LoadAssemblies()
        {
            if (AssemblyNames.Count <= 0)
            {
                AssemblyNames = GetMergeBlockAssembly(out WeakReference alcWeakRef);
                for (int index = 0; alcWeakRef.IsAlive && index < 10; ++index)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            foreach (string assemblyName in AssemblyNames)
            {
                if (LoadContext is null) continue;
                Assemblies.Add(LoadContext.LoadFromAssemblyName(new AssemblyName(assemblyName)));
            }
        }
        /// <summary>
        /// 获取MergeBlock程序集
        /// </summary>
        /// <param name="alcWeakRef"></param>
        /// <returns></returns>
        private List<string> GetMergeBlockAssembly(out WeakReference alcWeakRef)
        {
            List<string> result = [];
            PluginLoadContext target = new(RootPath, Name);
            alcWeakRef = new WeakReference(target, true);
            if (!Directory.Exists(RootPath)) return result;
            foreach (string file in Directory.GetFiles(RootPath, "*.dll"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                if (ExcludedAssemblies.Contains(name)) continue;
                try
                {
                    Assembly assembly = target.LoadFromAssemblyName(new AssemblyName(name));
                    if (!assembly.HasCustomAttribute<MergeBlockAssemblyAttribute>()) continue;
                    result.Add(name);
                }
                catch
                {
                    continue;
                }
            }
            target.Unload();
            return result;
        }
        /// <inheritdoc/>
        public override string ToString()
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler = new(1, 2);
            interpolatedStringHandler.AppendFormatted(Name);
            interpolatedStringHandler.AppendLiteral(" ");
            interpolatedStringHandler.AppendFormatted(PluginType);
            return interpolatedStringHandler.ToStringAndClear();
        }
    }
}
