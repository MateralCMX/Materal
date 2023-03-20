using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;

namespace RC.EnvironmentServer.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 配置项仓储实现
    /// </summary>
    public partial class ConfigurationItemRepositoryImpl: RCEFRepositoryImpl<ConfigurationItem, Guid>, IConfigurationItemRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConfigurationItemRepositoryImpl(EnvironmentServerDBContext dbContext) : base(dbContext) { }
    }
}
