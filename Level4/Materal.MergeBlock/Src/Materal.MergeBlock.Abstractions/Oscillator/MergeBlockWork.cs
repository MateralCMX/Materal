using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.MergeBlock.Abstractions.Oscillator
{
    /// <summary>
    /// MergeBlock任务
    /// </summary>
    /// <typeparam name="TWorkData"></typeparam>
    public abstract class MergeBlockWork<TWorkData> : WorkBase<TWorkData>
        where TWorkData : IWorkData, new()
    {
        /// <inheritdoc/>
        protected override async Task ExcuteWorkAsync(IOscillatorContext oscillatorContext)
        {
            try
            {
                if (OscillatorInitManager.IsInit(oscillatorContext.WorkData))
                {
                    await InitAsync(oscillatorContext);
                }
                else
                {
                    await WorkExecuteAsync(oscillatorContext);
                }
            }
            finally
            {
                OscillatorInitManager.RemoveInitKey(oscillatorContext.WorkData);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected virtual async Task InitAsync(IOscillatorContext workContext) => await Task.CompletedTask;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected abstract Task WorkExecuteAsync(IOscillatorContext workContext);
    }
}
