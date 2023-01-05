using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using RC.InGenerator.Models;

namespace RC.InGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class EnumGenerator : IIncrementalGenerator
    {
        private static bool IsGenerator = false;
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var typeDeclarations = context.SyntaxProvider.CreateSyntaxProvider(IsEnum, GetEnumClass).Where(static m => m is not null);
            var compilationAndTypes = context.CompilationProvider.Combine(typeDeclarations.Collect());
            context.RegisterSourceOutput(compilationAndTypes, static (spc, source) => Execute(spc, source.Right));
        }
        #region 筛选器
        /// <summary>
        /// 是Domain
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static bool IsEnum(SyntaxNode syntaxNode, CancellationToken token) => syntaxNode is EnumDeclarationSyntax && syntaxNode.Parent is NamespaceDeclarationSyntax @namespace && @namespace.Name.ToString().EndsWith(".Enums");
        /// <summary>
        /// 获得DomainClass
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static EnumDeclarationSyntax GetEnumClass(GeneratorSyntaxContext context, CancellationToken token) => (EnumDeclarationSyntax)context.Node;
        #endregion
        /// <summary>
        /// 生成代码
        /// </summary>
        private static void Execute(SourceProductionContext context, ImmutableArray<EnumDeclarationSyntax> classDeclarations)
        {
            if (IsGenerator) return;
            IsGenerator = true;
            if (classDeclarations.Length <= 0) return;
            if (classDeclarations[0].Parent is not NamespaceDeclarationSyntax namespaceDeclaration) return;
            ProjectModel project = new(context, namespaceDeclaration);
            foreach (EnumDeclarationSyntax enumDeclaration in classDeclarations)
            {
                project.AddEnum(enumDeclaration);
            }
            project.CreateEnumsController();
            IsGenerator = false;
        }
    }
}
