using Materal.APP.Core;
using Materal.APP.Enums;
using Materal.APP.Hubs.Clients;
using Materal.APP.Hubs.Hubs;
using Materal.APP.PresentationModel.Server;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Authority.Common;

namespace Authority.HubImpl.ServerHub
{
    public class ServerHubImpl : BaseHubImpl, IServerHub
    {
        private readonly IServerClient _serverClient;
        protected override HubConnection Connection => GetHubConnection(_serverClient);

        public ServerHubImpl(IServerClient serverClient)
        {
            _serverClient = serverClient;
            if (!(_serverClient is ServerClientImpl serverClientImpl)) return;
            serverClientImpl.SetConnectSuccessLaterAction(ServerClientImpl_OnConnectSuccess);
            Task task = Task.Run(async () =>
            {
                await serverClientImpl.ConnectWithRetryAsync();
            });
            Task.WaitAll(task);
        }

        private async Task ServerClientImpl_OnConnectSuccess()
        {
            var registerModel = new RegisterRequestModel
            {
                Name = AuthorityConfig.ServerInfo.Name,
                Url = ApplicationConfig.PublicUrl,
                ServerCategory = ServerCategoryEnum.Authority,
                Key = AuthorityConfig.ServerInfo.Key
            };
            await Register(registerModel);
        }

        public async Task Register(RegisterRequestModel requestModel)
        {
            await Connection.InvokeAsync(nameof(Register), requestModel);
        }
    }
}
