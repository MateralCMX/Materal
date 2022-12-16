using Materal.ConvertHelper;
using Materal.Oscillator.Abstractions.Models;
using System.Reflection;

namespace Materal.Oscillator.AutoMapperProfile
{
    public static class OscillatorConvertHelper
    {
        public static T? ConvertToInterface<T>(string type, string data)
        {
            T? result = type.GetObjectByTypeName<T>();
            if (result == null) return default;
            MethodInfo? methodInfo = result.GetType().GetMethod(nameof(IOscillatorOperationModel<T>.Deserialization), new[] { typeof(string) });
            if (methodInfo == null) return default;
            result = (T?)methodInfo.Invoke(result, new[] { data });
            return result;
        }
    }
}
