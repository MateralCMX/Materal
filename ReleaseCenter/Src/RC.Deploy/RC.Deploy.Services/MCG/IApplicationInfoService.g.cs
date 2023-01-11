using Materal.BaseCore.Services;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Services.Models.ApplicationInfo;

namespace RC.Deploy.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IApplicationInfoService : IBaseService<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO>
    {
    }
}
