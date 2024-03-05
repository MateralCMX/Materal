namespace Materal.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class StringExtensions
    {
        #region 私有方法
        /// <summary>
        /// 获得参数类型数组
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Type[] GetArgTypes(this object[] args) => args.Select(m => m.GetType()).ToArray();
        #endregion
        #region 根据过滤条件筛选
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Func<Type, bool> filter, Type[]? argTypes = null)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? tempType = assembly.GetTypes().Where(m => filter(m)).FirstOrDefault();
            if (tempType is null) return null;
            if (argTypes is not null)
            {
                ConstructorInfo? constructorInfo = tempType.GetConstructor(argTypes);
                if (constructorInfo is null) return null;
            }
            Type targetType = tempType;
            if (targetType is null) return null;
            return targetType;
        }
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Func<Type, bool> filter, Type[]? argTypes = null)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? targetType = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                targetType = typeName.GetTypeByTypeName(assembly, filter, argTypes);
                if (targetType is not null) break;
            }
            if (targetType is null) return null;
            return targetType;
        }
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Func<Type, bool> filter, object[] args) 
            => GetTypeByTypeName(typeName, assembly, filter, args.GetArgTypes());
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Func<Type, bool> filter, object[] args)
            => GetTypeByTypeName(typeName, filter, args.GetArgTypes());
        #endregion
        #region 名称相同、是类、不是抽象类
        /// <summary>
        /// 是否是类并且不是抽象类
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsClassAndNotAbstract(string typeName, Type type) => type.Name == typeName && type.IsClass && !type.IsAbstract;
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(assembly, m => IsClassAndNotAbstract(typeName, m), argTypes);
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(m => IsClassAndNotAbstract(typeName, m), argTypes);
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, object[] args)
            => typeName.GetTypeByTypeName(assembly, args.GetArgTypes());
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, object[] args)
            => typeName.GetTypeByTypeName(args.GetArgTypes());
        #endregion
        #region 名称相同、是类、不是抽象类、继承自指定类型
        /// <summary>
        /// 是否是类并且不是抽象类、继承自指定类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static bool IsClassAndNotAbstractAndInheritance(string typeName, Type type, Type targetType)
            => IsClassAndNotAbstract(typeName, type) && type.IsAssignableTo(targetType);
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="targetType"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Type targetType, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(assembly, m => IsClassAndNotAbstractAndInheritance(typeName, m, targetType), argTypes);
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Type targetType, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(m => IsClassAndNotAbstractAndInheritance(typeName, m, targetType), argTypes);
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Type targetType, object[] args)
            => typeName.GetTypeByTypeName(assembly, targetType, args.GetArgTypes());
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Type targetType, object[] args)
            => typeName.GetTypeByTypeName(targetType, args.GetArgTypes());
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, Assembly assembly, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(assembly, m => IsClassAndNotAbstractAndInheritance(typeName, m, typeof(T)), argTypes);
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, Type[]? argTypes = null)
            => typeName.GetTypeByTypeName(m => IsClassAndNotAbstractAndInheritance(typeName, m, typeof(T)), argTypes);
        /// <summary>
        /// 根据类型名称在指定程序集中获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, Assembly assembly, object[] args)
            => typeName.GetTypeByTypeName(assembly, typeof(T), args.GetArgTypes());
        /// <summary>
        /// 根据类型名称获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, object[] args)
            => typeName.GetTypeByTypeName(typeof(T), args.GetArgTypes());
        #endregion
#if NETSTANDARD
        /// <summary>
        /// 以inputChar开始
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="inputChar"></param>
        /// <returns></returns>
        public static bool StartsWith(this string inputString, char inputChar) => inputString.StartsWith(inputChar.ToString());
        /// <summary>
        /// 以inputChar开始
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="inputChar"></param>
        /// <returns></returns>
        public static bool EndsWith(this string inputString, char inputChar) => inputString.EndsWith(inputChar.ToString());
#endif
    }
}
