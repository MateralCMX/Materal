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
    }
}
