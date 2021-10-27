using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;
using ConfigCenter.Environment.Services;
using ConfigCenter.Hubs.Clients;
using Materal.APP.Hubs.Clients;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.APP.Core;
using AutoMapper;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;

namespace ConfigCenter.Environment.HubImpl.ConfigCenterHub
{
    public class ConfigCenterClientImpl : BaseClientImpl, IConfigCenterClient
    {
        private readonly IConfigurationItemService _configurationItemService;
        private readonly IMapper _mapper;
        public ConfigCenterClientImpl(IConfigurationItemService configurationItemService, IMapper mapper) : base($"{ConfigCenterEnvironmentConfig.ConfigCenterUrl}/ConfigCenterHub")
        {
            _configurationItemService = configurationItemService;
            _mapper = mapper;
            Connection.On<bool, string>(nameof(RegisterResult), RegisterResult);
            Connection.On<Guid>(nameof(DeleteProject), DeleteProject);
            Connection.On<Guid>(nameof(DeleteNamespace), DeleteNamespace);
            Connection.On<string, ICollection<string>, ICollection<AddConfigurationItemRequestModel>>(nameof(SyncConfigurationItem), SyncConfigurationItem);
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

        public async Task SyncConfigurationItem(string key, ICollection<string> targetKeys, ICollection<AddConfigurationItemRequestModel> configurationItems)
        {
            if (key == ApplicationConfig.PublicUrl) return;
            if (!targetKeys.Contains(ApplicationConfig.PublicUrl)) return;
            var model = _mapper.Map<List<AddConfigurationItemModel>>(configurationItems);
            await _configurationItemService.InitConfigurationItemsAsync(model);
        }
    }
}
