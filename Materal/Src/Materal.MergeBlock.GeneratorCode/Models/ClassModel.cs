using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 类模型
    /// </summary>
    public class ClassModel : CSharpCodeFileModel
    {
        /// <inheritdoc/>
        public override string Name { get; set; }
        /// <summary>
        /// 父类
        /// </summary>
        public string? BaseClass { get; set; }
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
        public ClassModel() : base()
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
        public ClassModel(ClassDeclarationSyntax node, IEnumerable<SyntaxNode> allNodes) : base(node, allNodes)
        {
            Name = GetName(node);
            BaseClass = GetBaseClass(node);
            Interfaces = GetInterfaces(node);
            Properties = GetProperties(node);
            Methods = GetMethods(node);
        }
        /// <inheritdoc/>
        private static string GetName(ClassDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取基类
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? GetBaseClass(ClassDeclarationSyntax node)
        {
            if (node.BaseList is null) return null;
            BaseTypeSyntax? baseType = node.BaseList.Types.FirstOrDefault();
            if (baseType is null || (baseType.Type is not SimpleNameSyntax && baseType.Type is not QualifiedNameSyntax)) return null;
            string typeName = baseType.Type.ToString();
            // 如果类型名以I开头且第二个字母是大写，通常是接口
            if (typeName.Length >= 2 && typeName[0] == 'I' && char.IsUpper(typeName[1])) return null;
            return typeName;
        }
        /// <summary>
        /// 获取接口
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> GetInterfaces(ClassDeclarationSyntax node)
        {
            List<string> result = [];
            if (node.BaseList is null) return result;
            int startIndex = string.IsNullOrWhiteSpace(BaseClass) ? 0 : 1;
            for (int i = startIndex; i < node.BaseList.Types.Count; i++)
            {
                BaseTypeSyntax baseType = node.BaseList.Types[i];
                if (baseType.Type is not SimpleNameSyntax && baseType.Type is not QualifiedNameSyntax) continue;
                string typeName = baseType.Type.ToString();
                if (result.Contains(typeName)) continue;
                result.Add(typeName);
            }
            return result;
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static List<PropertyModel> GetProperties(ClassDeclarationSyntax node)
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
        private static List<MethodModel> GetMethods(ClassDeclarationSyntax node)
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
        public virtual ClassModel Merge(ClassModel other)
        {
            if (!IsSameModel(other)) throw new ArgumentException("不能合并不同的类模型");
            BaseClass = BaseClass ?? other.BaseClass;
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
