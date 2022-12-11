using Materal.ConvertHelper;
using Materal.Oscillator.Abstractions.Models;
using System.Reflection;

namespace Materal.Oscillator.AutoMapperProfile
{
    public static class OscillatorConvertHelper
    {
        public static T? ConvertToInterface<T>(string type, string data)
        {
            Type? targetType = type.GetTypeByInterface<T>();
            if (targetType == null) return default;
            ConstructorInfo? constructorInfo = targetType.GetConstructor(Array.Empty<Type>());
            if (constructorInfo == null) return default;
            T? result = (T)constructorInfo.Invoke(null);
            MethodInfo? methodInfo = targetType.GetMethod(nameof(IOscillatorOperationModel<T>.Deserialization), new[] { typeof(string) });
            if (methodInfo == null) return default;
            result = (T?)methodInfo.Invoke(result, new[] { data });
            return result;
        }
    }
}
