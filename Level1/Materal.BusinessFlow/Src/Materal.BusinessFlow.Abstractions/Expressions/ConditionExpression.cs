using Materal.BusinessFlow.Abstractions.Enums;

namespace Materal.BusinessFlow.Abstractions.Expressions
{
    /// <summary>
    /// 条件表达式
    /// </summary>
    public abstract class ConditionExpression
    {
        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="fieldDatas"></param>
        /// <returns></returns>
        public abstract object? GetValue(Dictionary<string, object?> fieldDatas);
        /// <summary>
        /// 格式化条件表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ConditionExpression Parse(string expression)
        {
            string tempExpression = expression.Trim();
            (string left, string middle, string right) = SplitExpression(tempExpression);
            ConditionExpression leftConditionExpression = ParseUnitaryExpression(left);
            ComparisonTypeEnum middleType = ParseComparisonType(middle);
            ConditionExpression rightConditionExpression = ParseUnitaryExpression(right);
            BinaryConditionExpression result = new(leftConditionExpression, middleType, rightConditionExpression);
            return result;
        }
        /// <summary>
        /// 格式化逻辑类型
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static ComparisonTypeEnum ParseComparisonType(string expression)
        {
            if (expression.StartsWith("[") && expression.EndsWith("]"))
            {
                expression = expression[1..(expression.Length - 1)];
            }
            try
            {
                object? type = Enum.Parse(typeof(ComparisonTypeEnum), expression);
                if (type == null || type is not ComparisonTypeEnum result) throw new BusinessFlowException("表达式错误,未识别的逻辑类型");
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("表达式错误,未识别的逻辑类型", ex);
            }
        }
        /// <summary>
        /// 格式化一元表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static ConditionExpression ParseUnitaryExpression(string expression)
        {
            ConditionExpression result;
            if (expression.First() == '(')
            {
                expression = expression[1..(expression.Length - 1)];
                result = Parse(expression);
            }
            else if (expression.First() == '{')
            {
                result = expression[1] switch
                {
                    'F' => new FieldConditionExpression(expression),
                    'C' => new ConstantConditionExpression(expression),
                    _ => throw new BusinessFlowException("表达式错误,未识别的一元表达式")
                }; ;
            }
            else throw new BusinessFlowException("表达式错误,未识别的一元表达式");
            return result;
        }
        /// <summary>
        /// 拆分表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static (string left, string middle, string right) SplitExpression(string expression)
        {
            string tempExpression = expression;
            int startIndex = 0;
            int endIndex = GetEndIndex(tempExpression) + 1;
            string left = tempExpression[startIndex..endIndex];
            tempExpression = tempExpression[endIndex..];
            endIndex = GetEndIndex(tempExpression) + 1;
            string middle = tempExpression[startIndex..endIndex];
            string right = tempExpression[endIndex..];
            return (left, middle, right);
        }
        /// <summary>
        /// 获得结束位序
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static int GetEndIndex(string expression)
        {
            char startChar = expression.First();
            char endChar = startChar switch
            {
                '(' => ')',
                '{' => '}',
                '[' => ']',
                _ => throw new BusinessFlowException("表达式错误,未找到开始标记"),
            };
            int offsetIndex = 1;
            for (int i = 1; i < expression.Length; i++)
            {
                if (expression[i] == startChar)
                {
                    offsetIndex++;
                }
                else if (expression[i] == endChar)
                {
                    offsetIndex--;
                    if (offsetIndex == 0)
                    {
                        return i;
                    }
                }
            }
            throw new BusinessFlowException("表达式错误,未找到结束标记");
        }
    }
}
