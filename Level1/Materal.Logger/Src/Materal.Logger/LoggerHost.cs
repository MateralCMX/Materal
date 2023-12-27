using Materal.Logger.LoggerLogs;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public static class LoggerHost
    {
        private static ActionBlock<LoggerWriterModel>? _writeLoggerBlock;
        private static readonly List<ILoggerWriter> _loggerWriters = [];
        private static readonly List<TraceListener> _traceListeners = [];
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
            List<ILoggerWriter> loggerHandlers = [];
            foreach (TargetConfig targetConfig in model.Config.Targets)
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
                if(_loggerWriters.Contains(loggerWriter)) continue;
                _loggerWriters.Add(loggerWriter);
            }
        }
        /// <summary>
        /// 添加TraceListener
        /// </summary>
        /// <param name="traceListener"></param>
        public static void AddTraceListener(TraceListener traceListener)
        {
            _traceListeners.Add(traceListener);
            Trace.Listeners.Add(traceListener);
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
            foreach (TraceListener traceListener in _traceListeners)
            {
                Trace.Listeners.Remove(traceListener);
            }
            _traceListeners.Clear();
            if(_writeLoggerBlock is not null)
            {
                _writeLoggerBlock.Complete();
                await _writeLoggerBlock.Completion;
            }
            if (_loggerWriters is not null)
            {
                foreach (ILoggerWriter writer in _loggerWriters)
                {
                    await writer.ShutdownAsync();
                }
            }
            LoggerLog?.LogDebug($"[MateralLogger]关闭成功");
            if(LoggerLog is not null)
            {
                await LoggerLog.ShutdownAsync();
            }
        }
    }
}
