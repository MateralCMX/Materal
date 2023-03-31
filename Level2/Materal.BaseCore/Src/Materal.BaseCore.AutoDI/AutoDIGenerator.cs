using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Materal.BaseCore.AutoDI
{
    [Generator]
    public class AutoDIGenerator : IIncrementalGenerator
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
            if (syntaxNode is not ClassDeclarationSyntax classDeclarationSyntax) return false;
            if (!classDeclarationSyntax.Identifier.ValueText.EndsWith("ServiceImpl") && !classDeclarationSyntax.Identifier.ValueText.EndsWith("Controller")) return false;
            return true;
        }
        private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            if (context.Node is not ClassDeclarationSyntax classDeclarationSyntax) return null;
            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol) continue;
                    INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    string fullName = attributeContainingTypeSymbol.ToDisplayString();
                    if (fullName == "Materal.BaseCore.CodeGenerator.AutoDIAttribute" || fullName == "Materal.BaseCore.CodeGenerator.AutoThisDIAttribute" || fullName == "Materal.BaseCore.CodeGenerator.AutoBaseDIAttribute") return classDeclarationSyntax;
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
            StringBuilder codeContent = new();
            #region 拼装using
            SyntaxNode topNode = classDeclarationSyntax;
            while (topNode.Parent != null)
            {
                topNode = topNode.Parent;
            }
            if (topNode is CompilationUnitSyntax compilationUnitSyntax)
            {
                foreach (UsingDirectiveSyntax usingDirectiveSyntax in compilationUnitSyntax.Usings)
                {
                    codeContent.AppendLine($"using {usingDirectiveSyntax.Name};");
                }
            }
            #endregion
            codeContent.AppendLine("");
            #region 拼装Namespace
            string namespaceName = namespaceDeclarationSyntax.Name.ToString();
            codeContent.AppendLine($"namespace {namespaceName}");
            codeContent.AppendLine("{");
            #endregion
            string className = classDeclarationSyntax.Identifier.ValueText;
            bool canThis = false;
            bool canBase = false;
            #region 拼装Class
            codeContent.AppendLine($"    public partial class {className}");
            codeContent.AppendLine("    {");
            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    string attributeName = attributeSyntax.Name.ToString();
                    if(attributeName == "AutoThisDI" || attributeName == "AutoThisDIAttribute" || attributeName == "Materal.BaseCore.CodeGenerator.AutoThisDI" || attributeName == "Materal.BaseCore.CodeGenerator.AutoThisDIAttribute")
                    {
                        canThis = true;
                    }
                    if (attributeName == "AutoBaseDI" || attributeName == "AutoBaseDIAttribute" || attributeName == "Materal.BaseCore.CodeGenerator.AutoBaseDI" || attributeName == "Materal.BaseCore.CodeGenerator.AutoBaseDIAttribute")
                    {
                        canBase = true;
                        break;
                    }
                }
            }
            #endregion
            List<string> args = new();
            List<string> values = new();
            foreach (MemberDeclarationSyntax memberDeclarationSyntax in classDeclarationSyntax.Members)
            {
                if (memberDeclarationSyntax is not FieldDeclarationSyntax fieldDeclarationSyntax) continue;
                string typeName = fieldDeclarationSyntax.Declaration.Type.ToString();
                if (typeName == "IServiceProvider") continue;
                string fieldName = fieldDeclarationSyntax.Declaration.Variables.First().Identifier.ValueText;
                if (!fieldName.StartsWith("_")) continue;
                string argName = fieldName.Substring(1);
                args.Add($"{typeName} {argName}");
                values.Add($"            {fieldName} = {argName};");
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            if (args.Count <= 0)
            {
                if (canThis)
                {
                    codeContent.AppendLine($"        public {className}(IServiceProvider serviceProvider) : this(serviceProvider)");
                }
                else if (canBase)
                {
                    codeContent.AppendLine($"        public {className}(IServiceProvider serviceProvider) : base(serviceProvider)");
                }
                else
                {
                    return;
                }
                codeContent.AppendLine("        {");
                codeContent.AppendLine("        }");
            }
            else
            {
                if (canThis)
                {
                    codeContent.AppendLine($"        public {className}(IServiceProvider serviceProvider, {string.Join(", ", args)}) : this(serviceProvider)");
                }
                else if(canBase)
                {
                    codeContent.AppendLine($"        public {className}(IServiceProvider serviceProvider, {string.Join(", ", args)}) : base(serviceProvider)");
                }
                else
                {
                    codeContent.AppendLine($"        public {className}({string.Join(", ", args)})");
                }
                codeContent.AppendLine("        {");
                foreach (string value in values)
                {
                    codeContent.AppendLine(value);
                }
                codeContent.AppendLine("        }");
            }
            codeContent.AppendLine("    }");
            codeContent.AppendLine("}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8); ;
            string fileName = $"{classDeclarationSyntax.Identifier.ValueText}.AutoDI.g.cs";
            context.AddSource(fileName, sourceText);
        }
    }
}
