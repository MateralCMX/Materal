﻿using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Utils.Model
{
    /// <summary>
    /// 过滤器特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FilterAttribute : Attribute
    {
        /// <summary>
        /// 目标属性名称
        /// </summary>
        public string? TargetPropertyName { get; protected set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="targetPropertyName"></param>
        protected FilterAttribute(string? targetPropertyName = null)
        {
            TargetPropertyName = targetPropertyName;
        }
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
            if (value == null || value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression, Expression? otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
            Expression result = func(leftExpression, rightExpression);
            if (otherExpression != null)
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
            var isNullable = false;
            if (targetPropertyInfo != null)
            {
                string? fullName = targetPropertyInfo.PropertyType.FullName;
                if (string.IsNullOrWhiteSpace(fullName)) throw new InvalidOperationException("获取属性名失败");
                isNullable = fullName.IndexOf("System.Nullable", StringComparison.Ordinal) == 0;
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
            if (value == null) return null;
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
            MethodInfo? methodInfo = propertyType.GetMethod(methodName, new[] { propertyType });
            Expression? otherExpression;
            Expression result;
            if (methodInfo != null)
            {
                (Expression leftExpression, Expression rightExpression, otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                result = Expression.Call(leftExpression, methodInfo, rightExpression);
            }
            else
            {
                methodInfo = propertyType.GetMethod(methodName, new[] { targetPropertyInfo.PropertyType });
                if (methodInfo == null) return null;
                (Expression rightExpression, Expression leftExpression, otherExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value, targetPropertyInfo);
                result = Expression.Call(leftExpression, methodInfo, rightExpression);
            }
            if (otherExpression != null)
            {
                result = Expression.Add(otherExpression, result);
            }
            return result;
        }
    }
}
