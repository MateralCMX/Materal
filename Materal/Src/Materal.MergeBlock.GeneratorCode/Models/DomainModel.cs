using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 领域模型
    /// </summary>
    public class DomainModel : ClassModel
    {
        /// <summary>
        /// 是树形领域
        /// </summary>
        public bool IsTreeDomain => Interfaces.Contains("ITreeDomain");
        /// <summary>
        /// 获取树形领域的分组属性
        /// </summary>
        /// <returns></returns>
        public PropertyModel? GetTreeGroupProperty() => Properties.FirstOrDefault(m => m.HasAttribute<TreeGroupAttribute>());
        /// <summary>
        /// 是位序领域
        /// </summary>
        public bool IsIndexDomain => Interfaces.Contains("IIndexDomain");
        /// <summary>
        /// 获取树形领域的分组属性
        /// </summary>
        /// <returns></returns>
        public PropertyModel? GetIndexGroupProperty() => Properties.FirstOrDefault(m => m.HasAttribute<IndexGroupAttribute>());
        /// <summary>
        /// 是否为视图
        /// </summary>
        public bool IsView => Attributes.HasAttribute<ViewAttribute>();
        /// <summary>
        /// 构造方法
        /// </summary>
        public DomainModel() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="allNodes"></param>
        public DomainModel(ClassDeclarationSyntax node, IEnumerable<SyntaxNode> allNodes) : base(node, allNodes)
        {
        }
    }
}
