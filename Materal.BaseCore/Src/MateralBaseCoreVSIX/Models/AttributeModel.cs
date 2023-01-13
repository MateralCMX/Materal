using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MateralBaseCoreVSIX.Models
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
                List<string> trueArguments = new List<string>();
                string tempArg = null;
                foreach (string arg in arguments)
                {
                    if (string.IsNullOrWhiteSpace(tempArg))
                    {
                        tempArg = arg;
                    }
                    else
                    {
                        tempArg += $",{arg}";
                    }
                    if (tempArg.Count(m => m == '"') % 2 != 0) continue;
                    int left = tempArg.Count(m => m == '(');
                    int right = tempArg.Count(m => m == ')');
                    if (left != right) continue;
                    left = tempArg.Count(m => m == '<');
                    right = tempArg.Count(m => m == '>');
                    if (left != right) continue;
                    left = tempArg.Count(m => m == '[');
                    right = tempArg.Count(m => m == ']');
                    if (left != right) continue;
                    trueArguments.Add(tempArg);
                    tempArg = null;
                }
                AttributeArguments.AddRange(trueArguments.Select(item => new AttributeArgumentModel(item)));
            }
            else
            {
                Name = attributeName;
            }
        }
        public override string ToString()
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.Append(Name);
            if (AttributeArguments.Count > 0)
            {
                codeContent.Append("(");
                List<string> arguments = new List<string>();
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
