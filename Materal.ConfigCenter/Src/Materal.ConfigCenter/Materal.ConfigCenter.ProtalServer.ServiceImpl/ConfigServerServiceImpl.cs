using AutoMapper;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;
using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConfigCenter.ProtalServer.Services.Models;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl
{
    public class ConfigServerServiceImpl : IConfigServerService
    {
        private readonly Dictionary<string, ConfigServerModel> _configServers = new Dictionary<string, ConfigServerModel>();
        private readonly Timer healthTimer = new Timer(10000);
        private readonly IConfigServerRepository _configServerRepository;
        private readonly IMapper _mapper;
        public ConfigServerServiceImpl(IConfigServerRepository configServerRepository, IMapper mapper)
        {
            _configServerRepository = configServerRepository;
            _mapper = mapper;
            healthTimer.Elapsed += HealthTimer_Elapsed;
            healthTimer.Start();
        }
        public void RegisterNewClient(RegisterConfigServerModel model)
        {
            if (_configServers.ContainsKey(model.Name)) throw new MateralConfigCenterException("该配置服务已注册");
            var client = model.CopyProperties<ConfigServerModel>();
            _configServers.Add(client.Name, client);
            ConsoleHelper.ServerWriteLine($"配置服务{client.Name}({client.Address})注册成功,当前配置服务数量{_configServers.Count}");
        }
        public void DeleteProject(Guid id, string token)
        {
            RunTask(configServer => _configServerRepository.DeleteProjectAsync(configServer.Address, token, id));
        }
        public void DeleteNamespace(Guid id, string token)
        {
            RunTask(configServer => _configServerRepository.DeleteNamespaceAsync(configServer.Address, token, id));
        }
        public List<ConfigServerModel> GetConfigServers()
        {
            return _configServers.Select(m => m.Value).OrderBy(m => m.Name).ToList();
        }

        public async Task CopyConfigServer(CopyConfigServerModel model, string token)
        {
            if (!_configServers.ContainsKey(model.SourceConfigServerName)) throw new MateralConfigCenterException($"服务{model.SourceConfigServerName}不存在");
            var filterModel = new QueryConfigurationItemFilterModel();
            List<ConfigurationItemListDTO> configurationItems = await _configServerRepository.GetConfigurationItemAsync(filterModel, _configServers[model.SourceConfigServerName].Address);
            var addModels = _mapper.Map<List<AddConfigurationItemModel>>(configurationItems);
            RunTask(configServer => _configServerRepository.InitConfigurationItemsAsync(configServer.Address, token, addModels), model.TargetConfigServerNames);
        }

        public async Task CopyNamespace(CopyNamespaceModel model, string token)
        {
            if (!_configServers.ContainsKey(model.SourceConfigServerName)) throw new MateralConfigCenterException($"服务{model.SourceConfigServerName}不存在");
            var filterModel = new QueryConfigurationItemFilterModel
            {
                NamespaceID = model.NamespaceID
            };
            List<ConfigurationItemListDTO> configurationItems = await _configServerRepository.GetConfigurationItemAsync(filterModel, _configServers[model.SourceConfigServerName].Address);
            var addModel = new InitConfigurationItemsByNamespaceModel
            {
                ConfigurationItems = _mapper.Map<List<AddConfigurationItemModel>>(configurationItems),
                NamespaceID = model.NamespaceID
            };
            RunTask(configServer => _configServerRepository.InitConfigurationItemsByNamespaceAsync(configServer.Address, token, addModel), model.TargetConfigServerNames);
        }
        #region 私有方法
        /// <summary>
        /// 健康检查定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HealthTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            healthTimer.Stop();
            RunTask(HealthAsync);
            healthTimer.Start();
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="configServer"></param>
        private async Task HealthAsync(ConfigServerModel configServer)
        {
            try
            {
                await _configServerRepository.HealthAsync(configServer.Address);
            }
            catch (Exception)
            {
                _configServers.Remove(configServer.Name, out ConfigServerModel client);
                var ex = new MateralConfigCenterException($"健康检查失败,配置服务{client.Name}({client.Address})已注销,当前配置服务数量{_configServers.Count}");
                ConsoleHelper.ServerWriteError(ex);
            }
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="action"></param>
        /// <param name="names"></param>
        private void RunTask(Func<ConfigServerModel, Task> action, params string[] names)
        {
            var tasks = new List<Task>();
            foreach ((string name, ConfigServerModel value) in _configServers)
            {
                if (names.Length == 0 || names.Contains(name))
                {
                    tasks.Add(Task.Run(async () => await action(value)));
                }
            }
            Task.WaitAll(tasks.ToArray());
        }
        #endregion
    }
}
