using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 控制器接口模型
    /// </summary>

    public class IControllerModel : InterfaceModel
    {
        /// <summary>
        /// 领域名称
        /// </summary>
        public string DomainName => Name[1..^10];
        /// <summary>
        /// 构造方法
        /// </summary>
        public IControllerModel() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="allNodes"></param>
        public IControllerModel(InterfaceDeclarationSyntax node, IEnumerable<SyntaxNode> allNodes) : base(node, allNodes)
        {
        }
    }
}
