using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 不等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEqualAttribute : FilterAttribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetPropertyName"></param>
        public NotEqualAttribute(string? targetPropertyName = null) : base(targetPropertyName) { }
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
            return GetSearchExpression(parameterExpression, propertyInfo, value, targetPropertyInfo, Expression.NotEqual);
        }
    }
}
