using RC.ServerCenter.Abstractions.DTO.Namespace;
using RC.ServerCenter.Abstractions.RequestModel.Namespace;

namespace RC.ServerCenter.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 命名空间控制器访问器
    /// </summary>
    public partial class NamespaceControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<INamespaceController, AddNamespaceRequestModel, EditNamespaceRequestModel, QueryNamespaceRequestModel, NamespaceDTO, NamespaceListDTO>(serviceProvider), INamespaceController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "ServerCenter";
    }
}
