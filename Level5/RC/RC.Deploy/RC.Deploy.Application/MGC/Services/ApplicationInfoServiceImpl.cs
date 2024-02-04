using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models.ApplicationInfo;

namespace RC.Deploy.Application.Services
{
    /// <summary>
    /// 应用程序信息服务
    /// </summary>
    public partial class ApplicationInfoServiceImpl : BaseServiceImpl<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO, IApplicationInfoRepository, ApplicationInfo, IDeployUnitOfWork>, IApplicationInfoService, IScopedDependencyInjectionService<IApplicationInfoService>
    {
    }
}
