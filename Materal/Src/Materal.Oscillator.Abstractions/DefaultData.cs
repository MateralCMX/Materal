using Newtonsoft.Json;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 默认Oscillator数据
    /// </summary>
    public abstract class DefaultData : IData
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
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual async Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider)
        {
            JsonConvert.PopulateObject(jsonData, this);
            await Task.CompletedTask;
        }
    }
}
