using Materal.TTA.EFRepository;
using RC.Deploy.Enums;

namespace RC.Deploy.Domain.Repositories
{
    /// <summary>
    /// 应用程序信息仓储接口
    /// </summary>
    public partial interface IApplicationInfoRepository : IEFRepository<ApplicationInfo, Guid>
    {
    }
}
