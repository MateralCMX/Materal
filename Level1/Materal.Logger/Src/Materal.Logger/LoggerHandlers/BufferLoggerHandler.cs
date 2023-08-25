﻿using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 流日志处理器
    /// </summary>
    public abstract class BufferLoggerHandler<T> : LoggerHandler
        where T : BufferLoggerHandlerModel
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
        /// <summary>
        /// 最大间隔数据数量
        /// </summary>
        private readonly int _blockCount;
        /// <summary>
        /// 清空定时器执行中
        /// </summary>
        private bool _isClearTimerExecution = false;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="clearTimerInterval"></param>
        /// <param name="maxIntervalDataCount"></param>
        protected BufferLoggerHandler(int clearTimerInterval = 2000, int maxIntervalDataCount = 2000)
        {
            _clearTimerInterval = clearTimerInterval;
            _handlerDataBuffer = new(HandlerData);
            _blockCount = maxIntervalDataCount < 2 ? 2000 : maxIntervalDataCount;
            _messageBuffer = GetNewBatchBlock();
            ClearTimer = new(ClearTimerElapsed);
            ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
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
            if (!Logger.IsClose)
            {
                ClearTimer.Change(TimeSpan.FromMilliseconds(_clearTimerInterval), Timeout.InfiniteTimeSpan);
            }
        }
        /// <summary>
        /// 获得一个新的数据块
        /// </summary>
        /// <returns></returns>
        private BatchBlock<T> GetNewBatchBlock()
        {
            BatchBlock<T> result = new(_blockCount);
            result.LinkTo(_handlerDataBuffer);
            return result;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datas"></param>
        protected void HandlerData(T[] datas) => HandlerOKData(datas.Where(m => m.IsOK).ToArray());
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected abstract void HandlerOKData(T[] datas);
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            while (_isClearTimerExecution) { }
            _messageBuffer.Complete();
            await _messageBuffer.Completion;
            _handlerDataBuffer.Complete();
            await _handlerDataBuffer.Completion;
        }
    }
}
