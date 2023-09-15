using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Services.Models.ApplicationInfo;

namespace RC.Deploy.Services
{
    /// <summary>
    /// 应用程序信息服务
    /// </summary>
    public partial interface IApplicationInfoService : IBaseService<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
    }
}
