﻿/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerMapperCodeAsync
 */
using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;

namespace RC.EnvironmentServer.Application.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    public partial class ConfigurationItemController
    {
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel)
        {
            SyncConfigModel model = Mapper.Map<SyncConfigModel>(requestModel) ?? throw new RCException("映射失败");
            BindLoginUserID(model);
            await DefaultService.SyncConfigAsync(model);
            return ResultModel.Success("同步配置成功");
        }
    }
}
