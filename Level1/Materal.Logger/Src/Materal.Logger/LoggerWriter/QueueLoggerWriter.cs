using System.Threading.Tasks.Dataflow;

namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 队列日志写入器
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public abstract class QueueLoggerWriter<TModel, TTarget> : BaseLoggerWriter<TModel, TTarget>, ILoggerWriter<TModel>, ILoggerWriter
        where TModel : QueueLoggerWriterModel
        where TTarget : QueueTargetConfig
    {
        private readonly ActionBlock<TModel> _writeLoggerBlock;
        /// <summary>
        /// 构造方法
        /// </summary>
        public QueueLoggerWriter(TTarget targetConfig) : base(targetConfig)
        {
            _writeLoggerBlock = new(WriteQueueLoggerAsync);
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
        /// 写队列日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public abstract Task WriteQueueLoggerAsync(TModel model);
    }
}
