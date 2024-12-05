using Materal.Extensions;
using MongoDB.Bson;
using System.Collections;

namespace Materal.Utils.MongoDB.Extensions
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 转换为BsonDocument
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static BsonDocument ToBsonObject(this object? obj)
        {
            Dictionary<string, object?> dictionary = ConvertToDictionary(obj);
            BsonDocument result = new(dictionary);
            return result;
        }
        /// <summary>
        /// 转换为BsonDocument
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<BsonDocument> ToBsonObjects(this IEnumerable obj)
        {
            List<BsonDocument> result = [];
            foreach (object? item in obj)
            {
                result.Add(item.ToBsonObject());
            }
            return result;
        }
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Dictionary<string, object?> ConvertToDictionary(object? obj)
        {
            Dictionary<string, object?> result = [];
            if (obj is null) return result;
            Type objType = obj.GetType();
            foreach (PropertyInfo propertyInfo in objType.GetProperties())
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite) continue;
                object? valueObj = propertyInfo.GetValue(obj)?.ToExpandoObject();
                if (valueObj is Guid guid)
                {
                    valueObj = new BsonBinaryData(guid, GuidRepresentation.Standard);
                }
                result.TryAdd(propertyInfo.Name, valueObj);
            }
            return result;
        }
    }
}
