using Materal.BaseCore.Services;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.Services.Models.DefaultData;

namespace RC.Deploy.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface IDefaultDataService : IBaseService<AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO>
    {
    }
}
