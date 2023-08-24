using Materal.Logger.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 流日志处理器
    /// </summary>
    public abstract class BufferLoggerHandler<T> : LoggerHandler
        where T : BufferLogerHandlerModel
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
        /// 清空定时器间隔
        /// </summary>
        private readonly int _clearTimerInterval;
        private int _maxIntervalDataCount;
        /// <summary>
        /// 上次间隔数据量
        /// </summary>
        private int _upIntervalDataCount = 0;
        /// <summary>
        /// 间隔数据量
        /// </summary>
        private int _intervalDataCount = 0;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="clearTimerInterval"></param>
        /// <param name="defaultDataCount"></param>
        /// <param name="maxIntervalDataCount"></param>
        protected BufferLoggerHandler(int clearTimerInterval = 1000, int defaultDataCount = 10, int maxIntervalDataCount = 1000)
        {
            _clearTimerInterval = clearTimerInterval;
            _handlerDataBuffer = new(HandlerData);
            _messageBuffer = GetNewBatchBlock(defaultDataCount);
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
            _maxIntervalDataCount = maxIntervalDataCount <= 2 ? 1000 : maxIntervalDataCount;
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        protected override void Handler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            object? dataObj = typeof(T).Instantiation(rule, target, model);
            if (dataObj is null || dataObj is not T data) return;
            PushData(data);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        protected virtual void PushData(T data)
        {
            _messageBuffer.Post(data);
            _intervalDataCount++;
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ClearTimerElapsed(object? stateInfo)
        {
            ClearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            BatchBlock<T> oldBlock = _messageBuffer;
            _upIntervalDataCount = _intervalDataCount <= 0 ? 0 : _intervalDataCount;
            _upIntervalDataCount = _upIntervalDataCount > _maxIntervalDataCount ? _maxIntervalDataCount : _upIntervalDataCount;
            _intervalDataCount = 0;
            _messageBuffer = GetNewBatchBlock(_upIntervalDataCount);
            oldBlock.Complete();
            oldBlock.Completion.Wait();
            ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <param name="blockCount"></param>
        /// <returns></returns>
        private BatchBlock<T> GetNewBatchBlock(int blockCount)
        {
            blockCount = blockCount <= 1 ? 2 : blockCount;
            BatchBlock<T> result = new(blockCount);
            result.LinkTo(_handlerDataBuffer);
            return result;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datas"></param>
        protected abstract void HandlerData(T[] datas);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            ClearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            _messageBuffer.Complete();
            await _messageBuffer.Completion;
            _handlerDataBuffer.Complete();
            await _handlerDataBuffer.Completion;
        }
    }
    /// <summary>
    /// 流日志处理器模型
    /// </summary>
    public abstract class BufferLogerHandlerModel : LoggerHandlerModel
    {
        /// <summary>
        /// 规则
        /// </summary>
        public LoggerRuleConfigModel Rule { get; }
        /// <summary>
        /// 目标
        /// </summary>
        public LoggerTargetConfigModel Target { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public BufferLogerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model)
        {
            model.CopyProperties(this);
            Rule = rule;
            Target = target;
        }
    }
}
