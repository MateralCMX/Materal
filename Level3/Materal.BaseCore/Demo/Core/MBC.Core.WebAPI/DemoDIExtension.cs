﻿using Materal.BaseCore.WebAPI;
using Materal.TTA.Common.Model;
using MBC.Core.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MBC.Core.WebAPI
{
    /// <summary>
    /// MBC依赖注入扩展
    /// </summary>
    public static class MBCDIExtension
    {
        /// <summary>
        /// 添加MBC服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMBCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, Action<SwaggerGenOptions> swaggerGenConfig, string[] swaggerXmlPaths, params Assembly[] repositories)
            where T : DbContext
        {
            services.AddDBService<T>(sqliteConfig, repositories);
            services.AddWebAPIService(config =>
            {
                swaggerGenConfig?.Invoke(config); 
                if (swaggerXmlPaths != null && swaggerXmlPaths.Length > 0)
                {
                    foreach (string path in swaggerXmlPaths)
                    {
                        config.IncludeXmlComments(path);
                    }
                }
            }, Assembly.Load("MBC.Core.WebAPI"));
            return services;
        }
    }
}
