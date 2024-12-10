using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 方法参数模型
    /// </summary>
    public class MethodArgumentModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 请求名称
        /// </summary>
        public string RequestName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestPredefinedType { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Initializer { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull => PredefinedType.EndsWith('?');
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MethodArgumentModel()
        {
            Name = string.Empty;
            RequestName = string.Empty;
            PredefinedType = string.Empty;
            RequestPredefinedType = string.Empty;
            Initializer = null;
            Attributes = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public MethodArgumentModel(ParameterSyntax node)
        {
            Name = GetName(node);
            PredefinedType = GetPredefinedType(node);
            Initializer = GetInitializer(node);
            Attributes = node.GetAttributes();
            (RequestName, RequestPredefinedType) = GetRequestInfo();
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(ParameterSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetPredefinedType(ParameterSyntax node)
            => node.Type?.ToString() ?? string.Empty;
        /// <summary>
        /// 获取请求信息
        /// </summary>
        /// <returns></returns>
        private (string, string) GetRequestInfo()
        {
            string requestPredefinedType = PredefinedType;
            string requestName = Name;
            if (PredefinedType.EndsWith("Model") && !PredefinedType.EndsWith("RequestModel"))
            {
                requestPredefinedType = PredefinedType[0..^5] + "RequestModel";
                requestName = $"request{Name.FirstUpper()}";
            }
            return (requestName, requestPredefinedType);
        }
        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? GetInitializer(ParameterSyntax node)
        {
            if (node.Default is null) return null;
            return node.Default.Value is LiteralExpressionSyntax literalExpression
                ? literalExpression.Token.ValueText
                : node.Default.Value.ToString();
        }
    }
}
