using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using RC.InGenerator.Attribtues;
using RC.InGenerator.Utils;

namespace RC.InGenerator.Models
{
    public class EnumModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; private set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; private set; }
        /// <summary>
        /// 生成WebAPI
        /// </summary>
        public bool GeneratorWebAPI { get; private set; }
        public EnumModel(EnumDeclarationSyntax enumDeclaration)
        {
            Name = enumDeclaration.Identifier.ValueText;
            Annotation = enumDeclaration.GetAnnotation();
            Attributes = enumDeclaration.AttributeLists.GetAttributeSyntaxes();
            GeneratorWebAPI = !Attributes.HasAttribute<NotGeneratorAttribute>();
        }
    }
}
