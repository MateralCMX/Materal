using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 枚举模型
    /// </summary>
    public class EnumModel : CSharpCodeFileModel
    {
        /// <inheritdoc/>
        public override string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public List<EnumValueModel> Values { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnumModel() : base()
        {
            Name = string.Empty;
            Values = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        public EnumModel(EnumDeclarationSyntax node, IEnumerable<SyntaxNode> nodes) : base(node, nodes)
        {
            Name = GetName(node);
            Values = GetValues(node);
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(EnumDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<EnumValueModel> GetValues(EnumDeclarationSyntax node)
        {
            List<EnumValueModel> result = [];
            foreach (EnumMemberDeclarationSyntax member in node.Members)
            {
                EnumValueModel item = new(member);
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="other">要合并的模型</param>
        /// <returns>合并后的新模型</returns>
        public virtual EnumModel Merge(EnumModel other)
        {
            if (!IsSameModel(other)) throw new ArgumentException("不能合并不同的枚举模型");
            Usings = [.. Usings, .. other.Usings.Where(m => !Usings.Any(n => n == m))];
            Attributes = [.. Attributes, .. other.Attributes.Where(m => !Attributes.Any(n => n.Name == m.Name))];
            Annotation = string.IsNullOrWhiteSpace(Annotation) ? other.Annotation : Annotation;
            Values = [.. Values, .. other.Values.Where(m => !Values.Any(n => n.Name == n.Name))];
            return this;
        }
    }
}
