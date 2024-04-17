using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.BatchLogger
{
    /// <summary>
    /// 批量日志写入器
    /// </summary>
    public abstract class BatchLoggerWriter<TLoggerTargetOptions> : BaseLoggerWriter<TLoggerTargetOptions>
        where TLoggerTargetOptions : LoggerTargetOptions
    {
        private BatchBlock<BatchLog<TLoggerTargetOptions>> _logBlock;
        private readonly ActionBlock<BatchLog<TLoggerTargetOptions>[]> _logsBlock;
        private readonly SemaphoreSlim _clearTimerSemaphore = new(0, 1);
        /// <summary>
        /// 清空定时器
        /// </summary>
        protected readonly Timer ClearTimer;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BatchLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : base(options, loggerInfo)
        {
            _logsBlock = new(LogAsync);
            _logBlock = GetNewBatchBlock();
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromSeconds(options.CurrentValue.BatchPushInterval), Timeout.InfiniteTimeSpan);
            _clearTimerSemaphore.Release();
        }
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        public override async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, TLoggerTargetOptions targetOptions)
        {
            BatchLog<TLoggerTargetOptions> batchLog = new(log, ruleOptions, targetOptions);
            _logBlock.Post(batchLog);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写批量日志
        /// </summary>
        /// <param name="batchLogs"></param>
        /// <returns></returns>
        public abstract Task LogAsync(BatchLog<TLoggerTargetOptions>[] batchLogs);
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <returns></returns>
        protected virtual BatchBlock<BatchLog<TLoggerTargetOptions>> GetNewBatchBlock()
        {
            BatchBlock<BatchLog<TLoggerTargetOptions>> result = new(Options.CurrentValue.BatchSize);
            result.LinkTo(_logsBlock);
            return result;
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ClearTimerElapsed(object? stateInfo)
        {
            _clearTimerSemaphore.Wait();
            if (IsClose) return;
            try
            {
                BatchBlock<BatchLog<TLoggerTargetOptions>> oldBlock = _logBlock;
                _logBlock = GetNewBatchBlock();
                oldBlock.Complete();
                oldBlock.Completion.Wait();
            }
            finally
            {
                _clearTimerSemaphore.Release();
                if (!IsClose)
                {
                    ClearTimer.Change(TimeSpan.FromSeconds(Options.CurrentValue.BatchPushInterval), Timeout.InfiniteTimeSpan);
                }
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public override async Task StopAsync()
        {
            await _clearTimerSemaphore.WaitAsync();
            try
            {
                string name = GetType().Name;
                LoggerInfo.LogDebug($"正在关闭{name}");
                IsClose = true;
                _logBlock.Complete();
                await _logBlock.Completion;
                _logsBlock.Complete();
                await _logsBlock.Completion;
                LoggerInfo.LogDebug($"{name}关闭成功");
            }
            finally
            {
                _clearTimerSemaphore.Release();
            }
        }
    }
}
