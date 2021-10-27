using AutoMapper;
using ConfigCenter.Common;
using ConfigCenter.Hubs.Clients;
using ConfigCenter.Hubs.Hubs;
using ConfigCenter.PresentationModel.ConfigCenter;
using ConfigCenter.Services;
using ConfigCenter.Services.Models.ConfigCenter;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ConfigCenter.Server.Hubs
{
    /// <summary>
    /// 配置中心Hub
    /// </summary>
    public class ConfigCenterHub : Hub<IConfigCenterClient>, IConfigCenterHub
    {
        private readonly IMapper _mapper;
        private readonly IConfigCenterService _configCenterService;

        /// <summary>
        /// 配置中心Hub
        /// </summary>
        public ConfigCenterHub(IMapper mapper, IConfigCenterService configCenterService)
        {
            _mapper = mapper;
            _configCenterService = configCenterService;
        }

        /// <summary>
        /// 连接丢失
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _configCenterService.UnRegisterEnvironment(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 注册环境
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task RegisterEnvironment(RegisterEnvironmentRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<RegisterEnvironmentModel>(requestModel);
                _configCenterService.RegisterEnvironment(Context.ConnectionId, model);
                await Clients.Caller.RegisterResult(true, "注册成功");
            }
            catch (ConfigCenterException exception)
            {
                await Clients.Caller.RegisterResult(false, exception.Message);
            }
        }
    }
}
