using System.Collections;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 过滤器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FilterAttribute(string? targetPropertyName = null) : Attribute
    {
        /// <summary>
        /// 目标属性名称
        /// </summary>
        public string? TargetPropertyName { get; protected set; } = targetPropertyName;
        /// <summary>
        /// 获得表达式目录树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <returns></returns>
        public abstract Expression? GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, PropertyInfo targetPropertyInfo);
        /// <summary>
        /// 获得表达式目录树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected Expression? GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, PropertyInfo targetPropertyInfo, Func<Expression, Expression, Expression> func)
        {
            if (value is null || value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression, Expression? otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
            Expression result = func(leftExpression, rightExpression);
            if (otherExpression is not null)
            {
                result = Expression.AndAlso(otherExpression, result);
            }
            return result;
        }
        /// <summary>
        /// 获得左边和右边的表达式目录树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <returns></returns>
        /// <exception cref="UtilException"></exception>
        protected (Expression leftExpression, Expression rightExpression, Expression? otherExpression) GetLeftAndRightExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, PropertyInfo targetPropertyInfo)
        {
            bool isNullable = false;
            if (targetPropertyInfo is not null)
            {
                string? fullName = targetPropertyInfo.PropertyType.FullName;
                if (string.IsNullOrWhiteSpace(fullName)) throw new InvalidOperationException("获取属性名失败");
                isNullable = fullName.StartsWith("System.Nullable", StringComparison.Ordinal);
            }
            Expression leftExpression = GetLeftExpression(parameterExpression, propertyInfo, isNullable);
            Expression rightExpression = GetRightExpression(propertyInfo, value);
            Expression? otherExpression = null;
            if (isNullable)
            {
                otherExpression = Expression.Property(parameterExpression, TargetPropertyName ?? propertyInfo.Name);
                otherExpression = Expression.Property(otherExpression, "HasValue");
            }
            return (leftExpression, rightExpression, otherExpression);
        }
        /// <summary>
        /// 获得左边的表达式目录树
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="isNullable"></param>
        /// <returns></returns>
        protected Expression GetLeftExpression(ParameterExpression parameterExpression, PropertyInfo propertyInfo, bool isNullable)
        {
            MemberExpression leftExpression;
            if (isNullable)
            {
                leftExpression = Expression.Property(parameterExpression, TargetPropertyName ?? propertyInfo.Name);
                leftExpression = Expression.Property(leftExpression, "Value");
            }
            else
            {
                leftExpression = Expression.Property(parameterExpression, TargetPropertyName ?? propertyInfo.Name);
            }
            return leftExpression;
        }
        /// <summary>
        /// 获得右边的表达式目录树
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static Expression GetRightExpression<T>(PropertyInfo propertyInfo, T value)
        {
            dynamic useValue = GetValue(propertyInfo, value) ?? throw new UtilException("不能转换为对应类型");
            ConstantExpression rightExpression = Expression.Constant(useValue);
            return rightExpression;
        }
        /// <summary>
        /// 获得值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static dynamic? GetValue<T>(PropertyInfo propertyInfo, T value)
        {
            if (value is null) return null;
            dynamic? useValue = value;
            Type propertyType = propertyInfo.PropertyType;
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                propertyType = propertyType.GetGenericArguments()[0];
            }
            Type valueType = value.GetType();
            if (valueType != propertyType)
            {
                useValue = value.CanConvertTo(propertyType) ? value.ConvertTo(propertyType) : null;
            }
            return useValue;
        }

        /// <summary>
        /// 获得执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <param name="methodName"></param>
        /// <param name="targetPropertyInfo"></param>
        /// <returns></returns>
        protected Expression? GetCallExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, string methodName, PropertyInfo targetPropertyInfo)
        {
            Type propertyType = propertyInfo.PropertyType;
            MethodInfo? methodInfo = propertyType.GetMethod(methodName, [propertyType]);
            Expression? otherExpression = null;
            Expression? result = null;
            if (methodInfo is not null)
            {
                (Expression leftExpression, Expression rightExpression, otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                result = Expression.Call(leftExpression, methodInfo, rightExpression);
            }
            else
            {
                if (targetPropertyInfo.PropertyType.IsGenericType && targetPropertyInfo.PropertyType.GenericTypeArguments.Length == 1)
                {
                    Type genericType = targetPropertyInfo.PropertyType.GenericTypeArguments.First();
                    Type nullType = typeof(Nullable<>).MakeGenericType(genericType);
                    if (targetPropertyInfo.PropertyType == nullType)
                    {
                        methodInfo = propertyType.GetMethod(methodName, [genericType]);
                        if (methodInfo is not null)
                        {
                            (Expression rightExpression, Expression leftExpression, _) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                            result = Expression.Call(leftExpression, methodInfo, rightExpression);
                            Expression temp = Expression.Property(parameterExpression, targetPropertyInfo.Name);
                            temp = Expression.NotEqual(temp, Expression.Constant(null));
                            result = Expression.AndAlso(temp, result);
                        }
                    }
                }
                else
                {
                    methodInfo = propertyType.GetMethod(methodName, [targetPropertyInfo.PropertyType]);
                    if (methodInfo is not null)
                    {
                        (Expression rightExpression, Expression leftExpression, otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                        result = Expression.Call(leftExpression, methodInfo, rightExpression);
                    }
                }
            }
            if (result is null)
            {
                if (methodName == nameof(IList.Contains))
                {
                    result = GetContainsCallExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                }
            }
            if (result is null) return null;
            if (otherExpression is not null)
            {
                result = Expression.Add(otherExpression, result);
            }
            return result;
        }
        private Expression? GetContainsCallExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, PropertyInfo targetPropertyInfo)
        {
            Expression? result = null;
            Expression? otherExpression = null;
            MethodInfo? methodInfo = typeof(Enumerable).GetMethods().FirstOrDefault(m => m.Name == "Contains" && m.GetParameters().Length == 2);
            if (methodInfo is null) return null;
            if (targetPropertyInfo.PropertyType.IsGenericType && targetPropertyInfo.PropertyType.GenericTypeArguments.Length == 1)
            {
                Type genericType = targetPropertyInfo.PropertyType.GenericTypeArguments.First();
                methodInfo = methodInfo.MakeGenericMethod(genericType);
                Type nullType = typeof(Nullable<>).MakeGenericType(genericType);
                if (targetPropertyInfo.PropertyType == nullType)
                {
                    (Expression rightExpression, Expression leftExpression, _) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                    result = Expression.Call(null, methodInfo, leftExpression, rightExpression);
                    Expression temp = Expression.Property(parameterExpression, targetPropertyInfo.Name);
                    temp = Expression.NotEqual(temp, Expression.Constant(null));
                    result = Expression.AndAlso(temp, result);
                }
            }
            else
            {
                methodInfo = methodInfo.MakeGenericMethod(targetPropertyInfo.PropertyType);
                (Expression rightExpression, Expression leftExpression, otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                result = Expression.Call(null, methodInfo, leftExpression, rightExpression);
            }
            if (result is null) return null;
            if (otherExpression is not null)
            {
                result = Expression.Add(otherExpression, result);
            }
            return result;
        }
    }
}
