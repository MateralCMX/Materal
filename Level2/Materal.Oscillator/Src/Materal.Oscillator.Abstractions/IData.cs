using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// Osciilator数据
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        Task<string> SerializationAsync();
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<T> DeserializationAsync<T>(JToken jsonData, IServiceProvider serviceProvider)
            where T : IData => await DeserializationAsync<T>(jsonData.ToString(), serviceProvider);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<T> DeserializationAsync<T>(string jsonData, IServiceProvider serviceProvider)
            where T : IData
        {
            string typeName = GetTypeName(jsonData);
            T result = await DeserializationAsync<T>(typeName, jsonData, serviceProvider);
            return result;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static async Task<T> DeserializationAsync<T>(string typeName, string jsonData, IServiceProvider serviceProvider)
            where T : IData
        {
            Type type = typeName.GetTypeByTypeFullName<T>() ?? throw new OscillatorException($"获取类型失败:{typeName}:{typeof(T).FullName}");
            T result = type.Instantiation<T>(serviceProvider);
            await result.DeserializationAsync(jsonData, serviceProvider);
            return result;
        }
        /// <summary>
        /// 获得类型名称
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        private static string GetTypeName(string jsonData)
        {
            JObject data = JObject.Parse(jsonData);
            string typeName = data[nameof(TypeName)]?.ToString() ?? throw new OscillatorException($"反序列化失败:{nameof(TypeName)}");
            return typeName;
        }
    }
}
