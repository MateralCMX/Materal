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
        private IConfigCenterHub _configCenterHub;
        private readonly IConfigurationItemService _configurationItemService;
        public ConfigCenterClientImpl(IConfigurationItemService configurationItemService) : base($"{ConfigCenterEnvironmentConfig.ConfigCenterUrl}/ConfigCenterHub")
        {
            _configurationItemService = configurationItemService;
            Connection.On<bool>(nameof(RegisterResult), RegisterResult);
            Connection.On<Guid>(nameof(DeleteProject), DeleteProject);
            Connection.On<Guid>(nameof(DeleteNamespace), DeleteNamespace);
        }
        /// <summary>
        /// 设置Hub实现
        /// </summary>
        /// <param name="configCenterHub"></param>
        public void SetHubImpl(IConfigCenterHub configCenterHub)
        {
            _configCenterHub = configCenterHub;
        }

        public async Task RegisterResult(bool isSuccess)
        {
            if (isSuccess)
            {
                ConfigCenterEnvironmentConsoleHelper.WriteLine("注册成功");
            }
            else
            {
                if (_configCenterHub == null)
                {
                    ConfigCenterEnvironmentConsoleHelper.WriteLine("注册失败");
                }
                else
                {
                    ConfigCenterEnvironmentConsoleHelper.WriteLine("注册失败,5秒后重新注册");
                    RegisterEnvironmentRequestModel registerModel = _configCenterHub.GetRegisterModel();
                    await _configCenterHub.RegisterEnvironment(registerModel);
                }
            }
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
