using Materal.BaseCore.WebAPI;
using Materal.TTA.SqliteRepository.Model;
using MBC.Core.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddMBCService<T>(this IServiceCollection services, SqliteConfigModel sqliteConfig, params string[] swaggerXmlPaths)
            where T : DbContext
        {
            services.AddDBService<T>(sqliteConfig);
            services.AddWebAPIService(swaggerXmlPaths, Assembly.Load("MBC.Core.WebAPI"));
            return services;
        }
    }
}
