using NLog;
using NLog.Config;

namespace Materal.APP.Core
{
    public static class NLogManage
    {
        /// <summary>
        /// 注册
        /// </summary>
        public static void Register()
        {
            InstallationContext nLogInstallationContext = new InstallationContext();
            LogManager.Configuration.Install(nLogInstallationContext);
            LogManager.Configuration.Variables["MaxLogFileSaveDays"] = ApplicationConfig.NLogConfig.MaxLogFileSaveDays;
        }
    }
}
