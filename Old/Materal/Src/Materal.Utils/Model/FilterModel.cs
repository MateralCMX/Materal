using System.Linq.Expressions;
using System.Reflection;

namespace Materal.Utils.Model
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
            Expression? expression = null;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<FilterAttribute> filterAttributes = propertyInfo.GetCustomAttributes<FilterAttribute>();
                foreach (FilterAttribute filterAttribute in filterAttributes)
                {
                    object? value = propertyInfo.GetValue(this);
                    PropertyInfo? tValuePropertyInfo = tType.GetProperty(filterAttribute.TargetPropertyName ?? propertyInfo.Name);
                    if (tValuePropertyInfo == null) continue;
                    Expression? binaryExpression = filterAttribute.GetSearchExpression(mParameterExpression, propertyInfo, value, tValuePropertyInfo);
                    if (binaryExpression != null)
                    {
                        expression = expression == null ? binaryExpression : Expression.AndAlso(expression, binaryExpression);
                    }
                }
            }
            Expression<Func<T, bool>> result = expression != null
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
}
