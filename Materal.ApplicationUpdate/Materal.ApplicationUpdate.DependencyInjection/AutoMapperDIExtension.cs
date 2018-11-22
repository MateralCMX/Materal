using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.ApplicationUpdate.DependencyInjection
{
    public static class AutoMapperDIExtension
    {
        /// <summary>
        /// 使用AutoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperServices(this IServiceCollection services)
        {
            var automapperAssemblies = new List<Assembly>
            {
                Assembly.Load("Materal.ApplicationUpdate.Service"),
                Assembly.Load("Materal.ApplicationUpdate.WebAPI")
            };
            services.AddAutoMapper(automapperAssemblies);
        }
    }
}
