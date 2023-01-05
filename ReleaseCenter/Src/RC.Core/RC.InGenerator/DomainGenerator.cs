using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using RC.InGenerator.Models;

namespace RC.InGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class DomainGenerator : IIncrementalGenerator
    {
        private static bool IsGenerator = false;
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var typeDeclarations = context.SyntaxProvider.CreateSyntaxProvider(IsDomain, GetDomainClass).Where(static m => m is not null);
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
        private static bool IsDomain(SyntaxNode syntaxNode, CancellationToken token) => syntaxNode is ClassDeclarationSyntax && syntaxNode.Parent is NamespaceDeclarationSyntax @namespace && @namespace.Name.ToString().EndsWith(".Domain");
        /// <summary>
        /// 获得DomainClass
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static ClassDeclarationSyntax GetDomainClass(GeneratorSyntaxContext context, CancellationToken token) => (ClassDeclarationSyntax)context.Node;
        #endregion
        /// <summary>
        /// 生成代码
        /// </summary>
        private static void Execute(SourceProductionContext context, ImmutableArray<ClassDeclarationSyntax> classDeclarations)
        {
            if (IsGenerator) return;
            IsGenerator = true;
            if (classDeclarations.Length <= 0) return;
            if (classDeclarations[0].Parent is not NamespaceDeclarationSyntax namespaceDeclaration) return;
            ProjectModel project = new(context, namespaceDeclaration);
            foreach (ClassDeclarationSyntax classDeclaration in classDeclarations)
            {
                project.AddDomain(classDeclaration);
            }
            foreach (DomainModel domain in project.Domains)
            {
                domain.CreateFiles();
            }
            project.CreateDBContext();
            using (StreamReader sreamReader = new(Assembly.Load("RC.InGenerator").GetManifestResourceStream("RC.InGenerator.Templates.AuthorityTestController.txt")))
            {
                StringBuilder codeContent = new();
                while (sreamReader.Peek() != -1)
                {
                    codeContent.AppendLine(sreamReader.ReadLine());
                }
                SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
                context.AddSource($"AuthorityTestController.g.cs", sourceText);
            }
            IsGenerator = false;
        }
    }
}