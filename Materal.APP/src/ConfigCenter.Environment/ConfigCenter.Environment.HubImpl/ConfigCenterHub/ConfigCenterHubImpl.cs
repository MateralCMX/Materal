using ConfigCenter.Environment.Common;
using ConfigCenter.Hubs.Clients;
using ConfigCenter.Hubs.Hubs;
using ConfigCenter.PresentationModel.ConfigCenter;
using Materal.APP.Core;
using Materal.APP.Hubs.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.HubImpl.ConfigCenterHub
{
    public class ConfigCenterHubImpl : BaseHubImpl, IConfigCenterHub
    {
        private readonly IConfigCenterClient _serverClient;
        protected override HubConnection Connection => GetHubConnection(_serverClient);

        public ConfigCenterHubImpl(IConfigCenterClient serverClient)
        {
            _serverClient = serverClient;
            if (!(_serverClient is ConfigCenterClientImpl serverClientImpl)) return;
            serverClientImpl.SetHubImpl(this);
            Task task = Task.Run(async () =>
            {
                await serverClientImpl.ConnectWithRetryAsync();
            });
            Task.WaitAll(task);
        }

        public async Task RegisterEnvironment(RegisterEnvironmentRequestModel requestModel)
        {
            await Connection.InvokeAsync(nameof(RegisterEnvironment), requestModel);
        }

        public RegisterEnvironmentRequestModel GetRegisterModel()
        {
            return new RegisterEnvironmentRequestModel
            {
                Name = ConfigCenterEnvironmentConfig.EnvironmentName,
                Url = ApplicationConfig.Url,
                Key = ApplicationConfig.ServerInfo.Key
            };
        }
    }
}
