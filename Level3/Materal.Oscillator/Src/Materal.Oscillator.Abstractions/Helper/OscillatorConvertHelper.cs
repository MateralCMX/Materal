using Materal.Oscillator.Abstractions.Factories;
using Materal.Oscillator.Abstractions.Models;

namespace Materal.Oscillator.Abstractions.Helper
{
    /// <summary>
    /// 转换帮助类
    /// </summary>
    public static class OscillatorConvertHelper
    {
        /// <summary>
        /// 根据接口转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ConvertToInterface<T>(string type, string data)
            where T : IOscillatorOperationModel<T>
        {
            IEnumerable<IOscillatorConvertFactory> oscillatorConvertFactories = OscillatorServices.Services.GetServices<IOscillatorConvertFactory>();
            foreach (IOscillatorConvertFactory factory in oscillatorConvertFactories)
            {
                T? result = factory.ConvertToInterface<T>(type, data);
                if (result is not null) return result;
            }
            throw new OscillatorException($"获取{typeof(T)}的实现类型{type}失败");
        }
    }
}
