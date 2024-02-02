using Materal.MergeBlock.Abstractions.Services;
using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.Services.Models.DefaultData;

namespace RC.Deploy.Abstractions.Services
{
    /// <summary>
    /// 默认数据服务
    /// </summary>
    public partial interface IDefaultDataService : IBaseService<AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO>
    {
    }
}
