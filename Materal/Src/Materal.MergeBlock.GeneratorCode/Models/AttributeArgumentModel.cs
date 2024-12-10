using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 特性参数模型
    /// </summary>
    public class AttributeArgumentModel
    {
        /// <summary>
        /// 目标
        /// </summary>
        public string? Target { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public AttributeArgumentModel()
        {
            Target = null;
            Value = string.Empty;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public AttributeArgumentModel(AttributeArgumentSyntax node)
        {
            Target = GetTarget(node);
            Value = GetValue(node);
        }
        /// <summary>
        /// 获取目标
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? GetTarget(AttributeArgumentSyntax node)
            => node.NameEquals?.Name.ToString();
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetValue(AttributeArgumentSyntax node)
        {
            string result;
            if (node.Expression is LiteralExpressionSyntax literalExpression)
            {
                SyntaxKind kind = literalExpression.Kind();
                if (kind == SyntaxKind.StringLiteralExpression)
                {
                    result = $"\"{literalExpression.Token.ValueText}\"";
                }
                else
                {
                    result = literalExpression.Token.Value?.ToString() ?? string.Empty;
                }
            }
            else
            {
                result = node.Expression.ToString();
            }
            result = GetNameofValue(result);
            return result;
        }

        /// <summary>
        /// 获取nameof表达式的值
        /// </summary>
        /// <param name="nameofExpression">nameof表达式</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static string GetNameofValue(string nameofExpression)
        {
            if (!nameofExpression.StartsWith("nameof(") || !nameofExpression.EndsWith(')')) return nameofExpression;
            // 移除 nameof( 和最后的 )
            string result = nameofExpression[7..^1].Trim();
            // 验证括号内的表达式不为空
            if (string.IsNullOrWhiteSpace(result)) throw new ArgumentException("nameof表达式不能为空");
            // 验证表达式中不包含无效字符
            if (result.Contains('(') || result.Contains(')')) throw new ArgumentException("nameof表达式中包含无效字符");
            // 处理可能的多个点号
            var parts = result.Split('.');
            if (parts.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("nameof表达式格式无效");
            // 获取最后一个有效标识符
            result = parts[^1].Trim();
            // 验证标识符的有效性
            if (!result.All(c => char.IsLetterOrDigit(c) || c == '_')) throw new ArgumentException("nameof表达式中包含无效的标识符");
            return $"\"{result}\"";
        }
    }
}
