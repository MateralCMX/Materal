namespace Materal.MergeBlock
{
    /// <summary>
    /// 模块加载器
    /// </summary>
    internal class ModuleLoader
    {
        private static readonly object _lockDescriptors = new();
        internal static List<ModuleDescriptor> ModuleDescriptors { get; set; } = [];
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public static List<ModuleDescriptor> LoadModules(Type startupModuleType, string pluginName)
        {
            List<Type> allModuleTypes = FindAllModuleTypes(startupModuleType);
            List<ModuleDescriptor> modules = [];
            foreach (Type type1 in allModuleTypes.Distinct())
            {
                Type type = type1;
                lock (_lockDescriptors)
                {
                    ModuleDescriptor? moduleDescriptor = ModuleDescriptors.FirstOrDefault(m => m.Type == type);
                    if (moduleDescriptor is null)
                    {
                        object? mergeBlockModuleObj = Activator.CreateInstance(type);
                        if (mergeBlockModuleObj is not IMergeBlockModule mergeBlockModule) continue;
                        moduleDescriptor = new(type, mergeBlockModule, pluginName);
                        ModuleDescriptors.Add(moduleDescriptor);
                        MateralServices.Logger?.LogDebug($"已加载[{type.FullName}_{moduleDescriptor.Instance.Name}]模块");
                    }
                    if (moduleDescriptor is not null)
                    {
                        if (modules.Contains(moduleDescriptor)) continue;
                        modules.Add(moduleDescriptor);
                    }
                }
            }
            SetDependencies(modules);
            return modules;
        }
        /// <summary>
        /// 设置依赖关系
        /// </summary>
        /// <param name="modules"></param>
        private static void SetDependencies(List<ModuleDescriptor> modules)
        {
            foreach (ModuleDescriptor module in modules)
            {
                SetDependencies(modules, module);
            }
        }
        /// <summary>
        /// 设置依赖关系
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="module"></param>
        /// <exception cref="Exception"></exception>
        private static void SetDependencies(List<ModuleDescriptor> modules, ModuleDescriptor module)
        {
            foreach (Type dependedModuleType1 in FindDependedModuleTypes(module.Type))
            {
                Type dependedModuleType = dependedModuleType1;
                module.AddDependency(modules.FirstOrDefault(m => m.Type == dependedModuleType) ?? throw new MergeBlockException($"[{module.Type.FullName}]的依赖模块[{dependedModuleType.FullName}]不存在"));
            }
        }
        /// <summary>
        /// 查找所有模块
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        internal static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            List<Type> moduleTypes = [];
            MateralServices.Logger?.LogDebug($"正在加载模块[{startupModuleType.FullName}]及其依赖模块");
            AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType);
            return moduleTypes;
        }
        /// <summary>
        /// 查找依赖模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        internal static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IMergeBlockModule.IsMergeBlockModule(moduleType)) throw new ArgumentException($"类型[{moduleType.FullName}]不是模块类型");
            List<Type> dependedModuleTypes = [];
            foreach (DependsOnAttribute dependsOnAttribute in moduleType.GetCustomAttributes().OfType<DependsOnAttribute>())
            {
                foreach (Type dependedType in dependsOnAttribute.DependedTypes)
                {
                    if (dependedModuleTypes.Contains(dependedType)) continue;
                    dependedModuleTypes.Add(dependedType);
                }
            }
            return dependedModuleTypes;
        }
        /// <summary>
        /// 递归添加模块及其依赖模块
        /// </summary>
        /// <param name="moduleTypes"></param>
        /// <param name="moduleType"></param>
        /// <param name="depth"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void AddModuleAndDependenciesRecursively(List<Type> moduleTypes, Type moduleType, int depth = 0)
        {
            if (!IMergeBlockModule.IsMergeBlockModule(moduleType)) throw new ArgumentException($"类型[{moduleType.FullName}]不是模块类型");
            if (moduleTypes.Contains(moduleType)) return;
            moduleTypes.Add(moduleType);
            MateralServices.Logger?.LogDebug($"加载模块[{moduleType.FullName}]");
            foreach (Type dependedModuleType in FindDependedModuleTypes(moduleType))
            {
                AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType, depth + 1);
            }
        }
    }
}
