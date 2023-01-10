using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MateralBaseCoreVSIX.Models
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
            attributeCode = attributeCode.Substring(1, attributeCode.Length - 2);
            string[] temps = attributeCode.Split(',');
            List<string> attributeCodes = new List<string>();
            string tempCode = null;
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
        public static string GetAnnotation(this string[] codes, ref int startIndex)
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
                            result = result.Substring(3).Trim();
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
    }
}
