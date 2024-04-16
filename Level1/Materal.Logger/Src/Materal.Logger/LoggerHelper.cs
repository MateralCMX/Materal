using System.Text.RegularExpressions;

namespace Materal.Logger
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static partial class LoggerHelper
    {

#if NET8_0_OR_GREATER
        /// <summary>
        /// 模版表达式
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"\$\{[^\}]+\}")]
        private static partial Regex ExpressionRegex();
#endif
        /// <summary>
        /// 应用文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ApplyText(string text, Dictionary<string, object?> data)
        {
            if (data.Count <= 0) return text;
            string result = text;
#if NETSTANDARD
            Regex regex = new(@"\$\{[^\}]+\}");
#else
            Regex regex = ExpressionRegex();
#endif
            MatchCollection matchCollection = regex.Matches(result);
            foreach (object? matchItem in matchCollection)
            {
                if (matchItem is not Match match) continue;
                string valueName = match.Value[2..^1];
                object? value = data.GetObjectValue(valueName);
                if (value is null) continue;
                else if (value is string strinvValue)
                {
                    result = result.Replace(match.Value, strinvValue);
                }
                else
                {
                    result = result.Replace(match.Value, value?.ToJson());
                }
            }
            return result;
        }
    }
}
