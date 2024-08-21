namespace Materal.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class StringExtensions
    {
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
