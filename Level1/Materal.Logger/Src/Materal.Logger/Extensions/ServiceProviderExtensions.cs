namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 服务提供器扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 使用Dy日志
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IServiceProvider> UseMateralLoggerAsync(this IServiceProvider serviceProvider)
        {
            if (!LoggerHost.IsClose) return serviceProvider;
            IOptionsMonitor<LoggerConfig> loggerConfigMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<LoggerConfig>>();
            LoggerConfig loggerConfig = loggerConfigMonitor.CurrentValue;
            loggerConfig.UpdateConfig(serviceProvider);
            loggerConfigMonitor.OnChange(m =>
            {
                loggerConfig.UpdateConfig(serviceProvider);
                if (LoggerHost.LoggerLog is null) return;
                LoggerHost.LoggerLog.MinLevel = m.MinLoggerLogLevel;
                LoggerHost.LoggerLog.MaxLevel = m.MaxLoggerLogLevel;
            });
            AppDomain.CurrentDomain.ProcessExit += (_, _) => LoggerHost.ShutdownAsync().Wait();
            await LoggerHost.StartAsync();
            return serviceProvider;
        }
    }
}
