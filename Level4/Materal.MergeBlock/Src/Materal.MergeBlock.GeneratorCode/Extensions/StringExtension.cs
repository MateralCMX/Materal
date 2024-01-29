namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 是完整代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsFullCodeBlock(this string code)
        {
            if (code.Count(m => m == '"') % 2 != 0) return false;
            int left = code.Count(m => m == '(');
            int right = code.Count(m => m == ')');
            if (left != right) return false;
            left = code.Count(m => m == '<');
            right = code.Count(m => m == '>');
            if (left != right) return false;
            left = code.Count(m => m == '[');
            right = code.Count(m => m == ']');
            if (left != right) return false;
            return true;
        }
        /// <summary>
        /// 拼装完整代码
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="linkString"></param>
        /// <returns></returns>
        public static List<string> AssemblyFullCode(this string[] codes, string linkString)
        {
            List<string> trueCodes = [];
            string? tempArg = null;
            foreach (string arg in codes)
            {
                if (string.IsNullOrWhiteSpace(tempArg))
                {
                    tempArg = arg;
                }
                else
                {
                    tempArg += $"{linkString}{arg}";
                }
                if (!tempArg.IsFullCodeBlock()) continue;
                trueCodes.Add(tempArg);
                tempArg = null;
            }
            return trueCodes;
        }
        /// <summary>
        /// 移除包装
        /// 默认移除"" ''() [] {}
        /// </summary>
        /// <param name="code"></param>
        /// <param name="packags"></param>
        /// <returns></returns>
        public static string RemovePackag(this string code, params string[][] packags)
        {
            if (packags == null || packags.Length <= 0)
            {
                packags =
                [
                    ["\"", "\""],
                    ["'", "'"],
                    ["(", ")"],
                    ["[", "]"],
                    ["{", "}"]
                ];
            }
            string result = code;
            foreach (string[] package in packags)
            {
                if (package.Length != 2) continue;
                if (result.StartsWith(package[0]) && result.EndsWith(package[1]))
                {
                    result = result[package[0].Length..];
                    result = result[..^package[1].Length];
                }
            }
            return result;
        }
    }
}
