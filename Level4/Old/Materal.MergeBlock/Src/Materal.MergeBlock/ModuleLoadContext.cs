using System.Runtime.Loader;

namespace Materal.MergeBlock
{
    internal class ModuleLoadContext : AssemblyLoadContext
    {
        private readonly List<AssemblyDependencyResolver> _resolvers = [];
        public ModuleLoadContext(DirectoryInfo moduleDirectoryInfo)
        {
            FileInfo[] allDllFile = moduleDirectoryInfo.GetFiles("*.dll");
            foreach (FileInfo fileInfo in allDllFile)
            {
                AssemblyDependencyResolver resolver = new(fileInfo.FullName);
                _resolvers.Add(resolver);
            }
        }
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
        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            foreach (AssemblyDependencyResolver resolver in _resolvers)
            {
                string? libraryPath = resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
                if (libraryPath == null) continue;
                return LoadUnmanagedDllFromPath(libraryPath);
            }
            return nint.Zero;
        }
    }
}
