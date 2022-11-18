using LitJson;

namespace Materal.WeChatHelper
{
    public static class JsonDataExtension
    {
        public static int? GetInt(this JsonData jsonData, string key)
        {
            if (jsonData.ContainsKey(key) && jsonData[key].GetJsonType() == JsonType.Int)
            {
                return (int)jsonData[key];
            }
            return null;
        }
        public static string GetString(this JsonData jsonData, string key)
        {
            if (jsonData.ContainsKey(key) && jsonData[key].GetJsonType() == JsonType.String)
            {
                return (string)jsonData[key];
            }
            return null;
        }
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
