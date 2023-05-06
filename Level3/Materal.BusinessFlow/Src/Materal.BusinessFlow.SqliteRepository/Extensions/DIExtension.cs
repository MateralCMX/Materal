using Materal.BusinessFlow.SqliteRepository;
using Materal.TTA.SqliteADONETRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        public static IServiceCollection AddBusinessFlowSqliteRepository(this IServiceCollection services, BusinessFlowSqliteDBOption dbOption)
        {
            services.AddTTASqliteADONETRepository(dbOption);
            return services;
        }
    }
}
