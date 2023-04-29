using Materal.Oscillator.Abstractions.Models;
using System.Reflection;

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
        {
            T result = type.GetObjectByTypeName<T>() ?? throw new OscillatorException($"获取{typeof(T)}的实现类型{type}失败");
            MethodInfo methodInfo = result.GetType().GetMethod(nameof(IOscillatorOperationModel<T>.Deserialization), new[] { typeof(string) }) ?? throw new OscillatorException($"获取{result.GetType().Name}反序列化方法失败");
            result = (T)methodInfo.Invoke(result, new[] { data });
            return result;
        }
    }
}
