using System.Collections.Generic;
using System.Linq;

namespace MateralVSHelper.CodeGenerator
{
    public class DomainModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; } = new List<string>();
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; } = new List<AttributeModel>();
        /// <summary>
        /// 属性
        /// </summary>
        public List<DomainPropertyModel> Properties { get; } = new List<DomainPropertyModel>();
        public DomainModel(string[] codes, int classLineIndex)
        {
            #region 解析Class
            {
                int startIndex = classLineIndex;
                #region 解析名称
                if(startIndex < 0 || startIndex >= codes.Length) throw new VSHelperException("类行位序错误");
                const string classTag = " class ";
                Name = codes[startIndex];
                int classIndex = Name.IndexOf(classTag);
                if (classIndex <= 0) throw new VSHelperException("模型不是类");
                Name = Name.Substring(classIndex + classTag.Length);
                int domainIndex = Name.IndexOf(" : BaseDomain, IDomain");
                if (domainIndex <= 0) throw new VSHelperException("模型不是Domain");
                Name = Name.Substring(0, domainIndex);
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
                NameSpace = nameSpaceCode.Substring("namespace ".Length);
                #endregion
                #region 解析引用
                for (int i = 0; i < startIndex; i++)
                {
                    string usingCode = codes[i].Trim();
                    if (usingCode.StartsWith("using "))
                    {
                        Usings.Add(usingCode);
                    }
                }
                #endregion
            }
            #endregion
            #region 解析属性
            {
                for (int i = classLineIndex; i < codes.Length; i++)
                {
                    string propertyCode = codes[i].Trim();
                    if (!propertyCode.StartsWith("public ")) continue;
                    int getSetIndex = propertyCode.IndexOf("{ get; set; }");
                    if (getSetIndex <= 0) continue;
                    Properties.Add(new DomainPropertyModel(codes, i));
                }
            }
            #endregion
        }
    }
}
