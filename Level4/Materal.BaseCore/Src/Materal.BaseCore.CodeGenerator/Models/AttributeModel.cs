using Materal.BaseCore.CodeGenerator.Extensions;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class AttributeModel
    {
        public string Name { get; set; } = string.Empty;
        public List<AttributeArgumentModel> AttributeArguments { get; set; } = new List<AttributeArgumentModel>();
        public AttributeModel() { }
        public AttributeModel(string attributeName)
        {
            int leftbracketIndex = attributeName.IndexOf("(");
            if (leftbracketIndex > 0 && attributeName.EndsWith(")"))
            {
                Name = attributeName[..leftbracketIndex];
                string argumentString = attributeName[(leftbracketIndex + 1)..];
                argumentString = argumentString[..^1];
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
