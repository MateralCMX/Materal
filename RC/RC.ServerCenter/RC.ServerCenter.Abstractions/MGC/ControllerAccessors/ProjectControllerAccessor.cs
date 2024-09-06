/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessorAsync
 */
using RC.ServerCenter.Abstractions.DTO.Project;
using RC.ServerCenter.Abstractions.RequestModel.Project;

namespace RC.ServerCenter.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 项目控制器访问器
    /// </summary>
    public partial class ProjectControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<IProjectController, AddProjectRequestModel, EditProjectRequestModel, QueryProjectRequestModel, ProjectDTO, ProjectListDTO>(serviceProvider), IProjectController
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
