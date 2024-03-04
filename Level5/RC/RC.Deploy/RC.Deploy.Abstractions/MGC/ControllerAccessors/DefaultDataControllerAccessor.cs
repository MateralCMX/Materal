/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessor
 */
using RC.Deploy.Abstractions.DTO.DefaultData;
using RC.Deploy.Abstractions.RequestModel.DefaultData;

namespace RC.Deploy.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 默认数据控制器访问器
    /// </summary>
    public partial class DefaultDataControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<IDefaultDataController, AddDefaultDataRequestModel, EditDefaultDataRequestModel, QueryDefaultDataRequestModel, DefaultDataDTO, DefaultDataListDTO>(serviceProvider), IDefaultDataController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "Deploy";
    }
}
