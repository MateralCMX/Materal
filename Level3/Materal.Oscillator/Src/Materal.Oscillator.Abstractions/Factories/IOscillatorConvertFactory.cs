using Materal.Oscillator.Abstractions.Models;

namespace Materal.Oscillator.Abstractions.Factories
{
    /// <summary>
    /// Oscillator转换工厂
    /// </summary>
    public interface IOscillatorConvertFactory
    {
        /// <summary>
        /// 根据接口转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        T? ConvertToInterface<T>(string type, string data) where T : IOscillatorOperationModel<T>;
    }
}
