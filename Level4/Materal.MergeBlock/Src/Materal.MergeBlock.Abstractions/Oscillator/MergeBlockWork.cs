using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Works;

namespace Materal.MergeBlock.Abstractions.Oscillator
{
    /// <summary>
    /// MergeBlock任务
    /// </summary>
    /// <typeparam name="TWorkData"></typeparam>
    public abstract class MergeBlockWork<TWorkData> : WorkBase<TWorkData>
        where TWorkData : IWorkData, new()
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(IWorkContext workContext)
        {
            try
            {
                if (OscillatorInitManager.IsInit(workContext.Oscillator.WorkData))
                {
                    await InitAsync(workContext);
                }
                else
                {
                    await WorkExecuteAsync(workContext);
                }
            }
            finally
            {
                OscillatorInitManager.RemoveInitKey(workContext.Oscillator.WorkData);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected virtual Task InitAsync(IWorkContext workContext) => Task.CompletedTask;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected abstract Task WorkExecuteAsync(IWorkContext workContext);
    }
}
