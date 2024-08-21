namespace Materal.Utils.Model
{
    /// <summary>
    /// 过滤器模型
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// 排序属性名称
        /// </summary>
        public string? SortPropertyName { get; set; }
        /// <summary>
        /// 正序
        /// </summary>
        public bool IsAsc { get; set; } = false;
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
                    if (tValuePropertyInfo is null) continue;
                    Expression? binaryExpression = filterAttribute.GetSearchExpression(mParameterExpression, propertyInfo, value, tValuePropertyInfo);
                    if (binaryExpression is not null)
                    {
                        expression = expression is null ? binaryExpression : Expression.AndAlso(expression, binaryExpression);
                    }
                }
            }
            Expression<Func<T, bool>> result = expression is not null
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
        /// <summary>
        /// 获得排序目录树表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Expression<Func<T, object>>? GetSortExpression<T>()
        {
            if (string.IsNullOrWhiteSpace(SortPropertyName)) return null;
            Type tType = typeof(T);
            PropertyInfo? propertyInfo = tType.GetProperty(SortPropertyName);
            if (propertyInfo is null) return null;
            ParameterExpression parameterExpression = Expression.Parameter(tType, "m");
            MemberExpression memberExpression = Expression.Property(parameterExpression, propertyInfo);
            Expression<Func<T, object>> result;
            try
            {
                result = Expression.Lambda<Func<T, object>>(memberExpression, parameterExpression);
            }
            catch
            {
                result = Expression.Lambda<Func<T, object>>(Expression.Convert(memberExpression, typeof(object)), parameterExpression);
            }
            return result;
        }
        /// <summary>
        /// 获得排序委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<T, object>? GetSortDlegate<T>()
        {
            Expression<Func<T, object>>? searchExpression = GetSortExpression<T>();
            Func<T, object>? result = searchExpression?.Compile();
            return result;
        }
        /// <summary>
        /// 设置排序表达式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="defaultOrderExpression"></param>
        /// <param name="defaultIsAsc"></param>
        /// <returns></returns>
        public IQueryable<T> SetSortExpresssion<T>(IQueryable<T> values, Expression<Func<T, object>>? defaultOrderExpression = null, bool? defaultIsAsc = null)
        {
            IQueryable<T> result = values;
            bool isAsc = IsAsc;
            Expression<Func<T, object>>? sortExpression = GetSortExpression<T>();
            if (sortExpression is null)
            {
                if (defaultOrderExpression is null) return result;
                sortExpression = defaultOrderExpression;
                isAsc = defaultIsAsc ?? IsAsc;
            }
            if (isAsc)
            {
                result = result.OrderBy(sortExpression);
            }
            else
            {
                result = result.OrderByDescending(sortExpression);
            }
            return result;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="defaultOrderExpression"></param>
        /// <param name="defaultIsAsc"></param>
        /// <returns></returns>
        public List<T> Sort<T>(ICollection<T> values, Expression<Func<T, object>>? defaultOrderExpression = null, bool? defaultIsAsc = null)
        {
            List<T> result = [.. values];
            bool isAsc = IsAsc;
            Func<T, object>? sortDlegate = GetSortDlegate<T>();
            if (sortDlegate is null)
            {
                if (defaultOrderExpression is null) return result;
                sortDlegate = defaultOrderExpression.Compile();
                isAsc = defaultIsAsc ?? IsAsc;
            }
            if (isAsc)
            {
                result = [.. result.OrderBy(sortDlegate)];
            }
            else
            {
                result = [.. result.OrderByDescending(sortDlegate)];
            }
            return result;
        }
    }
}
