using Materal.BaseCore.CodeGenerator.Extensions;

namespace Materal.BaseCore.CodeGenerator.Models
{
    /// <summary>
    /// 枚举模型
    /// </summary>
    public class EnumModel
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; } = string.Empty;
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 生成代码
        /// </summary>
        public bool GeneratorCode { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();
        public EnumModel() { }
        /// <summary>
        /// 枚举模型
        /// </summary>
        /// <param name="fileName"></param>
        public EnumModel(string[] codes, int classLineIndex)
        {
            #region 解析Class
            {
                int startIndex = classLineIndex;
                #region 解析名称
                if (startIndex < 0 || startIndex >= codes.Length) throw new CodeGeneratorException("类行位序错误");
                const string classTag = " enum ";
                Name = codes[startIndex];
                int classIndex = Name.IndexOf(classTag);
                if (classIndex <= 0) throw new CodeGeneratorException("模型不是枚举");
                Name = Name.Substring(classIndex + classTag.Length);
                Name = Name.Split(':')[0].Trim();
                #endregion
                startIndex -= 1;
                #region 解析特性
                do
                {
                    if (startIndex < 0) break;
                    string attributeCode = codes[startIndex].Trim();
                    if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                    startIndex -= 1;
                    List<string> attributeCodes = attributeCode.GetAttributeCodes();
                    Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
                } while (true);
                GeneratorCode = !Attributes.HasAttribute<NotGeneratorAttribute>();
                #endregion
                #region 解析注释
                Annotation = codes.GetAnnotation(ref startIndex);
                #endregion
                #region 解析命名空间
                string nameSpaceCode = codes[startIndex].Trim();
                while (!nameSpaceCode.StartsWith("namespace ") && startIndex >= 0)
                {
                    nameSpaceCode = codes[--startIndex].Trim();
                }
                Namespace = nameSpaceCode.Substring("namespace ".Length);
                #endregion
            }
            #endregion
        }
    }
}
