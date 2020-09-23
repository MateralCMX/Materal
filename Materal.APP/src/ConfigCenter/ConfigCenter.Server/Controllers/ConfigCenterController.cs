using AutoMapper;
using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.HttpManage;
using ConfigCenter.Hubs.Clients;
using ConfigCenter.PresentationModel.ConfigCenter;
using ConfigCenter.Server.Hubs;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.ConfigCenter;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;

namespace ConfigCenter.Server.Controllers
{
    /// <summary>
    /// 配置中心控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ConfigCenterController : WebAPIControllerBase, IConfigCenterManage
    {
        private readonly IConfigCenterService _configCenterService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ConfigCenterHub, IConfigCenterClient> _configCenterHubContext;

        /// <summary>
        /// 配置中心控制器
        /// </summary>
        public ConfigCenterController(IConfigCenterService configCenterService, IHubContext<ConfigCenterHub, IConfigCenterClient> configCenterHubContext, IMapper mapper)
        {
            _configCenterService = configCenterService;
            _configCenterHubContext = configCenterHubContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public Task<ResultModel<List<EnvironmentListDTO>>> GetEnvironmentListAsync()
        {
            List<EnvironmentListDTO> result = _configCenterService.GetEnvironmentList();
            return Task.FromResult(ResultModel<List<EnvironmentListDTO>>.Success(result, "获取成功"));
        }
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> SyncAsync(SyncRequestModel requestModel)
        {
            var model = _mapper.Map<SyncModel>(requestModel);
            List<AddConfigurationItemRequestModel> configurationItems = await _configCenterService.GetSyncConfigurationItemModelAsync(model);
            await _configCenterHubContext.Clients.All.SyncConfigurationItem(requestModel.SourceKey, requestModel.TargetKeys, configurationItems);
            return ResultModel.Success("同步成功");
        }
    }
}
