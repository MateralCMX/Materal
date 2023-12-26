﻿namespace Materal.Logger
{
    /// <summary>
    /// Logger服务
    /// </summary>
    public static class LoggerServices
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        private static IServiceProvider? _services;
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceProvider Services { get => _services ?? throw new MateralException("服务容器未实例化"); set => _services = value; }
        /// <summary>
        /// 获得服务或者默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static T? GetService<T>()
        {
            try
            {
                return Services.GetService<T>();
            }
            catch
            {
                return default;
            }
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static T GetRequiredService<T>()
            where T : notnull
            => Services.GetRequiredService<T>();
        /// <summary>
        /// 获得服务或者默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object? GetService(Type type)
        {
            try
            {
                return Services.GetService(type);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MateralException"></exception>
        public static object GetRequiredService(Type type) => Services.GetRequiredService(type);
    }
}
