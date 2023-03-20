using System.Text;

namespace Materal.BaseCore.CodeGenerator.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 获得特性代码组
        /// </summary>
        /// <param name="attributeCode"></param>
        /// <returns></returns>
        public static List<string> GetAttributeCodes(this string attributeCode)
        {
            attributeCode = attributeCode[1..^1];
            string[] temps = attributeCode.Split(',');
            List<string> attributeCodes = new();
            string? tempCode = null;
            foreach (string item in temps)
            {
                string temp = item.Trim();
                if (tempCode == null)
                {
                    if (temp.Contains("("))
                    {
                        if (temp.EndsWith(")"))
                        {
                            attributeCodes.Add(temp);
                        }
                        else
                        {
                            tempCode = temp;
                        }
                    }
                    else
                    {
                        attributeCodes.Add(temp);
                    }
                }
                else
                {
                    tempCode += ", " + temp;
                    if (temp.EndsWith(")") && temp.Count(m => m == '(') != temp.Count(m => m == ')'))
                    {
                        attributeCodes.Add(tempCode);
                        tempCode = null;
                    }
                }
            }
            return attributeCodes;
        }
        /// <summary>
        /// 获得注释
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string? GetAnnotation(this string[] codes, ref int startIndex)
        {
            string annotationCode = codes[startIndex].Trim();
            if (annotationCode == "/// </summary>")
            {
                do
                {
                    annotationCode = codes[--startIndex].Trim();
                    if (annotationCode == "/// <summary>")
                    {
                        string result = codes[startIndex + 1].Trim();
                        if (result.StartsWith("/// "))
                        {
                            result = result[3..].Trim();
                        }
                        startIndex -= 1;
                        return result;
                    }
                } while (startIndex >= 0);
            }
            return null;
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static string SaveFile(this StringBuilder content, string path, string fileName) => content.ToString().SaveFile(path, fileName);
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static string SaveFile(this string content, string path, string fileName)
        {
            string outputPath = path;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            outputPath = Path.Combine(outputPath, fileName);
            File.WriteAllText(outputPath, content);
            return outputPath;
        }
        /// <summary>
        /// 是完整代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsFullCode(this string code)
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
            List<string> trueCodes = new();
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
                if (!tempArg.IsFullCode()) continue;
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
                packags = new[]
                {
                    new []{"\"","\""},
                    new []{"'","'"},
                    new []{"(",")"},
                    new []{"[","]"},
                    new []{"{","}"}
                };
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
