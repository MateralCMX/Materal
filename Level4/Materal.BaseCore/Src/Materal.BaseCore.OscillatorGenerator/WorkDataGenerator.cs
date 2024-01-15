using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Materal.BaseCore.OscillatorGenerator
{
    [Generator]
    public class WorkDataGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<ClassDeclarationSyntax> serviceImplDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(static (s, _) => IsSyntaxTargetForGeneration(s), static (ctx, _) => GetSemanticTargetForGeneration(ctx))
                .Where(static m => m != null)!;
            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndClass
                = context.CompilationProvider.Combine(serviceImplDeclarations.Collect());
            context.RegisterSourceOutput(compilationAndClass,
                static (spc, source) => Execute(source.Item2, spc));
        }
        public static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not ClassDeclarationSyntax) return false;
            return true;
        }
        private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax) return null;
            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    string fullName = attributeSyntax.Name.ToString();
                    if (fullName == "AutoWorkData" || fullName == "AutoWorkDataAttribute" || fullName == "Materal.BaseCore.CodeGenerator.AutoWorkData" || fullName == "Materal.BaseCore.CodeGenerator.AutoWorkDataAttribute")
                    {
                        return classDeclarationSyntax;
                    }
                }
            }
            return null;
        }
        private static void Execute(ImmutableArray<ClassDeclarationSyntax> classList, SourceProductionContext context)
        {
            if (classList.IsDefaultOrEmpty) return;
            IEnumerable<ClassDeclarationSyntax> distinctClassList = classList.Distinct();
            foreach (ClassDeclarationSyntax classDeclarationSyntax in distinctClassList)
            {
                HandlerClass(classDeclarationSyntax, context);
            }

        }
        private static void HandlerClass(ClassDeclarationSyntax classDeclarationSyntax, SourceProductionContext context)
        {
            if (classDeclarationSyntax.Members.Count <= 0) return;
            if (classDeclarationSyntax.Parent is not NamespaceDeclarationSyntax namespaceDeclarationSyntax) return;
            string namespaceName = namespaceDeclarationSyntax.Name.ToString();
            int lastPointIndex = namespaceName.LastIndexOf('.');
            namespaceName = namespaceName.Substring(0, lastPointIndex) + ".Works";
            string className = classDeclarationSyntax.Identifier.ValueText;
            if (className.EndsWith("Work"))
            {
                className = className.Substring(0, className.Length - 4);
            }
            else if (className.EndsWith("Schedule"))
            {
                className = className.Substring(0, className.Length - 8);
            }
            else
            {
                return;
            }
            className += "WorkData";
            #region 拼装代码
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.Oscillator.Abstractions.Works;");
            codeContent.AppendLine("");
            codeContent.AppendLine($"namespace {namespaceName}");
            codeContent.AppendLine("{");
            codeContent.AppendLine($"    public partial class {className} : BaseWorkData, IWorkData");
            codeContent.AppendLine("    {");
            codeContent.AppendLine("    }");
            codeContent.AppendLine("}");
            #endregion
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            string fileName = $"{className}.g.cs";
            context.AddSource(fileName, sourceText);
        }
    }
}
