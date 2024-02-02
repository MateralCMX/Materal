using Materal.MergeBlock.Abstractions.Services;
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models.ApplicationInfo;

namespace RC.Deploy.Abstractions.Services
{
    /// <summary>
    /// 应用程序信息服务
    /// </summary>
    public partial interface IApplicationInfoService : IBaseService<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
    }
}
