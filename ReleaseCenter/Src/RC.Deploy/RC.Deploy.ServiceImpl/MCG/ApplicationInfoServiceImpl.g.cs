using Materal.BaseCore.ServiceImpl;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;
using RC.Deploy.Services;
using RC.Deploy.Services.Models.ApplicationInfo;

namespace RC.Deploy.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class ApplicationInfoServiceImpl : BaseServiceImpl<AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO, IApplicationInfoRepository, ApplicationInfo>, IApplicationInfoService
    {
    }
}
