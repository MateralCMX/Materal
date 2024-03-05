using Materal.Oscillator.Abstractions.Models;

namespace Materal.Oscillator.Abstractions.Factories
{
    /// <summary>
    /// Oscillator转换工厂
    /// </summary>
    public class OscillatorConvertFactory : IOscillatorConvertFactory
    {
        /// <summary>
        /// 根据接口转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public T? ConvertToInterface<T>(string type, string data)
            where T : IOscillatorOperationModel<T>
        {
            T? result = default;
            foreach (Assembly assembly in OscillatorServices.WorkAssemblies)
            {
                result = type.GetObjectByTypeName<T>(assembly, OscillatorServices.Services);
                if (result is not null) break;
            }
            if (result is null || result is not IOscillatorOperationModel<T> t) return default;
            result = t.Deserialization(data);
            return result;
        }
    }
}
