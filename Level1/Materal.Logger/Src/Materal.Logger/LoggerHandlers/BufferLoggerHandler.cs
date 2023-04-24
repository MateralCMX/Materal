using Materal.Logger.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 流日志处理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BufferLoggerHandler<T> : LoggerHandler
        where T : class, new()
    {
        private BatchBlock<T> messageBuffer;
        private readonly ActionBlock<T[]> saveBuffer;
        /// <summary>
        /// 清空定时器
        /// </summary>
        protected readonly Timer ClearTimer;
        private readonly int _clearTimerInterval;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="clearTimerInterval"></param>
        protected BufferLoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, int clearTimerInterval = 2000) : base(rule, target)
        {
            _clearTimerInterval = clearTimerInterval;
            saveBuffer = new(SaveData);
            messageBuffer = new(target.BufferCount);
            messageBuffer.LinkTo(saveBuffer);
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        protected virtual void PushData(T data)
        {
            //clearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
            messageBuffer.Post(data);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datas"></param>
        protected abstract void SaveData(T[] datas);
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            ClearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            messageBuffer.Complete();
            messageBuffer.Completion.Wait();
            saveBuffer.Complete();
            saveBuffer.Completion.Wait();
            base.Close();
        }
        /// <summary>
        /// 清空定时器执行
        /// </summary>
        /// <param name="stateInfo"></param>
        public void ClearTimerElapsed(object? stateInfo)
        {
            ClearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            BatchBlock<T> oldBlock = messageBuffer;
            messageBuffer = GetNewBatchBlock();
            oldBlock.Complete();
            oldBlock.Completion.Wait();
            ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
        }
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <returns></returns>
        private BatchBlock<T> GetNewBatchBlock()
        {
            BatchBlock<T> result = new(Target.BufferCount);
            result.LinkTo(saveBuffer);
            return result;
        }
    }
}
