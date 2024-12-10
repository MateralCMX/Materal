using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// C#代码文件模型
    /// </summary>
    public abstract class CSharpCodeFileModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public CSharpCodeFileModel()
        {
            Usings = [];
            Namespace = null;
            Annotation = null;
            Attributes = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="allNodes"></param>
        public CSharpCodeFileModel(SyntaxNode node, IEnumerable<SyntaxNode> allNodes)
        {
            Usings = GetUsings(allNodes);
            Namespace = GetNamespace(allNodes);
            Annotation = node.GetAnnotation();
            Attributes = node.GetAttributes();
        }
        /// <summary>
        /// 获取引用
        /// </summary>
        /// <param name="allNodes"></param>
        private static List<string> GetUsings(IEnumerable<SyntaxNode> allNodes)
        {
            List<string> result = [];
            IEnumerable<UsingDirectiveSyntax> usingDirectives = allNodes.OfType<UsingDirectiveSyntax>();
            foreach (UsingDirectiveSyntax usingDirective in usingDirectives)
            {
                if (usingDirective.Name is null) continue;
                string usingName = usingDirective.Name.ToString();
                if (result.Contains(usingName)) continue;
                result.Add(usingName);
            }
            return result;
        }
        /// <summary>
        /// 获取命名空间
        /// </summary>
        /// <param name="allNodes"></param>
        /// <returns></returns>
        private static string? GetNamespace(IEnumerable<SyntaxNode> allNodes)
        {
            NamespaceDeclarationSyntax? namespaceNode = allNodes.OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
            if (namespaceNode is not null)
            {
                return namespaceNode.Name.ToString();
            }
            else
            {
                FileScopedNamespaceDeclarationSyntax? fileScopedNamespaceNode = allNodes.OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
                if (fileScopedNamespaceNode is not null)
                {
                    return fileScopedNamespaceNode.Name.ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// 比较是否为相同模型
        /// </summary>
        /// <param name="other">要比较的模型</param>
        /// <returns></returns>
        public bool IsSameModel(CSharpCodeFileModel? other)
        {
            if (other is null) return false;
            if (string.IsNullOrWhiteSpace(Namespace) && string.IsNullOrWhiteSpace(other.Namespace))
            {
                return Name == other.Name;
            }
            return Namespace == other.Namespace && Name == other.Name;
        }
    }
}
