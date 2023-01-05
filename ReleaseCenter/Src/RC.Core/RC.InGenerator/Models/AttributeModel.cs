using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace RC.InGenerator.Models
{
    public class AttributeModel
    {
        public string Name { get; private set; }
        public List<AttributeArgumentModel> AttributeArguments { get; private set; }
        public AttributeModel(AttributeSyntax attributeSyntax)
        {
            Name = attributeSyntax.Name.ToString();
            AttributeArguments = new List<AttributeArgumentModel>();
            if (attributeSyntax.ArgumentList == null) return;
            foreach (AttributeArgumentSyntax argument in attributeSyntax.ArgumentList.Arguments)
            {
                AttributeArguments.Add(new AttributeArgumentModel(argument));
            }
        }
    }
}
