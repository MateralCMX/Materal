using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ApplicationInfoController(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
