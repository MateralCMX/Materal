using Materal.Oscillator.Abstractions.Works;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 转换帮助类
    /// </summary>
    public static class OscillatorConvertHelper
    {
        /// <summary>
        /// 创建新任务
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static IWork CreateNewWork(IWorkData workData, IServiceProvider serviceProvider)
        {
            Type workType = workData.WorkTypeName.GetTypeByTypeFullName<IWork>() ?? throw new OscillatorException($"获取任务类型失败:{workData.WorkTypeName}");
            IWork work = workType.InstantiationOrDefault<IWork>(serviceProvider) ?? throw new OscillatorException($"实例化任务失败:{workData.WorkTypeName}");
            work.SetData(workData);
            return work;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<T> DeserializationAsync<T>(JToken jsonData, IServiceProvider serviceProvider)
            where T : IOscillatorData => await DeserializationAsync<T>(jsonData.ToString(), serviceProvider);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<T> DeserializationAsync<T>(string jsonData, IServiceProvider serviceProvider)
            where T : IOscillatorData
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
            where T : IOscillatorData
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
            string typeName = data[nameof(IOscillatorData.TypeName)]?.ToString() ?? throw new OscillatorException($"反序列化失败:{nameof(IOscillatorData.TypeName)}");
            return typeName;
        }
    }
}
