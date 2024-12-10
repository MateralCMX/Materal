using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 特性模型
    /// </summary>
    public class AttributeModel
    {
        /// <summary>
        /// 特性名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 特性参数列表
        /// </summary>
        public List<AttributeArgumentModel> AttributeArguments { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public AttributeModel()
        {
            Name = string.Empty;
            AttributeArguments = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public AttributeModel(AttributeSyntax node)
        {
            Name = GetName(node);
            AttributeArguments = GetAttributeArguments(node);
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(AttributeSyntax node)
            => node.Name.ToString();
        /// <summary>
        /// 获取特性参数列表
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<AttributeArgumentModel> GetAttributeArguments(AttributeSyntax node)
        {
            List<AttributeArgumentModel> result = [];
            if (node.ArgumentList is null) return result;
            foreach (AttributeArgumentSyntax argumentNode in node.ArgumentList.Arguments)
            {
                AttributeArgumentModel item = new(argumentNode);
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder codeContent = new();
            codeContent.Append(Name);
            if (AttributeArguments.Count > 0)
            {
                codeContent.Append('(');
                List<string> arguments = [];
                for (int i = 0; i < AttributeArguments.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(AttributeArguments[i].Target))
                    {
                        arguments.Add(AttributeArguments[i].Value);
                    }
                    else
                    {
                        arguments.Add($"{AttributeArguments[i].Target} = {AttributeArguments[i].Value}");
                    }
                }
                codeContent.Append(string.Join(", ", arguments));
                codeContent.Append(')');
            }
            return codeContent.ToString();
        }
    }
}
