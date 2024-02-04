using RC.ServerCenter.Abstractions.DTO.Namespace;
using RC.ServerCenter.Abstractions.RequestModel.Namespace;

namespace RC.ServerCenter.Abstractions.Controllers
{
    /// <summary>
    /// 命名空间控制器
    /// </summary>
    public partial interface INamespaceController : IMergeBlockControllerBase<AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, NamespaceDTO, NamespaceListDTO>
    {
    }
}
