using Materal.BaseCore.WebAPI.Controllers;
using RC.Deploy.DataTransmitModel.ApplicationInfo;
using RC.Deploy.PresentationModel.ApplicationInfo;
using RC.Deploy.Services;
using RC.Deploy.Services.Models.ApplicationInfo;

namespace RC.Deploy.WebAPI.Controllers
{
    /// <summary>
    /// 应用程序信息控制器
    /// </summary>
    public partial class ApplicationInfoController : MateralCoreWebAPIServiceControllerBase<AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO, IApplicationInfoService>
    {
    }
}
