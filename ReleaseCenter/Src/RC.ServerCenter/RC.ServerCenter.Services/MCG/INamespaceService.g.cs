using Materal.BaseCore.Services;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.Services.Models.Namespace;

namespace RC.ServerCenter.Services
{
    /// <summary>
    /// 服务
    /// </summary>
    public partial interface INamespaceService : IBaseService<AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO>
    {
    }
}
