namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 操作模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOscillatorOperationModel<T>
    {
        /// <summary>
        /// 反序列化为计划触发器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialization(string data);
    }
}
