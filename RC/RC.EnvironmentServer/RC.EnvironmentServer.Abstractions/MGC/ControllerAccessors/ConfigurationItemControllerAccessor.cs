﻿/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessorAsync
 */
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 控制器访问器
    /// </summary>
    public partial class ConfigurationItemControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<IConfigurationItemController, AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, ConfigurationItemDTO, ConfigurationItemListDTO>(serviceProvider), IConfigurationItemController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "EnvironmentServer";
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel)
            => await HttpHelper.SendAsync<IConfigurationItemController, ResultModel>(ProjectName, ModuleName, nameof(SyncConfigAsync), [], requestModel);
    }
}
