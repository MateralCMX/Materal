using Materal.BaseCore.WebAPI;
using Materal.TTA.Common.Model;
using MBC.Core.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        /// <param name="sqliteConfig"></param>
        /// <param name="swaggerXmlPaths"></param>
        /// <returns></returns>
        public static IServiceCollection AddMBCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, params string[] swaggerXmlPaths)
            where T : DbContext => AddMBCService<T>(services, sqliteConfig, null, swaggerXmlPaths);
        /// <summary>
        /// 添加MBC服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqliteConfig"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <param name="swaggerXmlPaths"></param>
        /// <returns></returns>
        public static IServiceCollection AddMBCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, Action<SwaggerGenOptions>? swaggerGenConfig, params string[] swaggerXmlPaths)
            where T : DbContext
        {
            services.AddDBService<T>(sqliteConfig);
            HttpMessageHandler httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, cert, chan, error) => true
            };
            HttpClient httpClient = new(httpClientHandler);
            services.AddSingleton(httpClient);
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
            }, null, Assembly.Load("MBC.Core.WebAPI"));
            return services;
        }
    }
}
