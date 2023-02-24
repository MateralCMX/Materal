using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.WebAPI.Controllers
{
    public partial class ConfigurationItemController
    {
        /// <summary>
        /// 获得列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task<PageResultModel<ConfigurationItemListDTO>> GetListAsync(QueryConfigurationItemRequestModel requestModel)
        {
            return base.GetListAsync(requestModel);
        }
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel)
        {
            SyncConfigModel model = Mapper.Map<SyncConfigModel>(requestModel);
            await DefaultService.SyncConfigAsync(model);
            return ResultModel.Success("开始同步，请稍后查看");
        }
    }
}
