namespace Materal.Oscillator.Abstractions.Models
{
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
