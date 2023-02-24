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
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Func<Type, bool> filter, params Type[] argTypes)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? targetType = null;
            if (_cacheTypes.ContainsKey(typeName))
            {
                targetType = _cacheTypes[typeName];
            }
            else
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    Type? tempType = assembly.GetTypes().Where(m => filter(m)).FirstOrDefault();
                    if (tempType == null) continue;
                    ConstructorInfo? constructorInfo = tempType.GetConstructor(argTypes);
                    if (constructorInfo == null) continue;
                    targetType = tempType;
                    break;
                }
            }
            if (targetType == null) return null;
            if (!_cacheTypes.ContainsKey(typeName))
            {
                lock (_operationCacheObjectLock)
                {

                    if (!_cacheTypes.ContainsKey(typeName))
                    {
                        _cacheTypes.Add(typeName, targetType);
                    }
                }
            }
            return targetType;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, params Type[] argTypes) => typeName.GetTypeByTypeName(m => m.Name == typeName && m.IsClass && !m.IsAbstract, argTypes);
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
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName(this string typeName, Type targetType, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? result = GetTypeByTypeName(typeName, args);
            if (result == null || !result.IsAssignableTo(targetType)) return null;
            return result;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static Type? GetTypeByTypeName<T>(this string typeName, params object[] args) => GetTypeByTypeName(typeName, typeof(T), args);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType(this string typeName, Type parentType, params Type[] argTypes) => typeName.GetTypeByTypeName((m => m.Name == typeName && m.IsClass && !m.IsAbstract && TypeExtension.IsAssignableTo(m, parentType)), argTypes);
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <returns></returns>
        public static Type? GetTypeByParentType<T>(this string typeName, params Type[] argTypes) => typeName.GetTypeByParentType(parentType: typeof(T), argTypes);
    }
}
