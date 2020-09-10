using Materal.APP.Common;
using Materal.APP.Hubs.Clients;
using Materal.APP.Hubs.Hubs;
using Materal.Common;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.APP.PresentationModel.Server;
using Materal.APP.Services;
using AutoMapper;
using Materal.APP.Services.Models.Server;

namespace Materal.APP.Server.Hubs
{
    /// <summary>
    /// 服务Hub
    /// </summary>
    public class ServerHub : Hub<IServerClient>, IServerHub
    {
        private readonly IMapper _mapper;
        private readonly IServerService _serverService;
        /// <summary>
        /// 服务Hub
        /// </summary>
        public ServerHub(IMapper mapper, IServerService serverService)
        {
            _mapper = mapper;
            _serverService = serverService;
        }

        /// <summary>
        /// 连接丢失
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _serverService.UnRegisterServer(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        /// <summary>
        /// 获得注册模型
        /// </summary>
        /// <returns></returns>
        public RegisterRequestModel GetRegisterModel() => null;
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task Register(RegisterRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<RegisterModel>(requestModel);
                bool result = _serverService.RegisterServer(Context.ConnectionId, model);
                await Clients.Caller.RegisterResult(result);
            }
            catch (Exception exception)
            {
                throw new HubException("注册失败", exception);
            }
        }
    }
}
