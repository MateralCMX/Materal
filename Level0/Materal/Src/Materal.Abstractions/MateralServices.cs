using Microsoft.Extensions.DependencyInjection;

namespace Materal.Abstractions
{
    /// <summary>
    /// Materal服务
    /// </summary>
    public static class MateralServices
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceProvider? Services { get; set; }
        /// <summary>
        /// 获得服务或者默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static T? GetServiceOrDefatult<T>()
        {
            if (Services == null) throw new MateralException("容器未构建");
            T? result = Services.GetService<T>();
            return result;
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static T GetService<T>()
        {
            T? result = GetServiceOrDefatult<T>();
            return result ?? throw new MateralException("未找到对应服务");
        }
        /// <summary>
        /// 获得服务或者默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object? GetServiceOrDefatult(Type type)
        {
            if (Services == null) throw new MateralException("容器未构建");
            object? result = Services.GetService(type);
            return result;
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object GetService(Type type)
        {
            object? result = GetServiceOrDefatult(type);
            return result ?? throw new MateralException("未找到对应服务");
        }
    }
}
