using Materal.Logger.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    public abstract class BufferLoggerHandler<T> : LoggerHandler
        where T : class, new()
    {
        private BatchBlock<T> messageBuffer;
        private readonly ActionBlock<T[]> saveBuffer;
        protected readonly Timer clearTimer;
        private readonly int _clearTimerInterval;
        protected BufferLoggerHandler(MateralLoggerRuleConfigModel rule, MateralLoggerTargetConfigModel target, int clearTimerInterval = 2000) : base(rule, target)
        {
            _clearTimerInterval = clearTimerInterval;
            saveBuffer = new(SaveData);
            messageBuffer = new(target.BufferCount);
            messageBuffer.LinkTo(saveBuffer);
            clearTimer = new(ClearTimerElapsed);
            clearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
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
        public override void Close()
        {
            clearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            messageBuffer.Complete();
            messageBuffer.Completion.Wait();
            saveBuffer.Complete();
            saveBuffer.Completion.Wait();
            base.Close();
        }
        public void ClearTimerElapsed(object? stateInfo)
        {
            clearTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            BatchBlock<T> oldBlock = messageBuffer;
            messageBuffer = GetNewBatchBlock();
            oldBlock.Complete();
            oldBlock.Completion.Wait();
            clearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
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
