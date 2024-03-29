using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using RC.ServerCenter.DataTransmitModel.Namespace;
using RC.ServerCenter.Services.Models.Namespace;

namespace RC.ServerCenter.Services
{
    /// <summary>
    /// 命名空间服务
    /// </summary>
    public partial interface INamespaceService : IBaseService<AddNamespaceModel, EditNamespaceModel, QueryNamespaceModel, NamespaceDTO, NamespaceListDTO>
    {
    }
}
