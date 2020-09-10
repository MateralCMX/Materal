using ConfigCenter.Environment.Common;
using ConfigCenter.Hubs.Clients;
using ConfigCenter.Hubs.Hubs;
using ConfigCenter.PresentationModel.ConfigCenter;
using Materal.APP.Hubs.Clients;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using ConfigCenter.Environment.Services;

namespace ConfigCenter.Environment.HubImpl.ConfigCenterHub
{
    public class ConfigCenterClientImpl : BaseClientImpl, IConfigCenterClient
    {
        private readonly IConfigurationItemService _configurationItemService;
        public ConfigCenterClientImpl(IConfigurationItemService configurationItemService) : base($"{ConfigCenterEnvironmentConfig.ConfigCenterUrl}/ConfigCenterHub")
        {
            _configurationItemService = configurationItemService;
            Connection.On<bool, string>(nameof(RegisterResult), RegisterResult);
            Connection.On<Guid>(nameof(DeleteProject), DeleteProject);
            Connection.On<Guid>(nameof(DeleteNamespace), DeleteNamespace);
        }

        public Task RegisterResult(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                ConfigCenterEnvironmentConsoleHelper.WriteLine("注册成功");
            }
            else
            {
                ConfigCenterEnvironmentConsoleHelper.WriteLine(message, "注册失败", ConsoleColor.Red);
            }
            return Task.CompletedTask;
        }

        public async Task DeleteProject(Guid id)
        {
            await _configurationItemService.DeleteConfigurationItemByProjectIDAsync(id);
        }

        public async Task DeleteNamespace(Guid id)
        {
            await _configurationItemService.DeleteConfigurationItemByNamespaceIDAsync(id);
        }
    }
}
