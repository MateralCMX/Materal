using Materal.Extensions;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class StringExtension
    {

        private readonly static Dictionary<string, Type> _cacheTypes = new();
        private readonly static object _operationCacheObjectLock = new();
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="filter">过滤器</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Func<Type, bool> filter, params Type[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? triggerDataType = null;
            if (_cacheTypes.ContainsKey(typeName))
            {
                triggerDataType = _cacheTypes[typeName];
            }
            else
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                Type[] argTypes = args.Select(m => m.GetType()).ToArray();
                foreach (Assembly assembly in assemblies)
                {
                    Type? targetType = assembly.GetTypes().Where(m => filter(m)).FirstOrDefault();
                    if (targetType == null) continue;
                    ConstructorInfo? constructorInfo = targetType.GetConstructor(argTypes);
                    if (constructorInfo == null) continue;
                    triggerDataType = targetType;
                    break;
                }
            }
            if (triggerDataType == null) return null;
            if (!_cacheTypes.ContainsKey(typeName))
            {
                lock (_operationCacheObjectLock)
                {

                    if (!_cacheTypes.ContainsKey(typeName))
                    {
                        _cacheTypes.Add(typeName, triggerDataType);
                    }
                }
            }
            return triggerDataType;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, params Type[] args) => typeName.GetTypeByTypeName(m => m.Name == typeName && m.IsClass && !m.IsAbstract, args);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            return GetTypeByTypeName(typeName, m => m.Name == typeName && m.IsClass && !m.IsAbstract, argTypes);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? triggerDataType = GetTypeByTypeName(typeName, args);
            if (triggerDataType == null || !triggerDataType.IsAssignableTo(typeof(T))) return null;
            return triggerDataType;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType(this string typeName, Type parentType, params Type[] args) => typeName.GetTypeByTypeName((m => m.Name == typeName && m.IsClass && !m.IsAbstract && TypeExtension.IsAssignableTo(m, parentType)), args);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType<T>(this string typeName, params Type[] args) => typeName.GetTypeByParentType(parentType: typeof(T), args);
    }
}
