namespace Materal.Utils.Model
{
    /// <summary>
    /// 小于等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LessThanOrEqualAttribute(string? targetPropertyName = null) : FilterAttribute(targetPropertyName)
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <returns></returns>
        public override Expression? GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, PropertyInfo targetPropertyInfo)
        {
            return GetSearchExpression(parameterExpression, propertyInfo, value, targetPropertyInfo, Expression.LessThanOrEqual);
        }
    }
}
