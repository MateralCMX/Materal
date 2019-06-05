using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    /// <summary>
    /// 自动映射依赖注入
    /// </summary>
    public static class AutoMapperDIExtension
    {
        /// <summary>
        /// 添加自动映射服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddAutoMapperService(this IServiceCollection services, params Assembly[] assemblies)
        {
            var autoMapperAssemblies = new List<Assembly>();
            if (assemblies.Length > 0)
            {
                autoMapperAssemblies.AddRange(assemblies);
            }
            services.AddAutoMapper(autoMapperAssemblies);
        }
    }
}
