namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 构建Materal服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IServiceProvider BuildMateralServiceProvider(this IServiceCollection services, Func<Type, Type, bool>? filter = null)
        {
            services.AddSingleton<InterceptorHelper>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IServiceProvider result = new MateralServiceProvider(serviceProvider, filter);
            return result;
        }
        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptor<T>(this IServiceCollection services, Func<MethodInfo, MethodInfo, bool>? filter = null, int order = -1)
            where T : InterceptorAttribute
        {
            services.AddInterceptor(typeof(T), filter, order);
            return services;
        }
        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interceptorAttributeType"></param>
        /// <param name="filter"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptor(this IServiceCollection services, Type interceptorAttributeType, Func<MethodInfo, MethodInfo, bool>? filter = null, int order = -1)
        {
            if (!interceptorAttributeType.IsAssignableTo<InterceptorAttribute>()) return services;
            object? interceptorObj = Activator.CreateInstance(interceptorAttributeType);
            if (interceptorObj is null || interceptorObj is not InterceptorAttribute interceptor) return services;
            interceptor.Order = order;
            services.AddInterceptor(interceptor, filter);
            return services;
        }
        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interceptorAttribute"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptor(this IServiceCollection services, InterceptorAttribute interceptorAttribute, Func<MethodInfo, MethodInfo, bool>? filter = null)
        {
            services.AddInterceptor(new GolablInterceptorModel(interceptorAttribute, filter));
            return services;
        }
        /// <summary>
        /// 添加全局拦截器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="golablInterceptor"></param>
        /// <returns></returns>
        public static IServiceCollection AddInterceptor(this IServiceCollection services, GolablInterceptorModel golablInterceptor)
        {
            services.AddSingleton(golablInterceptor);
            return services;
        }
    }
}
