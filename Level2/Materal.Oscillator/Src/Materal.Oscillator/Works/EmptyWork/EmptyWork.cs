using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works.EmptyWork
{
    /// <summary>
    /// 空白任务
    /// </summary>
    public class EmptyWork() : WorkBase<EmptyWorkData>("空白任务")
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        public override async Task ExecuteAsync(IWorkContext workContext) => await Task.CompletedTask;
    }
}
