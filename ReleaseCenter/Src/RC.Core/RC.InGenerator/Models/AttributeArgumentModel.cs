using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RC.InGenerator.Models
{
    public class AttributeArgumentModel
    {
        /// <summary>
        /// 目标
        /// </summary>
        public string? Target { get; private set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; } = string.Empty;
        public AttributeArgumentModel(AttributeArgumentSyntax argument)
        {
            string[] temp = argument.ToString().Split('=');
            if (temp.Length == 1)
            {
                Value = HandlerStringValue(temp[0].Trim());
            }
            else if (temp.Length == 2)
            {
                Target = temp[0].Trim();
                Value = HandlerStringValue(temp[1].Trim());
            }
        }
        private string HandlerStringValue(string value)
        {
            if(value.StartsWith("nameof(") && value.EndsWith(")"))
            {
                value = value.Substring("nameof(".Length);
                value = value.Substring(0, value.Length - 1);
            }
            return value;
        }
    }
}
