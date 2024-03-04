/*
 * Generator Code From MateralMergeBlock=>GeneratorControllersCode
 */
using RC.Deploy.Abstractions.DTO.ApplicationInfo;
using RC.Deploy.Abstractions.RequestModel.ApplicationInfo;
using RC.Deploy.Abstractions.Services.Models.ApplicationInfo;

namespace RC.Deploy.Application.Controllers
{
    /// <summary>
    /// 应用程序信息控制器
    /// </summary>
    public partial class ApplicationInfoController : DeployController<AddApplicationInfoRequestModel, EditApplicationInfoRequestModel, QueryApplicationInfoRequestModel, AddApplicationInfoModel, EditApplicationInfoModel, QueryApplicationInfoModel, ApplicationInfoDTO, ApplicationInfoListDTO, IApplicationInfoService>, IApplicationInfoController
    {
    }
}
