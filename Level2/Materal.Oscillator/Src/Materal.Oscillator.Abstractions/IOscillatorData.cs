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
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider);
    }
}
