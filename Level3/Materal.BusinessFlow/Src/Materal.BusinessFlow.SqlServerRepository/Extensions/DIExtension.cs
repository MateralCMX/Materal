using Materal.BusinessFlow.SqlServerRepository;
using Materal.TTA.SqlServerADONETRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        public static IServiceCollection AddBusinessFlowSqlServerRepository(this IServiceCollection services, BusinessFlowSqlServerDBOption dbOption)
        {
            services.AddTTASqlServerADONETRepository(dbOption);
            return services;
        }
    }
}
