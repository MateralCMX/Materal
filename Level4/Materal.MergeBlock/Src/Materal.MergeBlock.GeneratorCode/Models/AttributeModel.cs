using Materal.MergeBlock.GeneratorCode.Extensions;
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
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 特性参数列表
        /// </summary>
        public List<AttributeArgumentModel> AttributeArguments { get; set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="code"></param>
        public AttributeModel(string code)
        {
            int leftbracketIndex = code.IndexOf("(");
            if (leftbracketIndex > 0 && code.EndsWith(")"))
            {
                Name = code[..leftbracketIndex];
                string argumentString = code[(leftbracketIndex + 1)..^1];
                string[] arguments = argumentString.Trim().Split(',');
                List<string> trueArguments = arguments.AssemblyFullCode(",");
                AttributeArguments.AddRange(trueArguments.Select(item => new AttributeArgumentModel(item)));
            }
            else
            {
                Name = code;
            }
        }
        /// <summary>
        /// 获取特性列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static List<AttributeModel> GetAttributes(string code)
        {
            List<AttributeModel> result = [];
            string attributeCode = code[1..^1];
            string[] attributeCodes = attributeCode.Split(',');
            attributeCode = string.Empty;
            foreach (string item in attributeCodes)
            {
                if(attributeCode is null || string.IsNullOrWhiteSpace(attributeCode))
                {
                    attributeCode = item.Trim();
                }
                else
                {
                    attributeCode += $", {item}".Trim();
                }
                if (attributeCode.Count(m => m == '(') != attributeCode.Count(m => m == ')')) continue;
                result.Add(new AttributeModel(attributeCode));
                attributeCode = string.Empty;
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
                codeContent.Append("(");
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
                codeContent.Append(")");
            }
            return codeContent.ToString();
        }
    }
}
