using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 接口模型
    /// </summary>
    public class InterfaceModel : CSharpCodeFileModel
    {
        /// <inheritdoc/>
        public override string Name { get; set; }
        /// <summary>
        /// 接口
        /// </summary>
        public List<string> Interfaces { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        public List<PropertyModel> Properties { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public List<MethodModel> Methods { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public InterfaceModel() : base()
        {
            Name = string.Empty;
            Interfaces = [];
            Properties = [];
            Methods = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="allNodes"></param>
        public InterfaceModel(InterfaceDeclarationSyntax node, IEnumerable<SyntaxNode> allNodes) : base(node, allNodes)
        {
            Name = GetName(node);
            Interfaces = GetInterfaces(node);
            Properties = GetProperties(node);
            Methods = GetMethods(node);
        }
        /// <inheritdoc/>
        private static string GetName(InterfaceDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<string> GetInterfaces(InterfaceDeclarationSyntax node)
        {
            List<string> result = [];
            if (node.BaseList is null) return result;
            foreach (BaseTypeSyntax baseType in node.BaseList.Types)
            {
                if (baseType.Type is not SimpleNameSyntax && baseType.Type is not QualifiedNameSyntax) continue;
                string interfaceName = baseType.Type.ToString();
                if (result.Contains(interfaceName)) continue;
                result.Add(interfaceName);
            }
            return result;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<PropertyModel> GetProperties(InterfaceDeclarationSyntax node)
        {
            List<PropertyModel> result = [];
            foreach (MemberDeclarationSyntax member in node.Members)
            {
                if (member is not PropertyDeclarationSyntax propertyNode) continue;
                PropertyModel item = new(propertyNode);
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 获取方法
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<MethodModel> GetMethods(InterfaceDeclarationSyntax node)
        {
            List<MethodModel> result = [];
            foreach (MemberDeclarationSyntax member in node.Members)
            {
                if (member is not MethodDeclarationSyntax methodNode) continue;
                MethodModel item = new(methodNode);
                result.Add(item);
            }
            return result;
        }
        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="other">要合并的模型</param>
        /// <returns>合并后的新模型</returns>
        public virtual InterfaceModel Merge(InterfaceModel other)
        {
            if (!IsSameModel(other)) throw new ArgumentException("不能合并不同的接口模型");
            Usings = [.. Usings, .. other.Usings.Where(m => !Usings.Any(n => n == m))];
            Attributes = [.. Attributes, .. other.Attributes.Where(m => !Attributes.Any(n => n.Name == m.Name))];
            Interfaces = [.. Interfaces, .. other.Interfaces.Where(m => !Interfaces.Contains(m))];
            Properties = [.. Properties, .. other.Properties.Where(m => !Properties.Any(n => n.Name == m.Name))];
            Methods = [.. Methods, .. other.Methods.Where(m => !Methods.Any(n => n.Name == m.Name))];
            Annotation = string.IsNullOrWhiteSpace(Annotation) ? other.Annotation : Annotation;
            return this;
        }
    }
}
