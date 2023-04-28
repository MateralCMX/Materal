namespace Materal.BusinessFlow.Abstractions.Expressions
{
    /// <summary>
    /// 字段条件表达式
    /// </summary>
    public class FieldConditionExpression : UnitaryConditionExpression
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; }
        public FieldConditionExpression(string expression)
        {
            if (expression.StartsWith("{") && expression.EndsWith("}"))
            {
                expression = expression[1..(expression.Length - 1)];
            }
            string[] expressions = expression.Split('|');
            if (expressions.Length != 2) throw new BusinessFlowException($"表达式{expression}错误,字段表达式不符合规范");
            if (expressions[0] != "F") throw new BusinessFlowException($"表达式{expression}错误,不是字段表达式");
            FieldName = expressions[1];
        }
        public override object? GetValue(Dictionary<string, object?> fieldDatas)
        {
            if (fieldDatas.ContainsKey(FieldName)) return fieldDatas[FieldName];
            throw new BusinessFlowException($"找不到字段{FieldName}");
        }
    }
}
