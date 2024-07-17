using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator
{
    /// <summary>
    /// 默认Oscillator数据
    /// </summary>
    public abstract class DefaultOscillatorData : IOscillatorData
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => GetType().FullName ?? throw new OscillatorException("获取类型完整名称失败");
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<string> SerializationAsync()
        {
            string json = JsonConvert.SerializeObject(this);
            return await Task.FromResult(json);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual async Task DeserializationAsync(JObject data, IServiceProvider serviceProvider)
        {
            object dataObj = data.ToObject(GetType()) ?? throw new OscillatorException("反序列化失败");
            dataObj.CopyProperties(this);
            await Task.CompletedTask;
        }
    }
}
