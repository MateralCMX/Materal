using Materal.BusinessFlow.Abstractions.Enums;

namespace Materal.BusinessFlow.Abstractions.Expressions
{
    /// <summary>
    /// 二元表条件达式
    /// </summary>
    public class BinaryConditionExpression : ConditionExpression
    {
        /// <summary>
        /// 左边的表达式
        /// </summary>
        public ConditionExpression Left { get; }
        /// <summary>
        /// 逻辑类型
        /// </summary>
        public ComparisonTypeEnum ComparisonType { get; }
        /// <summary>
        /// 右边的表达式
        /// </summary>
        public ConditionExpression Right { get; }
        public BinaryConditionExpression(ConditionExpression left, ComparisonTypeEnum comparisonType, ConditionExpression right)
        {
            Left = left;
            ComparisonType = comparisonType;
            Right = right;
        }
        public override object? GetValue(Dictionary<string, object?> fieldDatas)
        {
            object? leftValue = Left.GetValue(fieldDatas);
            object? rightValue = Right.GetValue(fieldDatas);
            return ComparisonType switch
            {
                ComparisonTypeEnum.Equal => ObjectEquals(leftValue, rightValue),
                ComparisonTypeEnum.NotEqual => !ObjectEquals(leftValue, rightValue),
                ComparisonTypeEnum.GreaterThan => CompareNumber(leftValue, rightValue, (m, n) => m > n),
                ComparisonTypeEnum.LessThan => CompareNumber(leftValue, rightValue, (m, n) => m < n),
                ComparisonTypeEnum.GreaterThanOrEqual => CompareNumber(leftValue, rightValue, (m, n) => m >= n),
                ComparisonTypeEnum.LessThanOrEqual => CompareNumber(leftValue, rightValue, (m, n) => m <= n),
                ComparisonTypeEnum.And => CompareBool(leftValue, rightValue, (m, n) => m && n),
                ComparisonTypeEnum.Or => (object)CompareBool(leftValue, rightValue, (m, n) => m || n),
                _ => throw new BusinessFlowException("未知逻辑类型"),
            };
        }
        /// <summary>
        /// 对象是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private bool ObjectEquals(object? left, object? right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;
            if(IsNumber(left) && IsNumber(right))
            {
                decimal leftNumber = Convert.ToDecimal(left);
                decimal rightNumber = Convert.ToDecimal(right);
                return leftNumber.Equals(rightNumber);
            }
            return left.Equals(right);
        }
        /// <summary>
        /// 比较数字
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparFunc"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private bool CompareNumber(object? left, object? right, Func<decimal, decimal, bool> comparFunc)
        {
            if (left == null || right == null) throw new BusinessFlowException("不能比较大小");
            try
            {
                decimal leftValue = Convert.ToDecimal(left);
                decimal rightValue = Convert.ToDecimal(right);
                return comparFunc(leftValue, rightValue);
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("不能比较大小", ex);
            }
        }
        /// <summary>
        /// 比较布尔
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparFunc"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private bool CompareBool(object? left, object? right, Func<bool, bool, bool> comparFunc)
        {
            if (left == null || right == null) throw new BusinessFlowException("不能比较位");
            try
            {
                bool leftValue = Convert.ToBoolean(left);
                bool rightValue = Convert.ToBoolean(right);
                return comparFunc(leftValue, rightValue);
            }
            catch (Exception ex)
            {
                throw new BusinessFlowException("不能比较位", ex);
            }
        }
        /// <summary>
        /// 是数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsNumber(object obj)
        {
            return obj is int || obj is long || obj is short || obj is uint || obj is ushort || obj is ulong || obj is float || obj is double || obj is decimal;
        }
    }
}
