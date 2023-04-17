using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Repositories;
using Materal.BusinessFlow.SqlServerRepository;
using Materal.BusinessFlow.SqlServerRepository.Models;
using Materal.BusinessFlow.SqlServerRepository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.BusinessFlow
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        public static IServiceCollection AddBusinessFlowSqlServerRepository(this IServiceCollection services, SqlServerConfigModel dbConfigModel)
        {
            services.TryAddScoped<IDataModelRepository, DataModelRepositoryImpl>();
            services.TryAddScoped<IDataModelFieldRepository, DataModelFieldRepositoryImpl>();
            services.TryAddScoped<IFlowTemplateRepository, FlowTemplateRepositoryImpl>();
            services.TryAddScoped<INodeRepository, NodeRepositoryImpl>();
            services.TryAddScoped<IStepRepository, StepRepositoryImpl>();
            services.TryAddScoped<IUserRepository, UserRepositoryImpl>();
            services.TryAddScoped<IFlowRepository, FlowRepositoryImpl>();
            services.TryAddScoped<IFlowRecordRepository, FlowRecordRepositoryImpl>();
            services.TryAddScoped<IFlowRecordRepository, FlowRecordRepositoryImpl>();
            services.TryAddScoped(typeof(IRepositoryHelper<>), typeof(SqlServerRepositoryHelper<>));
            services.TryAddSingleton(dbConfigModel);
            services.TryAddScoped<IUnitOfWork, UnitOfWorkImpl>();
            return services;
        }
    }
}
