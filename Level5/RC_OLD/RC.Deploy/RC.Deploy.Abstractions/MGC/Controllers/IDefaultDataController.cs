using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.RequestModel.DefaultData;

namespace RC.Deploy.Abstractions.Controllers
{
    /// <summary>
    /// 默认数据控制器
    /// </summary>
    public partial interface IDefaultDataController : IMergeBlockControllerBase<AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, DefaultDataDTO, DefaultDataListDTO>
    {
    }
}
