using Materal.ConfigCenter.ConfigServer.Domain.Repositories;
using Materal.ConfigCenter.ConfigServer.Services;
using System;
using System.Threading.Tasks;
using System.Timers;
using Materal.ConfigCenter.ConfigServer.Common;

namespace Materal.ConfigCenter.ConfigServer.ServiceImpl
{
    public class ConfigServerServiceImpl : IConfigServerService
    {
        private readonly Timer healthTimer = new Timer(10000);
        private readonly Timer registerTimer = new Timer(5000);
        private readonly IConfigServerRepository _configServerRepository;
        public ConfigServerServiceImpl(IConfigServerRepository configServerRepository)
        {
            _configServerRepository = configServerRepository;
            healthTimer.Elapsed += HealthTimer_Elapsed;
            registerTimer.Elapsed += RegisterTimer_Elapsed;
        }

        public void Register()
        {
            if (healthTimer.Enabled)
            {
                healthTimer.Stop();
            }
            registerTimer.Start();
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
            Task.Run(async () =>
            {
                try
                {
                    await _configServerRepository.HealthAsync();
                    if (!healthTimer.Enabled)
                    {
                        healthTimer.Start();
                    }
                }
                catch (Exception ex)
                {
                    ex = new MateralConfigCenterException("Protal健康检查失败", ex);
                    ConsoleHelper.ServerWriteError(ex);
                    Register();
                }
            });
        }
        /// <summary>
        /// 注册定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            registerTimer.Stop();
            Task.Run(async () =>
            {
                try
                {
                    ConsoleHelper.ServerWriteLine($"开始向Protal注册服务{ApplicationConfig.ProtalServerConfig.Name}");
                    await _configServerRepository.RegisterAsync();
                    ConsoleHelper.ServerWriteLine("注册成功");
                    if (!healthTimer.Enabled)
                    {
                        healthTimer.Start();
                    }
                }
                catch (Exception ex)
                {
                    ex = new MateralConfigCenterException("注册失败", ex);
                    ConsoleHelper.ServerWriteError(ex);
                    Register();
                }
            });
        }
        #endregion
    }
}
