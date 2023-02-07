using Materal.BaseCore.ServiceImpl;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.Domain;
using RC.ServerCenter.Domain.Repositories;
using RC.ServerCenter.Services;
using RC.ServerCenter.Services.Models.Namespace;

namespace RC.ServerCenter.ServiceImpl
{
    /// <summary>
    /// 命名空间服务实现
    /// </summary>
    public partial class NamespaceServiceImpl : BaseServiceImpl<AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO, INamespaceRepository, Namespace>, INamespaceService
    {
    }
}
