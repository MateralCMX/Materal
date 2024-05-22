using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// Osciilator数据
    /// </summary>
    public interface IOscillatorData
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
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        Task DeserializationAsync(JObject data, IServiceProvider serviceProvider);
    }
}
