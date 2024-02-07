using System.Reflection;
using System.Runtime.Loader;

namespace Materal.MergeBlock
{
    /// <summary>
    /// Module加载上下文
    /// </summary>
    public class ModuleLoadContext : AssemblyLoadContext
    {
        private readonly List<AssemblyDependencyResolver> _resolvers = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        public ModuleLoadContext(DirectoryInfo moduleDirectoryInfo)
        {
            FileInfo[] allDllFile = moduleDirectoryInfo.GetFiles("*.dll");
            foreach (FileInfo fileInfo in allDllFile)
            {
                AssemblyDependencyResolver resolver = new(fileInfo.FullName);
                _resolvers.Add(resolver);
            }
        }
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            foreach (AssemblyDependencyResolver resolver in _resolvers)
            {
                string? assemblyPath = resolver.ResolveAssemblyToPath(assemblyName);
                if (assemblyPath == null) continue;
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }
        /// <summary>
        /// 加载未托管的DLL
        /// </summary>
        /// <param name="unmanagedDllName"></param>
        /// <returns></returns>
        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            foreach (AssemblyDependencyResolver resolver in _resolvers)
            {
                string? libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
                if (libraryPath == null) continue;
                return LoadUnmanagedDllFromPath(libraryPath);
            }
            return 0;
        }
    }
}
