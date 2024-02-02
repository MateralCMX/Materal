using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.Services.Models.DefaultData;

namespace RC.Deploy.Application.Services
{
    /// <summary>
    /// 默认数据服务
    /// </summary>
    public partial class DefaultDataServiceImpl : BaseServiceImpl<AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO, IDefaultDataRepository, DefaultData>, IDefaultDataService
    {
    }
}
