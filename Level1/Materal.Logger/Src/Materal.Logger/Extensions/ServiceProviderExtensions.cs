using Materal.Logger.LoggerLogs;

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
            LoggerHost.LoggerLog = serviceProvider.GetService<ILoggerLog>() ?? new ConsoleLoggerLog(serviceProvider);
            AppDomain.CurrentDomain.ProcessExit += (_, _) => LoggerHost.ShutdownAsync().Wait();
            await LoggerHost.StartAsync(serviceProvider);
            return serviceProvider;
        }
    }
}
