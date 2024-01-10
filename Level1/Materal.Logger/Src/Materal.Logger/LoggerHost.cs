using Materal.Logger.LoggerLogs;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public static class LoggerHost
    {
        private static ActionBlock<LoggerWriterModel>? _writeLoggerBlock;
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
            if (!model.Config.Enable) return;
            foreach (TargetConfig targetConfig in LoggerConfig.Targets)
            {
                if (!targetConfig.Enable) continue;
                ILoggerWriter loggerWriter = targetConfig.GetLoggerWriter();
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
        public static async Task StartAsync()
        {
            if (LoggerLog is not null)
            {
                await LoggerLog.StartAsync();
            }
            LoggerLog?.LogDebug($"正在启动[MateralLogger]");
            IsClose = false;
            _writeLoggerBlock = new(AsyncWriteLogger);
            LoggerLog?.LogDebug($"[MateralLogger]启动成功");
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerLog?.LogDebug($"正在关闭[MateralLogger]");
            if(_writeLoggerBlock is not null)
            {
                _writeLoggerBlock.Complete();
                await _writeLoggerBlock.Completion;
            }
            foreach (TargetConfig targetConfig in LoggerConfig.Targets)
            {
                if (!targetConfig.Enable) continue;
                ILoggerWriter loggerWriter = targetConfig.GetLoggerWriter();
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
