using Materal.BaseCore.WebAPI;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RC.Core.EFRepository;
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
        /// <returns></returns>
        public static IServiceCollection AddRCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, params string[] swaggerXmlPaths)
            where T : DbContext
        {
            services.AddDBService<T>(sqliteConfig);
            services.AddWebAPIService(swaggerXmlPaths, Assembly.Load("RC.Core.WebAPI"));
            return services;
        }
    }
}
