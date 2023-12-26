﻿using Materal.Logger.LoggerLogs;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger
{
    /// <summary>
    /// 日志主机
    /// </summary>
    public static class LoggerHost
    {
        private static readonly ActionBlock<LoggerWriterModel> _writeLoggerBlock;
        private static readonly List<ILoggerWriter> _loggerWriters = [];
        /// <summary>
        /// 日志自身日志
        /// </summary>
        public static ILoggerLog? LoggerLog { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public static bool IsClose { get; private set; }
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static LoggerHost()
        {
            IsClose = false;
            _writeLoggerBlock = new(AsyncWriteLogger);
        }
        /// <summary>
        ///  写入日志
        /// </summary>
        /// <param name="model"></param>
        public static void WriteLogger(LoggerWriterModel model)
        {
            if (IsClose) return;
            _writeLoggerBlock.Post(model);
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
        /// 关闭
        /// </summary>
        public static async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerLog?.LogDebug($"正在关闭[MateralLogger]");
            if (_loggerWriters is not null)
            {
                _writeLoggerBlock.Complete();
                await _writeLoggerBlock.Completion;
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
