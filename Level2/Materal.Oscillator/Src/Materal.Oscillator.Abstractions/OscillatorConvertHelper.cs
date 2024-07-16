﻿using Materal.Oscillator.Abstractions.Works;
using Newtonsoft.Json;
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
            Type workType = workData.WorkTypeName.GetTypeByTypeFullName<IWork>() ?? throw new OscillatorException($"获取任务类型失败:{workData.WorkTypeName}"); ;
            IWork work = workType.InstantiationOrDefault<IWork>(serviceProvider) ?? throw new OscillatorException($"实例化任务失败:{workData.WorkTypeName}");
            work.SetData(workData);
            return work;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static async Task<T> DeserializationAsync<T>(string data, IServiceProvider serviceProvider)
            where T : IOscillatorData
        {
            JToken jToken = JsonConvert.DeserializeObject<JToken>(data) ?? throw new OscillatorException("反序列化失败");
            if (jToken is not JObject jObject) throw new OscillatorException("反序列化失败");
            return await DeserializationAsync<T>(jObject, serviceProvider);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static async Task<T> DeserializationAsync<T>(JObject data, IServiceProvider serviceProvider)
            where T : IOscillatorData
        {
            string typeName = data[nameof(IOscillatorData.TypeName)]?.ToString() ?? throw new OscillatorException($"反序列化失败:{nameof(IOscillatorData.TypeName)}");
            return await DeserializationAsync<T>(typeName, data, serviceProvider);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static async Task<T> DeserializationAsync<T>(string typeName, JObject data, IServiceProvider serviceProvider)
            where T : IOscillatorData
        {
            Type type = typeName.GetTypeByTypeFullName<T>() ?? throw new OscillatorException($"获取{typeof(T)}的实现类型{typeName}失败");
            T result = type.Instantiation<T>(serviceProvider);
            await result.DeserializationAsync(data, serviceProvider);
            return result;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName"></param>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public static async Task<T> DeserializationAsync<T>(string typeName, string data, IServiceProvider serviceProvider)
            where T : IOscillatorData
        {
            JToken jToken = JsonConvert.DeserializeObject<JToken>(data) ?? throw new OscillatorException("反序列化失败");
            if (jToken is not JObject jObject) throw new OscillatorException("反序列化失败");
            return await DeserializationAsync<T>(typeName, jObject, serviceProvider);
        }
    }
}
