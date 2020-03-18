using Materal.ConfigCenter.ProtalServer.Common;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;
using Materal.ConfigCenter.ProtalServer.ServiceImpl.Models;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

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
            if (_configServers.ContainsKey(model.Name)) throw new MateralConfigCenterException("该配置服务已注册");
            var client = model.CopyProperties<ConfigServerModel>();
            _configServers.Add(client.Name, client);
            ConsoleHelper.ServerWriteLine($"配置服务{client.Name}({client.Address})注册成功,当前配置服务数量{_configServers.Count}");
        }
        public void DeleteProject(Guid id)
        {
            RunTask(configServer => _configServerRepository.DeleteProjectAsync(configServer.Address, id));
        }
        public void DeleteNamespace(Guid id)
        {
            RunTask(configServer => _configServerRepository.DeleteNamespaceAsync(configServer.Address, id));
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
                RunTask(configServer => HealthAsync(configServer.Address));
            }
            finally
            {
                healthTimer.Start();
            }
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="address"></param>
        private async Task HealthAsync(string address)
        {
            try
            {
                await _configServerRepository.HealthAsync(address);
            }
            catch (Exception ex)
            {
                _configServers.Remove(address, out ConfigServerModel client);
                ex = new MateralConfigCenterException($"发生错误,配置服务{client.Name}({client.Address})已注销", ex);
                ConsoleHelper.ServerWriteError(ex);
            }
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="action"></param>
        private void RunTask(Func<ConfigServerModel, Task> action)
        {
            var tasks = new List<Task>();
            foreach ((string _, ConfigServerModel value) in _configServers)
            {
                tasks.Add(action(value));
            }
            Task.WaitAll(tasks.ToArray());
        }
        #endregion
    }
}
