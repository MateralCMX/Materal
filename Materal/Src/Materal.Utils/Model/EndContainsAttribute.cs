﻿namespace Materal.Utils.Model
{
    /// <summary>
    /// EndContains
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EndContainsAttribute(string? targetPropertyName = null) : FilterAttribute(targetPropertyName)
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
            if (value is null || value.IsNullOrEmptyString()) return null;
            Expression? result = GetCallExpression(parameterExpression, propertyInfo, value, "EndsWith", targetPropertyInfo);
            return result;
        }
    }
}
