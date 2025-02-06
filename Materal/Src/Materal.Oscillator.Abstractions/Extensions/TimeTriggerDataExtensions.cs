using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 时间触发器数据扩展
    /// </summary>
    public static class TimeTriggerDataExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        /// <summary>
        /// 获取时间触发器
        /// </summary>
        /// <param name="timeTriggerData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<ITimeTrigger?> GetTimeTriggerAsync(this ITimeTriggerData timeTriggerData, IServiceProvider serviceProvider)
        {
            await _semaphore.WaitAsync();
            try
            {
                Type dataType = timeTriggerData.GetType();
                if (ITimeTriggerData.MapperTable[dataType] is Type timeTriggerType)
                {
                    if (serviceProvider.GetService(timeTriggerType) is not ITimeTrigger timeTrigger) return null;
                    await timeTrigger.SetDataAsync(timeTriggerData);
                    return timeTrigger;
                }
                else
                {
                    IEnumerable<ITimeTrigger> timeTriggers = serviceProvider.GetServices<ITimeTrigger>();
                    ITimeTrigger? timeTrigger = timeTriggers.FirstOrDefault(m => m.DataType == dataType);
                    if (timeTrigger is null) return null;
                    await timeTrigger.SetDataAsync(timeTriggerData);
                    ITimeTriggerData.MapperTable[dataType] = timeTrigger.GetType();
                    return timeTrigger;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
