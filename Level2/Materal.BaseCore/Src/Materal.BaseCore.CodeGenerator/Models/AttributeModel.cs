using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class AttributeModel
    {
        public string Name { get; private set; }
        public List<AttributeArgumentModel> AttributeArguments { get; } = new List<AttributeArgumentModel>();
        public AttributeModel(string attributeName)
        {
            int leftbracketIndex = attributeName.IndexOf("(");
            if (leftbracketIndex > 0 && attributeName.EndsWith(")"))
            {
                Name = attributeName.Substring(0, leftbracketIndex);
                string argumentString = attributeName.Substring(leftbracketIndex + 1);
                argumentString = argumentString.Substring(0, argumentString.Length - 1);
                string[] arguments = argumentString.Trim().Split(',');
                List<string> trueArguments = arguments.AssemblyFullCode(",");
                AttributeArguments.AddRange(trueArguments.Select(item => new AttributeArgumentModel(item)));
            }
            else
            {
                Name = attributeName;
            }
        }
        public override string ToString()
        {
            StringBuilder codeContent = new();
            codeContent.Append(Name);
            if (AttributeArguments.Count > 0)
            {
                codeContent.Append("(");
                List<string> arguments = new();
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
