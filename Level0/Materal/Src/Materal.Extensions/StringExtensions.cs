namespace Materal.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// 获得类型
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
        /// 获得类型
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
                if(targetType is not null) break;
            }
            if (targetType is null) return null;
            return targetType;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type[] argTypes) => typeName.GetTypeByTypeName(m => m.Name == typeName && m.IsClass && !m.IsAbstract, argTypes);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName) => typeName.GetTypeByTypeName(m => m.Name == typeName && m.IsClass && !m.IsAbstract);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            return GetTypeByTypeName(typeName, assembly, m => m.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) && m.IsClass && !m.IsAbstract, argTypes);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            return GetTypeByTypeName(typeName, m => m.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) && m.IsClass && !m.IsAbstract, argTypes);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Type targetType, object[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? result = GetTypeByTypeName(typeName, args);
            if (result is null || !result.IsAssignableTo(targetType)) return null;
            return result;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Assembly assembly, Type targetType, object[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? result = GetTypeByTypeName(typeName, assembly, args);
            if (result is null || !result.IsAssignableTo(targetType)) return null;
            return result;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, Assembly assembly, object[] args) => GetTypeByTypeName(typeName, assembly, typeof(T), args);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, object[] args) => GetTypeByTypeName(typeName, typeof(T), args);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName) => GetTypeByParentType(typeName, typeof(T));
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType(this string typeName, Type parentType, Type[] argTypes) => typeName.GetTypeByTypeName((m => m.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) && m.IsClass && !m.IsAbstract && m.IsAssignableTo(parentType)), argTypes);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType(this string typeName, Type parentType) => typeName.GetTypeByTypeName((m => m.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase) && m.IsClass && !m.IsAbstract && m.IsAssignableTo(parentType)));
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType<T>(this string typeName, Type[] argTypes) => typeName.GetTypeByParentType(parentType: typeof(T), argTypes);
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
