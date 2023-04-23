using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Utils.Model
{
    /// <summary>
    /// EndContains
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EndContainsAttribute : FilterAttribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetPropertyName"></param>
        public EndContainsAttribute(string? targetPropertyName = null) : base(targetPropertyName) { }
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
            if (value == null || value.IsNullOrEmptyString()) return null;
            Expression? result = GetCallExpression(parameterExpression, propertyInfo, value, "EndsWith", targetPropertyInfo);
            return result;
        }
    }
}
