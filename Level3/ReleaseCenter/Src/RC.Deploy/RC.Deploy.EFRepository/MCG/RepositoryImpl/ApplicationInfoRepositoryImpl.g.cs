using Microsoft.EntityFrameworkCore;
using RC.Core.EFRepository;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;
using RC.Deploy.Enums;

namespace RC.Deploy.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 应用程序信息仓储实现
    /// </summary>
    public partial class ApplicationInfoRepositoryImpl: RCEFRepositoryImpl<ApplicationInfo, Guid>, IApplicationInfoRepository
    {
    }
}
