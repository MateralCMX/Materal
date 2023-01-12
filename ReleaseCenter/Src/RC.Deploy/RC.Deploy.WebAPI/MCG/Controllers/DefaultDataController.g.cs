using Materal.BaseCore.WebAPI.Controllers;
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
