using LitJson;

namespace Materal.Utils.Wechat
{
    /// <summary>
    /// Json数据扩展
    /// </summary>
    public static class JsonDataExtension
    {
        /// <summary>
        /// 获得int值
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetInt(this JsonData jsonData, string key)
        {
            if (jsonData.ContainsKey(key) && jsonData[key].GetJsonType() == JsonType.Int)
            {
                return (int)jsonData[key];
            }
            return null;
        }
        /// <summary>
        /// 获得字符串值
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? GetString(this JsonData jsonData, string key)
        {
            if (jsonData.ContainsKey(key) && jsonData[key].GetJsonType() == JsonType.String)
            {
                return (string)jsonData[key];
            }
            return null;
        }
        /// <summary>
        /// 获得布尔值
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool? GetBoolean(this JsonData jsonData, string key)
        {
            if (jsonData.ContainsKey(key) && jsonData[key].GetJsonType() == JsonType.Boolean)
            {
                return (bool)jsonData[key];
            }
            return null;
        }
    }
}
