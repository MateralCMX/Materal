using Materal.BaseCore.WebAPI;
using Materal.TTA.Common.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RC.Core.EFRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RC.Core.WebAPI
{
    /// <summary>
    /// RC依赖注入扩展
    /// </summary>
    public static class RCDIExtension
    {
        /// <summary>
        /// 添加RC服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqliteConfig"></param>
        /// <param name="swaggerXmlPaths"></param>
        /// <returns></returns>
        public static IServiceCollection AddRCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, params string[] swaggerXmlPaths)
            where T : DbContext
        {
            return AddRCService<T>(services, sqliteConfig, null, swaggerXmlPaths);
        }
        /// <summary>
        /// 添加RC服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sqliteConfig"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <param name="swaggerXmlPaths"></param>
        /// <returns></returns>
        public static IServiceCollection AddRCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, Action<SwaggerGenOptions>? swaggerGenConfig, params string[] swaggerXmlPaths)
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
            }, null, Assembly.Load("RC.Core.WebAPI"));
            return services;
        }
    }
}
