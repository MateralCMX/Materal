using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 流日志处理器
    /// </summary>
    public abstract class BufferLoggerHandler<T, T2> : LoggerHandler<T2>
        where T : BufferLoggerHandlerModel
        where T2 : LoggerTargetConfigModel
    {
        /// <summary>
        /// 消息缓冲区
        /// </summary>
        private BatchBlock<T> _messageBuffer;
        /// <summary>
        /// 保存数据
        /// </summary>
        private readonly ActionBlock<T[]> _handlerDataBuffer;
        /// <summary>
        /// 清空定时器
        /// </summary>
        protected readonly Timer ClearTimer;
        /// <summary>
        /// 清空定时器执行中
        /// </summary>
        private bool _isClearTimerExecution = false;
        /// <summary>
        /// 流缓冲区数量
        /// </summary>
        private readonly int _bufferCount;
        /// <summary>
        /// 缓冲推入间隔(ms)
        /// </summary>
        private readonly int _bufferPushInterval;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected BufferLoggerHandler(LoggerRuntime loggerRuntime) : base(loggerRuntime)
        {
            _bufferPushInterval = Config.BufferPushInterval;
            _bufferCount = Config.BufferCount;
            _handlerDataBuffer = new(HandlerData);
            _messageBuffer = GetNewBatchBlock();
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromMilliseconds(_bufferPushInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, T2 target, LoggerHandlerModel model)
        {
            object? dataObj = typeof(T).Instantiation(rule, target, model, Config);
            if (dataObj is null || dataObj is not T data) return;
            PushData(data);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        protected virtual void PushData(T data) => _messageBuffer.Post(data);
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ClearTimerElapsed(object? stateInfo)
        {
            _isClearTimerExecution = true;
            BatchBlock<T> oldBlock = _messageBuffer;
            _messageBuffer = GetNewBatchBlock();
            oldBlock.Complete();
            oldBlock.Completion.Wait();
            _isClearTimerExecution = false;
            if (!IsClose)
            {
                ClearTimer.Change(TimeSpan.FromMilliseconds(_bufferPushInterval), Timeout.InfiniteTimeSpan);
            }
        }
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <returns></returns>
        private BatchBlock<T> GetNewBatchBlock()
        {
            BatchBlock<T> result = new(_bufferCount);
            result.LinkTo(_handlerDataBuffer);
            return result;
        }
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected abstract void HandlerData(T[] datas);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            string handlerName = GetType().Name;
            LoggerLog.LogDebug($"正在关闭{handlerName}");
            while (_isClearTimerExecution) { }
            _messageBuffer.Complete();
            await _messageBuffer.Completion;
            _handlerDataBuffer.Complete();
            await _handlerDataBuffer.Completion;
            LoggerLog.LogDebug($"已关闭{handlerName}");
        }
    }
}
