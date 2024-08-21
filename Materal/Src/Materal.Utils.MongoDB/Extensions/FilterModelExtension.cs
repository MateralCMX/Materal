using MongoDB.Bson;
using MongoDB.Driver;

namespace Materal.Utils.MongoDB.Extensions
{
    /// <summary>
    /// FilterModel扩展
    /// </summary>
    public static class FilterModelExtension
    {
        /// <summary>
        /// 获取排序过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public static SortDefinition<T>? GetSortDefinition<T>(this FilterModel filterModel)
        {
            if (filterModel.SortPropertyName is null || string.IsNullOrWhiteSpace(filterModel.SortPropertyName)) return null;
            Type tType = typeof(T);
            if (tType != typeof(BsonDocument))
            {
                PropertyInfo? propertyInfo = tType.GetProperty(filterModel.SortPropertyName);
                if (propertyInfo is null) return null;
                if (filterModel.IsAsc) return Builders<T>.Sort.Ascending(propertyInfo.Name);
                else return Builders<T>.Sort.Descending(propertyInfo.Name);
            }
            else
            {
                if (filterModel.IsAsc) return Builders<T>.Sort.Ascending(filterModel.SortPropertyName);
                else return Builders<T>.Sort.Descending(filterModel.SortPropertyName);
            }
        }
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        public static FilterDefinition<T> GetSearchFilterDefinition<T>(this FilterModel filterModel)
        {
            FilterDefinition<T>? filter = null;
            foreach (PropertyInfo propertyInfo in filterModel.GetType().GetProperties())
            {
                FilterDefinition<T>? right = GetSearchFilterDefinition<T>(propertyInfo, filterModel);
                if (right is null) continue;
                filter = filter is null ? right : Builders<T>.Filter.And(filter, right);
            }
            return filter ?? Builders<T>.Filter.Empty;
        }
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(PropertyInfo propertyInfo, FilterModel filterModel)
        {
            if (!propertyInfo.CanWrite || !propertyInfo.CanRead) return null;
            object? value = propertyInfo.GetValue(filterModel);
            if (value is null) return null;
            if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue)) return null;
            FilterDefinition<T>? filter = null;
            IEnumerable<FilterAttribute> filterAttributes = propertyInfo.GetCustomAttributes<FilterAttribute>();
            foreach (FilterAttribute filterAttribute in filterAttributes)
            {
                FilterDefinition<T>? right = filterAttribute.GetSearchFilterDefinition<T>(propertyInfo, value);
                if (right is null) continue;
                filter = filter is null ? right : Builders<T>.Filter.And(filter, right);
            }
            return filter;
        }
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filterAttribute"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="MongoUtilException"></exception>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this FilterAttribute filterAttribute, PropertyInfo propertyInfo, object value)
        {
            string name;
            Type tType = typeof(T);
            if (filterAttribute.TargetPropertyName is null || string.IsNullOrWhiteSpace(filterAttribute.TargetPropertyName))
            {
                if (tType != typeof(BsonDocument))
                {
                    PropertyInfo targetPropertyInfo = tType.GetProperty(propertyInfo.Name) ?? throw new MongoUtilException($"类型{tType.FullName}中不存在属性{propertyInfo.Name}");
                    name = targetPropertyInfo.Name;
                }
                else
                {
                    name = propertyInfo.Name;
                }
            }
            else
            {
                if (tType != typeof(BsonDocument))
                {
                    PropertyInfo targetPropertyInfo = tType.GetProperty(filterAttribute.TargetPropertyName) ?? throw new MongoUtilException($"类型{tType.FullName}中不存在属性{filterAttribute.TargetPropertyName}");
                    name = targetPropertyInfo.Name;
                }
                else
                {
                    name = filterAttribute.TargetPropertyName;
                }
            }
            return filterAttribute switch
            {
                ContainsAttribute containsAttribute => containsAttribute.GetSearchFilterDefinition<T>(name, value),
                EndContainsAttribute endContainsAttribute => endContainsAttribute.GetSearchFilterDefinition<T>(name, value),
                EqualAttribute equalAttribute => equalAttribute.GetSearchFilterDefinition<T>(name, value),
                GreaterThanAttribute greaterThanAttribute => greaterThanAttribute.GetSearchFilterDefinition<T>(name, value),
                GreaterThanOrEqualAttribute greaterThanOrEqualAttribute => greaterThanOrEqualAttribute.GetSearchFilterDefinition<T>(name, value),
                LessThanAttribute lessThanAttribute => lessThanAttribute.GetSearchFilterDefinition<T>(name, value),
                LessThanOrEqualAttribute lessThanOrEqualAttribute => lessThanOrEqualAttribute.GetSearchFilterDefinition<T>(name, value),
                NotEqualAttribute notEqualAttribute => notEqualAttribute.GetSearchFilterDefinition<T>(name, value),
                StartContainsAttribute startContainsAttribute => startContainsAttribute.GetSearchFilterDefinition<T>(name, value),
                _ => null
            };
        }
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this EqualAttribute _, string name, object value)
            => Builders<T>.Filter.Eq(name, value);
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this ContainsAttribute _, string name, object value)
            => Builders<T>.Filter.Regex(name, $"/{value}/");
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this StartContainsAttribute _, string name, object value)
            => Builders<T>.Filter.Regex(name, $"/^{value}/");
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this EndContainsAttribute _, string name, object value)
            => Builders<T>.Filter.Regex(name, $"/{value}^/");
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this GreaterThanAttribute _, string name, object value)
            => Builders<T>.Filter.Gt(name, value);
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this GreaterThanOrEqualAttribute _, string name, object value)
            => Builders<T>.Filter.Gte(name, value);
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this LessThanAttribute _, string name, object value)
            => Builders<T>.Filter.Lt(name, value);
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this LessThanOrEqualAttribute _, string name, object value)
            => Builders<T>.Filter.Lte(name, value);
        /// <summary>
        /// 获取搜索过滤器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static FilterDefinition<T>? GetSearchFilterDefinition<T>(this NotEqualAttribute _, string name, object value)
            => Builders<T>.Filter.Not(Builders<T>.Filter.Eq(name, value));
    }
}
