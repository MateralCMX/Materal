using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 服务接口模型
    /// </summary>

    public class IServiceModel : InterfaceModel
    {
        /// <summary>
        /// 领域名称
        /// </summary>
        public string DomainName => Name[1..^7];
        /// <summary>
        /// 是否有Mapper方法
        /// </summary>
        public bool HasMapperMethod => Methods.Count > 0 && Methods.Any(m => m.Attributes.HasAttribute<MapperControllerAttribute>());
        /// <summary>
        /// 构造方法
        /// </summary>
        public IServiceModel() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="allNodes"></param>
        public IServiceModel(InterfaceDeclarationSyntax node, IEnumerable<SyntaxNode> allNodes) : base(node, allNodes)
        {
        }
    }
}
