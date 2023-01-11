using Materal.BaseCore.ServiceImpl;
using RC.Deploy.DataTransmitModel.DefaultData;
using RC.Deploy.Domain;
using RC.Deploy.Domain.Repositories;
using RC.Deploy.Services;
using RC.Deploy.Services.Models.DefaultData;

namespace RC.Deploy.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class DefaultDataServiceImpl : BaseServiceImpl<AddDefaultDataModel, EditDefaultDataModel, QueryDefaultDataModel, DefaultDataDTO, DefaultDataListDTO, IDefaultDataRepository, DefaultData>, IDefaultDataService
    {
    }
}
