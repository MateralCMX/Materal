using Microsoft.Extensions.Hosting;

namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 主机扩展
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// 使用Materal日志
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost UseMateralLogger(this IHost host)
        {
            LoggerServices.Services = host.Services;
            host.Services.GetRequiredService<IOptionsMonitor<LoggerConfig>>()
                .OnChange(m=>
                {
                    if (LoggerHost.LoggerLog is null) return;
                    LoggerHost.LoggerLog.MinLevel = m.MinLoggerLogLevel;
                    LoggerHost.LoggerLog.MaxLevel = m.MaxLoggerLogLevel;
                });
            AppDomain.CurrentDomain.ProcessExit += (_, _) => LoggerHost.ShutdownAsync().Wait();
            LoggerHost.LoggerLog?.LogDebug($"MateralLogger已启动");
            return host;
        }
    }
}
