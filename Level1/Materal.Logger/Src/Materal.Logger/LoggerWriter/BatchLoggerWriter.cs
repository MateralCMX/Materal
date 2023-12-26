using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 批量日志写入器
    /// </summary>
    public abstract class BatchLoggerWriter<TModel, TTarget> : BaseLoggerWriter<TModel, TTarget>, ILoggerWriter<TModel>, ILoggerWriter
        where TModel : BatchLoggerWriterModel
        where TTarget : BatchTargetConfig
    {
        private BatchBlock<TModel> _writeLoggerBlock;
        private readonly ActionBlock<TModel[]> _writeLoggersBlock;
        /// <summary>
        /// 清空定时器
        /// </summary>
        protected readonly Timer ClearTimer;
        /// <summary>
        /// 清空定时器执行中
        /// </summary>
        private bool _isClearTimerExecution = false;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BatchLoggerWriter(TTarget targetConfig) : base(targetConfig)
        {
            _writeLoggersBlock = new(WriteBatchLoggerAsync);
            _writeLoggerBlock = GetNewBatchBlock();
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromSeconds(targetConfig.Batch.PushInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteLoggerAsync(TModel model)
        {
            _writeLoggerBlock.Post(model);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写批量日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public abstract Task WriteBatchLoggerAsync(TModel[] models);
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <returns></returns>
        protected virtual BatchBlock<TModel> GetNewBatchBlock()
        {
            BatchBlock<TModel> result = new(TargetConfig.Batch.BatchSize);
            result.LinkTo(_writeLoggersBlock);
            return result;
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ClearTimerElapsed(object? stateInfo)
        {
            _isClearTimerExecution = true;
            BatchBlock<TModel> oldBlock = _writeLoggerBlock;
            _writeLoggerBlock = GetNewBatchBlock();
            oldBlock.Complete();
            oldBlock.Completion.Wait();
            if (!IsClose)
            {
                ClearTimer.Change(TimeSpan.FromSeconds(TargetConfig.Batch.PushInterval), Timeout.InfiniteTimeSpan);
            }
            _isClearTimerExecution = false;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            IsClose = true;
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{TargetConfig.Name}]");
            while (_isClearTimerExecution) { await Task.Delay(1000); }
            _writeLoggerBlock.Complete();
            await _writeLoggerBlock.Completion;
            _writeLoggersBlock.Complete();
            await _writeLoggersBlock.Completion;
            LoggerHost.LoggerLog?.LogDebug($"[{TargetConfig.Name}]关闭成功");
        }
    }
}
