namespace Materal.Extensions
{
    /// <summary>
    /// 字符串转行扩展类
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
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByTypeName(this string typeName, params object[] args)
        {
            Type? type = typeName.GetTypeByTypeName(args);
            if (type is null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByTypeName<T>(this string typeName, params object[] args)
        {
            Type? type = typeName.GetTypeByTypeName<T>(args);
            if (type is null) return default;
            object? typeObject = type.Instantiation(args);
            if (typeObject is null || typeObject is not T result) return default;
            return result;
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="parentType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object? GetObjectByParentType(this string typeName, Type parentType, params object[] args)
        {
            Type[] argTypes = args.Select(m => m.GetType()).ToArray();
            Type? type = typeName.GetTypeByParentType(parentType, argTypes);
            if (type is null) return null;
            return type.Instantiation(args);
        }
        /// <summary>
        /// 根据类型名称获得对象
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T? GetObjectByParentType<T>(this string typeName, params object[] args)
        {
            object? obj = typeName.GetObjectByParentType(parentType: typeof(T), args);
            if (obj is null || obj is not T result) return default;
            return result;
        }
    }
}
