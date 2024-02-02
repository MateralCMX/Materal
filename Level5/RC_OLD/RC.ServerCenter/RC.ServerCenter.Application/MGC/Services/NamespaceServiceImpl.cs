using RC.ServerCenter.Abstractions.DTO.Namespace;
using RC.ServerCenter.Abstractions.Services.Models.Namespace;

namespace RC.ServerCenter.Application.Services
{
    /// <summary>
    /// 命名空间服务
    /// </summary>
    public partial class NamespaceServiceImpl : BaseServiceImpl<AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO, INamespaceRepository, Namespace>, INamespaceService
    {
    }
}
