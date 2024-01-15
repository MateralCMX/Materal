using Newtonsoft.Json;
using System.Xml;

namespace Materal.Extensions
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Json转换为XML文档对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>XML文档对象</returns>
        public static XmlDocument? JsonToXml(this string jsonStr) => JsonConvert.DeserializeXmlNode(jsonStr);
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object JsonToObject(this string jsonStr)
        {
            try
            {
                var model = new object();
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new ExtensionException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object JsonToObject(this string jsonStr, string typeName)
        {
            Type triggerDataType = typeName.GetTypeByTypeName(Array.Empty<object>()) ?? throw new ExtensionException("转换失败");
            object result = jsonStr.JsonToObject(triggerDataType) ?? throw new ExtensionException("转换失败");
            return result;
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object JsonToObject(this string jsonStr, Type type)
        {
            try
            {
                object model = Activator.CreateInstance(type) ?? throw new ExtensionException("创建实体失败");
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new ExtensionException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static T JsonToObject<T>(this string jsonStr)
        {
            try
            {
                T model = typeof(T).Instantiation<T>() ?? throw new ExtensionException("转换失败");
                JsonConvert.PopulateObject(jsonStr, model);
                return model;
            }
            catch (Exception ex)
            {
                throw new ExtensionException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static object JsonToDeserializeObject(this string jsonStr)
        {
            try
            {
                object model = JsonConvert.DeserializeObject(jsonStr) ?? throw new ExtensionException("转换失败");
                return model;
            }
            catch (Exception ex)
            {
                throw new ExtensionException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="jsonStr">Json字符串</param>
        /// <returns>转换后的对象</returns>
        public static T JsonToDeserializeObject<T>(this string jsonStr)
        {
            try
            {
                T model = JsonConvert.DeserializeObject<T>(jsonStr) ?? throw new ExtensionException("转换失败");
                return model;
            }
            catch (Exception ex)
            {
                throw new ExtensionException("Json字符串有误", ex);
            }
        }
        /// <summary>
        /// Json字符串转换对象
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="jsonStr"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        /// <exception cref="ExtensionException"></exception>
        public static T JsonToInterface<T>(this string jsonStr, string typeName)
        {
            Type triggerDataType = typeName.GetTypeByTypeName<T>() ?? throw new ExtensionException("转换失败");
            return (T)jsonStr.JsonToObject(triggerDataType);
        }
        /// <summary>
        /// 对象转换为Json
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns>转换后的Json字符串</returns>
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }
}
