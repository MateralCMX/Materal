using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;

namespace RC.Deploy.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 默认数据仓储实现
    /// </summary>
    public partial class DefaultDataRepositoryImpl: RCEFRepositoryImpl<DefaultData, Guid>, IDefaultDataRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultDataRepositoryImpl(DeployDBContext dbContext) : base(dbContext) { }
    }
}
