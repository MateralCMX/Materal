using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.RequestModel.DefaultData;
using RC.Deploy.Abstractions.Services.Models.DefaultData;

namespace RC.Deploy.Application.Controllers
{
    /// <summary>
    /// 默认数据控制器
    /// </summary>
    public partial class DefaultDataController : DeployController<AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO, IDefaultDataService>, IDefaultDataController
    {
    }
}
