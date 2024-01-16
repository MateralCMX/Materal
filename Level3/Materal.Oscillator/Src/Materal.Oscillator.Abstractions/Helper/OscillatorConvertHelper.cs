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
        {
            T tEntity = type.GetObjectByTypeName<T>() ?? throw new OscillatorException($"获取{typeof(T)}的实现类型{type}失败");
            MethodInfo methodInfo = tEntity.GetType().GetMethod(nameof(IOscillatorOperationModel<T>.Deserialization), new[] { typeof(string) }) ?? throw new OscillatorException($"获取{tEntity.GetType().Name}反序列化方法失败");
            object? resultObj = methodInfo.Invoke(tEntity, new[] { data });
            if (resultObj is not T result) throw new OscillatorException($"反序列化失败");
            return result;
        }
    }
}
