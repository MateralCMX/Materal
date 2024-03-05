namespace Materal.Extensions
{
    /// <summary>
    /// 字符串转换扩展类
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ConvertToEnum<T>(this string value, bool ignoreCase = true) where T : Enum => (T)Enum.Parse(typeof(T), value, ignoreCase);
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string FirstLower(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return inputString;
            return inputString[0].ToString().ToLower() + inputString[1..];
        }
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string FirstUpper(this string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) return inputString;
            return inputString[0].ToString().ToUpper() + inputString[1..];
        }
        #region 根据类型名称获得对象
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Assembly assembly, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(assembly);
            }
            else
            {
                type = typeName.GetTypeByTypeName(assembly, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName();
            }
            else
            {
                type = typeName.GetTypeByTypeName(args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(args);
        }
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Assembly assembly, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(assembly);
            }
            else
            {
                type = typeName.GetTypeByTypeName(assembly, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(serviceProvider, args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName();
            }
            else
            {
                type = typeName.GetTypeByTypeName(args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(serviceProvider, args);
        }
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Assembly assembly, Type targetType, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(assembly, targetType);
            }
            else
            {
                type = typeName.GetTypeByTypeName(assembly, targetType, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Type targetType, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(targetType);
            }
            else
            {
                type = typeName.GetTypeByTypeName(targetType, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(args);
        }
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="targetType"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Assembly assembly, Type targetType, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(assembly, targetType);
            }
            else
            {
                type = typeName.GetTypeByTypeName(assembly, targetType, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(serviceProvider, args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="targetType"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, Type targetType, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName(targetType);
            }
            else
            {
                type = typeName.GetTypeByTypeName(targetType, args);
            }
            if (type is null) return null;
            return type.InstantiationOrDefault(serviceProvider, args);
        }
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, Assembly assembly, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName<T>(assembly);
            }
            else
            {
                type = typeName.GetTypeByTypeName<T>(assembly, args);
            }
            if (type is null) return default;
            return type.InstantiationOrDefault<T>(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName<T>();
            }
            else
            {
                type = typeName.GetTypeByTypeName<T>(args);
            }
            if (type is null) return default;
            return type.InstantiationOrDefault<T>(args);
        }
        /// <summary>
        /// 根据类型名称在指定程序集获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, Assembly assembly, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName<T>(assembly);
            }
            else
            {
                type = typeName.GetTypeByTypeName<T>(assembly, args);
            }
            if (type is null) return default;
            return type.InstantiationOrDefault<T>(serviceProvider, args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, IServiceProvider serviceProvider, object[]? args = null)
        {
            Type? type;
            if (args is null || args.Length == 0)
            {
                type = typeName.GetTypeByTypeName<T>();
            }
            else
            {
                type = typeName.GetTypeByTypeName<T>(args);
            }
            if (type is null) return default;
            return type.InstantiationOrDefault<T>(serviceProvider, args);
        }
        #endregion
    }
}
