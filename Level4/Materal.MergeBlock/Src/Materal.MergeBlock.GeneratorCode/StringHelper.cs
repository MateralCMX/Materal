using Materal.MergeBlock.GeneratorCode.Models;

namespace Materal.MergeBlock.GeneratorCode
{
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 获得注释并设置特性
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="currentLine"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string GetAnnotationSetAttributes(string[] codes, int currentLine, List<AttributeModel> attributes)
        {
            string annotation = string.Empty;
            int index = currentLine - 1;
            while (index >= 0)
            {
                string upCode = codes[index].Trim();
                if (upCode.StartsWith("["))
                {
                    attributes.AddRange(AttributeModel.GetAttributes(upCode));
                }
                else if (upCode.StartsWith("/// </summary>"))
                {
                    while (index >= 0)
                    {
                        upCode = codes[index].Trim();
                        if (upCode.StartsWith("/// <summary>"))
                        {
                            if (index + 1 >= codes.Length) break;
                            upCode = codes[index + 1].Trim();
                            annotation = upCode[4..];
                            break;
                        }
                        index -= 1;
                    }
                    break;
                }
                index -= 1;
            }
            return annotation;
        }
    }
}
