using Materal.Common;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Model
{
    /// <summary>
    /// 过滤器模型
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// 获得查询目录树表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Expression<Func<T, bool>> GetSearchExpression<T>()
        {
            Type tType = typeof(T);
            Type thisType = GetType();
            ParameterExpression mParameterExpression = Expression.Parameter(tType, "m");
            PropertyInfo[] propertyInfos = thisType.GetProperties();
            Expression<Func<T, bool>> result;
            Expression expression = null;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<FilterAttribute> filterAttributes = propertyInfo.GetCustomAttributes<FilterAttribute>();
                foreach (FilterAttribute filterAttribute in filterAttributes)
                {
                    try
                    {
                        object value = propertyInfo.GetValue(this);
                        Expression binaryExpression = filterAttribute.GetSearchExpression(mParameterExpression, propertyInfo, value);
                        if (binaryExpression != null)
                        {
                            expression = expression == null ? binaryExpression : Expression.And(expression, binaryExpression);
                        }
                    }
                    catch (MateralConvertException) { }
                }
            }
            result = expression != null
                ? Expression.Lambda<Func<T, bool>>(expression, mParameterExpression)
                : m => true;
            return result;
        }
        /// <summary>
        /// 获得查询委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<T, bool> GetSearchDelegate<T>()
        {
            Expression<Func<T, bool>> searchExpression = GetSearchExpression<T>();
            Func<T, bool> result = searchExpression.Compile();
            return result;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FilterAttribute : Attribute
    {
        /// <summary>
        /// 目标属性名称
        /// </summary>
        public string TargetPropertyName { get; protected set; }
        protected FilterAttribute(string targetPropertyName = null)
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
        /// <returns></returns>
        public abstract Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value);
        /// <summary>
        /// 获得左边和右边的表达式目录树
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="MateralConvertException"></exception>
        protected (Expression leftExpression, Expression rightExpression) GetLeftAndRightExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            Expression leftExpression = GetLeftExpression(parameterExpression, propertyInfo);
            Expression rightExpression = GetRightExpression(propertyInfo,value);
            return (leftExpression, rightExpression);
        }
        /// <summary>
        /// 获得左边的表达式目录树
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        protected Expression GetLeftExpression(ParameterExpression parameterExpression, PropertyInfo propertyInfo)
        {
            MemberExpression leftExpression = Expression.Property(parameterExpression, TargetPropertyName ?? propertyInfo.Name);
            return leftExpression;
        }
        /// <summary>
        /// 获得右边的表达式目录树
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected Expression GetRightExpression<T>(PropertyInfo propertyInfo,T value)
        {
            dynamic useValue = GetValue(propertyInfo, value);
            if(useValue == null) throw new MateralConvertException("不能转换为对应类型");
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
        protected dynamic GetValue<T>(PropertyInfo propertyInfo, T value)
        {
            dynamic useValue = value;
            Type propertyType = propertyInfo.PropertyType;
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
        /// <returns></returns>
        protected Expression GetCallExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value, string methodName)
        {
            Type propertyType = propertyInfo.PropertyType;
            MethodInfo methodInfo = propertyType.GetMethod(methodName, new[] { propertyType });
            if (methodInfo == null) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            MethodCallExpression result = Expression.Call(leftExpression, methodInfo, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EqualAttribute : FilterAttribute
    {
        public EqualAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.Equal(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 不等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEqualAttribute : FilterAttribute
    {
        public NotEqualAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.NotEqual(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 大于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanAttribute : FilterAttribute
    {
        public GreaterThanAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.GreaterThan(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 大于等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class GreaterThanOrEqualAttribute : FilterAttribute
    {
        public GreaterThanOrEqualAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 小于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LessThanAttribute : FilterAttribute
    {
        public LessThanAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.LessThan(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 小于等于
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LessThanOrEqualAttribute : FilterAttribute
    {
        public LessThanOrEqualAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            (Expression leftExpression, Expression rightExpression) = GetLeftAndRightExpression(parameterExpression, propertyInfo, value);
            BinaryExpression result = Expression.LessThanOrEqual(leftExpression, rightExpression);
            return result;
        }
    }
    /// <summary>
    /// 包含
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ContainsAttribute : FilterAttribute
    {
        public ContainsAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            Expression result = GetCallExpression(parameterExpression, propertyInfo, value, "Contains");
            return result;
        }
    }
    /// <summary>
    /// StartContains
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StartContainsAttribute : FilterAttribute
    {
        public StartContainsAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            Expression result = GetCallExpression(parameterExpression, propertyInfo, value, "StartsWith");
            return result;
        }
    }
    /// <summary>
    /// EndContains
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EndContainsAttribute : FilterAttribute
    {
        public EndContainsAttribute(string targetPropertyName = null) : base(targetPropertyName) { }
        public override Expression GetSearchExpression<T>(ParameterExpression parameterExpression, PropertyInfo propertyInfo, T value)
        {
            if (value.IsNullOrEmptyString()) return null;
            Expression result = GetCallExpression(parameterExpression, propertyInfo, value, "EndsWith");
            return result;
        }
    }
}
