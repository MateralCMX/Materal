using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.PresentationModel.DefaultData;
using RC.Deploy.Services;
using RC.Deploy.Services.Models.DefaultData;

namespace RC.Deploy.WebAPI.Controllers
{
    /// <summary>
    /// 默认数据控制器
    /// </summary>
    public partial class DefaultDataController : MateralCoreWebAPIServiceControllerBase<AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO, IDefaultDataService>
    {
    }
}
