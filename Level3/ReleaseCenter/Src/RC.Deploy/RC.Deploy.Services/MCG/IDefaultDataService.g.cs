using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.Services.Models.DefaultData;

namespace RC.Deploy.Services
{
    /// <summary>
    /// 默认数据服务
    /// </summary>
    public partial interface IDefaultDataService : IBaseService<AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO>
    {
    }
}
