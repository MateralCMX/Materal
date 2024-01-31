using Materal.Logger.LoggerLogs;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public static class LoggerHost
    {
        private static ActionBlock<LoggerWriterModel>? _writeLoggerBlock;
        private static IServiceProvider? _serviceProvider;
        /// <summary>
        /// 日志自身日志
        /// </summary>
        public static ILoggerLog? LoggerLog { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static bool IsClose { get; private set; } = true;
        /// <summary>
        ///  写入日志
        /// </summary>
        /// <param name="model"></param>
        public static void WriteLogger(LoggerWriterModel model)
        {
            if (IsClose) return;
            _writeLoggerBlock?.Post(model);
        }
        /// <summary>
        ///  写入日志
        /// </summary>
        /// <param name="model"></param>
        private static async Task AsyncWriteLogger(LoggerWriterModel model)
        {
            if (!model.Config.Enable || _serviceProvider is null) return;
            foreach (TargetConfig targetConfig in LoggerConfig.Targets)
            {
                if (!targetConfig.Enable) continue;
                ILoggerWriter loggerWriter = targetConfig.GetLoggerWriter(_serviceProvider);
                try
                {
                    await loggerWriter.WriteLoggerAsync(model);
                }
                catch (Exception ex)
                {
                    LoggerLog?.LogError($"写入日志失败", ex);
                }
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        public static async Task StartAsync(IServiceProvider serviceProvider)
        {
            if (!IsClose) return;
            try
            {
                IsClose = false;
                _serviceProvider = serviceProvider;
                if (LoggerLog is not null)
                {
                    await LoggerLog.StartAsync();
                }
                LoggerLog?.LogDebug($"正在启动[MateralLogger]");
                _writeLoggerBlock = new(AsyncWriteLogger);
                LoggerLog?.LogDebug($"[MateralLogger]启动成功");
            }
            catch (Exception ex)
            {
                IsClose = true;
                LoggerLog?.LogError($"[MateralLogger]启动失败", ex);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static async Task ShutdownAsync()
        {
            if(IsClose) return;
            IsClose = true;
            LoggerLog?.LogDebug($"正在关闭[MateralLogger]");
            if(_writeLoggerBlock is not null)
            {
                _writeLoggerBlock.Complete();
                await _writeLoggerBlock.Completion;
            }
            foreach (TargetConfig targetConfig in LoggerConfig.Targets)
            {
                if (!targetConfig.Enable || _serviceProvider is null) continue;
                ILoggerWriter loggerWriter = targetConfig.GetLoggerWriter(_serviceProvider);
                await loggerWriter.ShutdownAsync();
            }
            LoggerLog?.LogDebug($"[MateralLogger]关闭成功");
            if(LoggerLog is not null)
            {
                await LoggerLog.ShutdownAsync();
            }
        }
    }
}
