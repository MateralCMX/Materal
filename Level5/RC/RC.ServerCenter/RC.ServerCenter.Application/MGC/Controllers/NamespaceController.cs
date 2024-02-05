using RC.ServerCenter.Abstractions.DTO.Namespace;
using RC.ServerCenter.Abstractions.RequestModel.Namespace;
using RC.ServerCenter.Abstractions.Services.Models.Namespace;

namespace RC.ServerCenter.Application.Controllers
{
    /// <summary>
    /// 命名空间控制器
    /// </summary>
    public partial class NamespaceController : ServerCenterController<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO, INamespaceService>, INamespaceController
    {
    }
}
