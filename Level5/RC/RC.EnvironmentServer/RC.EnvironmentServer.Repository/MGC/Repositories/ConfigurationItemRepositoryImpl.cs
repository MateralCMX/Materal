﻿/*
 * Generator Code From MateralMergeBlock=>GeneratorRepositoryImplCode
 */
namespace RC.EnvironmentServer.Repository.Repositories
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public partial class ConfigurationItemRepositoryImpl(EnvironmentServerDBContext dbContext) : EnvironmentServerRepositoryImpl<ConfigurationItem>(dbContext), IConfigurationItemRepository, IScopedDependencyInjectionService<IConfigurationItemRepository>
    {
    }
}
