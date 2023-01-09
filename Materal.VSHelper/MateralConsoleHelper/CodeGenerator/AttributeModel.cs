using System.Collections.Generic;
using System.Linq;

namespace MateralVSHelper.CodeGenerator
{
    public class AttributeModel
    {
        public string Name { get; private set; }
        public List<AttributeArgumentModel> AttributeArguments { get; } = new List<AttributeArgumentModel>();
        public AttributeModel(string attributeName)
        {
            int leftbracketIndex = attributeName.IndexOf("(");
            if(leftbracketIndex > 0 && attributeName.EndsWith(")"))
            {
                Name = attributeName.Substring(0, leftbracketIndex);
                string argumentString = attributeName.Substring(leftbracketIndex + 1);
                argumentString = argumentString.Substring(0, argumentString.Length - 1);
                string[] arguments = argumentString.Trim().Split(',');
                AttributeArguments.AddRange(arguments.Select(item => new AttributeArgumentModel(item)));
            }
            else
            {
                Name = attributeName;
            }
        }
    }
}
