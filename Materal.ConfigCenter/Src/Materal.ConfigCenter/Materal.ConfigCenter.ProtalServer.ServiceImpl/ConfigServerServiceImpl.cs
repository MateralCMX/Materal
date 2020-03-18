using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.ServiceImpl.Models;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl
{
    public class ConfigServerServiceImpl : IConfigServerService
    {
        private readonly Dictionary<string, ConfigServerModel> _configServers = new Dictionary<string, ConfigServerModel>();
        private readonly Timer healthTimer = new Timer(10000);
        private readonly IConfigServerRepository _configServerRepository;
        public ConfigServerServiceImpl(IConfigServerRepository configServerRepository)
        {
            _configServerRepository = configServerRepository;
            healthTimer.Elapsed += HealthTimer_Elapsed;
            healthTimer.Start();
        }
        public void RegisterNewClient(RegisterConfigServerModel model)
        {
            if(_configServers.ContainsKey(model.Name)) throw new MateralConfigCenterException("该配置服务已注册");
            var client = model.CopyProperties<ConfigServerModel>();
            _configServers.Add(client.Name, client);
            ConsoleHelper.ServerWriteLine($"配置服务{client.Name}({client.Address})注册成功,当前配置服务数量{_configServers.Count}");
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="name"></param>
        private async Task HealthAsync(string name)
        {
            try
            {
                if(!_configServers.ContainsKey(name)) throw new MateralConfigCenterException($"{name}配置服务不存在");
                ConfigServerModel configServer = _configServers[name];
                await _configServerRepository.HealthAsync(configServer.Address);
            }
            catch (Exception ex)
            {
                var exception = new MateralConfigCenterException("发生错误", ex);
                if (_configServers.ContainsKey(name))
                {
                    _configServers.Remove(name, out ConfigServerModel client);
                    exception = new MateralConfigCenterException($"发生错误,配置服务{client.Name}({client.Address})已注销", ex);
                }
                ConsoleHelper.ServerWriteError(exception);
            }
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
            try
            {
                Task.WaitAll(_configServers.Keys.Select(HealthAsync).ToArray());
            }
            finally
            {
                healthTimer.Start();
            }
        }
        #endregion
    }
}
