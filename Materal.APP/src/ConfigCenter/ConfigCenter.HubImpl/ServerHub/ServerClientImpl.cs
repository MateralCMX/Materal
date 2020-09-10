using ConfigCenter.Common;
using Materal.APP.Core;
using Materal.APP.Hubs.Clients;
using Materal.APP.Hubs.Hubs;
using Materal.APP.PresentationModel.Server;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ConfigCenter.HubImpl.ServerHub
{
    public class ServerClientImpl : BaseClientImpl, IServerClient
    {
        private IServerHub _serverHub;
        public ServerClientImpl() : base($"{ApplicationConfig.ServerInfo.Url}/ServerHub")
        {
            Connection.On<bool>(nameof(RegisterResult), RegisterResult);
        }
        /// <summary>
        /// 设置Hub实现
        /// </summary>
        /// <param name="serverHub"></param>
        public void SetHubImpl(IServerHub serverHub)
        {
            _serverHub = serverHub;
        }
        public async Task RegisterResult(bool isSuccess)
        {
            if (isSuccess)
            {
                ConfigCenterConsoleHelper.WriteLine("注册成功");
            }
            else
            {
                if (_serverHub == null)
                {
                    ConfigCenterConsoleHelper.WriteLine("注册失败");
                }
                else
                {
                    ConfigCenterConsoleHelper.WriteLine("注册失败,5秒后重新注册");
                    RegisterRequestModel registerModel = _serverHub.GetRegisterModel();
                    await _serverHub.Register(registerModel);
                }
            }
        }
    }
}
