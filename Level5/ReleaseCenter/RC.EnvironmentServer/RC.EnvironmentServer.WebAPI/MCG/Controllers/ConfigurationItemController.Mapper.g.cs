#nullable enable
using Materal.BaseCore.PresentationModel;
using Materal.BaseCore.Services.Models;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.CodeGenerator;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.EnvironmentServer.WebAPI.Controllers
{
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
            var model0 = Mapper.Map<SyncConfigModel>(requestModel);
            await DefaultService.SyncConfigAsync(model0);
            return ResultModel.Success();
        }
    }
}
