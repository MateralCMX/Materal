using Materal.MergeBlock.GeneratorCode.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Extensions
{
    internal static class SyntaxNodeExtension
    {
        /// <summary>
        /// 注释黑名单
        /// </summary>
        private static string[] _annotationBlackStringList = [
            "///",
            "\r\n",
            "\n",
            "\r",
            " "
        ];
        /// <summary>
        /// 获取注释
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string? GetAnnotation(this SyntaxNode node)
        {
            SyntaxTriviaList trivia = node.GetLeadingTrivia();
            foreach (SyntaxTrivia triviaNode in trivia)
            {
                if (!triviaNode.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.SingleLineDocumentationCommentTrivia) && !triviaNode.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.MultiLineDocumentationCommentTrivia)) continue;
                SyntaxNode? commentXml = triviaNode.GetStructure();
                if (commentXml is null) continue;
                XmlElementSyntax? summaryNode = commentXml.DescendantNodes().OfType<XmlElementSyntax>().FirstOrDefault(x => x.StartTag.Name.ToString() == "summary");
                if (summaryNode == null) continue;
                string annotation = summaryNode.Content.ToString();
                foreach (string blackString in _annotationBlackStringList)
                {
                    annotation = annotation.Replace(blackString, string.Empty);
                }
                return annotation;
            }
            return null;
        }
        /// <summary>
        /// 获取特性
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<AttributeModel> GetAttributes(this SyntaxNode node)
        {
            List<AttributeModel> result = [];
            if (node is not MemberDeclarationSyntax memberNode) return result;
            foreach (AttributeListSyntax attributeList in memberNode.AttributeLists)
            {
                foreach (AttributeSyntax attribute in attributeList.Attributes)
                {
                    AttributeModel item = new(attribute);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
