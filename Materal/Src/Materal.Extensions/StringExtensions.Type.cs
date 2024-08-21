namespace Materal.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => FullNameEqualIsClassAndNotAbstract(typeName, m), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => NameEqualIsClassAndNotAbstract(typeName, m), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, Type[] argTypes, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => FullNameEqualIsClassAndNotAbstract(typeName, m), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type[] argTypes, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => NameEqualIsClassAndNotAbstract(typeName, m), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type[] argTypes = GetArgTypes(args);
            return GetTypeByTypeFullName(typeName, argTypes, assemblies);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type[] argTypes = GetArgTypes(args);
            return GetTypeByTypeName(typeName, argTypes, assemblies);
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, Type targetType, object[] args, Assembly[]? assemblies = null)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? result = GetTypeByTypeFullName(typeName, args, assemblies);
            if (result == null || !result.IsAssignableTo(targetType)) return null;
            return result;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type targetType, object[] args, Assembly[]? assemblies = null)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            Type? result = GetTypeByTypeName(typeName, args, assemblies);
            if (result == null || !result.IsAssignableTo(targetType)) return null;
            return result;
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName<T>(this string typeName, object[] args, Assembly[]? assemblies = null) => GetTypeByTypeFullName(typeName, typeof(T), args, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName<T>(this string typeName, object[] args, Assembly[]? assemblies = null) => GetTypeByTypeName(typeName, typeof(T), args, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, Type parentType, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => FullNameEqualIsClassAndNotAbstractAndInheritance(typeName, m, parentType), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type parentType, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => NameEqualIsClassAndNotAbstractAndInheritance(typeName, m, parentType), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName(this string typeName, Type parentType, Type[] argTypes, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => FullNameEqualIsClassAndNotAbstractAndInheritance(typeName, m, parentType), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName(this string typeName, Type parentType, Type[] argTypes, Assembly[]? assemblies = null) => TypeHelper.GetTypeByFilter(m => NameEqualIsClassAndNotAbstractAndInheritance(typeName, m, parentType), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName<T>(this string typeName, Assembly[]? assemblies = null) => GetTypeByTypeFullName(typeName, typeof(T), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName<T>(this string typeName, Assembly[]? assemblies = null) => GetTypeByTypeName(typeName, typeof(T), assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeFullName<T>(this string typeName, Type[] argTypes, Assembly[]? assemblies = null) => GetTypeByTypeName(typeName, typeof(T), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <typeparam name="T">父级类型</typeparam>
        /// <param name="typeName"></param>
        /// <param name="argTypes"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static Type? GetTypeByTypeName<T>(this string typeName, Type[] argTypes, Assembly[]? assemblies = null) => GetTypeByTypeName(typeName, typeof(T), argTypes, assemblies);
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeFullName(this string typeName, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeFullName(typeName, assemblies);
            if (type == null) return null;
            return type.Instantiation();
        }
        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeName(typeName, assemblies);
            if (type == null) return null;
            return type.Instantiation();
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeFullName(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeFullName(typeName, args, assemblies);
            if (type == null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeName(typeName, args, assemblies);
            if (type == null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeFullName<T>(this string typeName, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeFullName<T>(typeName, assemblies);
            if (type == null) return default;
            object? typeObject = type.Instantiation();
            if (typeObject == null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeName<T>(typeName, assemblies);
            if (type == null) return default;
            object? typeObject = type.Instantiation();
            if (typeObject == null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeFullName<T>(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeFullName<T>(typeName, args, assemblies);
            if (type == null) return default;
            object? typeObject = type.Instantiation(args);
            if (typeObject == null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, object[] args, Assembly[]? assemblies = null)
        {
            Type? type = GetTypeByTypeName<T>(typeName, args, assemblies);
            if (type == null) return default;
            object? typeObject = type.Instantiation(args);
            if (typeObject == null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 获得参数类型数组
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Type[] GetArgTypes(object[] args) => args.Select(m => m.GetType()).ToArray();
        /// <summary>
        /// 是否是类并且不是抽象类
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool NameEqualIsClassAndNotAbstract(string typeName, Type type) => type.Name == typeName && type.IsClass && !type.IsAbstract;
        /// <summary>
        /// 是否是类并且不是抽象类
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool FullNameEqualIsClassAndNotAbstract(string typeName, Type type) => type.FullName == typeName && type.IsClass && !type.IsAbstract;
        /// <summary>
        /// 是否是类并且不是抽象类
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static bool NameEqualIsClassAndNotAbstractAndInheritance(string typeName, Type type, Type targetType) => NameEqualIsClassAndNotAbstract(typeName, type) && type.IsAssignableTo(targetType);
        /// <summary>
        /// 是否是类并且不是抽象类
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static bool FullNameEqualIsClassAndNotAbstractAndInheritance(string typeName, Type type, Type targetType) => FullNameEqualIsClassAndNotAbstract(typeName, type) && type.IsAssignableTo(targetType);
    }
}
