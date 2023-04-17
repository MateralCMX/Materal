namespace Materal.BusinessFlow.Abstractions.Expressions
{
    /// <summary>
    /// 常量条件表达式
    /// </summary>
    public class ConstantConditionExpression : UnitaryConditionExpression
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DataTypeEnum DataType { get; }
        public ConstantConditionExpression(string expression)
        {
            if (expression.StartsWith("{") && expression.EndsWith("}"))
            {
                expression = expression[1..(expression.Length - 1)];
            }
            string[] expressions = expression.Split('|');
            if (expressions.Length < 2 || expressions.Length > 3) throw new BusinessFlowException($"表达式{expression}错误,常量表达式不符合规范");
            if (expressions[0] != "C") throw new BusinessFlowException($"表达式{expression}错误,不是常量表达式");
            Value = expressions[1];
            if (expressions.Length > 2)
            {
                try
                {
                    object? type = Enum.Parse(typeof(DataTypeEnum), expressions[2]);
                    if (type == null || type is not DataTypeEnum dataType) throw new BusinessFlowException($"表达式{expression}错误,未识别的数据类型{expressions[2]}");
                    DataType = dataType;
                }
                catch (Exception ex)
                {
                    throw new BusinessFlowException($"表达式{expression}错误,未识别的数据类型{expressions[2]}", ex);
                }
            }
            else
            {
                DataType = DataTypeEnum.String;
            }
        }
        public override object? GetValue(Dictionary<string, object?> fieldDatas)
        {
            try
            {
                return DataType switch
                {
                    DataTypeEnum.Enum or 
                    DataTypeEnum.String => Value,
                    DataTypeEnum.Number => Convert.ToDecimal(Value),
                    DataTypeEnum.Date or 
                    DataTypeEnum.Time or 
                    DataTypeEnum.DateTime => Convert.ToDateTime(Value),
                    DataTypeEnum.Boole => Convert.ToBoolean(Value),
                    _ => throw new BusinessFlowException("未知数据类型"),
                };
            }
            catch(BusinessFlowException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("转换值失败", ex);
            }
        }
    }
}
