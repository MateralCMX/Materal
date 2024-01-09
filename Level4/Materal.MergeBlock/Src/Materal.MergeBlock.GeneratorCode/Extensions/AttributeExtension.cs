using Materal.MergeBlock.GeneratorCode.Models;

namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    /// <summary>
    /// 特性扩展
    /// </summary>
    public static class AttributeExtension
    {
        /// <summary>
        /// 获得代码
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static string? GetCode(this List<AttributeModel> attributes)
        {
            if (attributes.Count <= 0) return null;
            List<string> attributeCodes = [];
            foreach (AttributeModel attribute in attributes)
            {
                attributeCodes.Add(attribute.ToString());
            }
            string code = $"[{string.Join(", ", attributeCodes)}]";
            return code;
        }
    }
}
