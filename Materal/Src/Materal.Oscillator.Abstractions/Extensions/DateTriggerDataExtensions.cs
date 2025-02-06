using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 日期触发器数据扩展
    /// </summary>
    public static class DateTriggerDataExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        /// <summary>
        /// 获取日期触发器
        /// </summary>
        /// <param name="dateTriggerData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IDateTrigger?> GetDateTriggerAsync(this IDateTriggerData dateTriggerData, IServiceProvider serviceProvider)
        {
            await _semaphore.WaitAsync();
            try
            {
                Type dataType = dateTriggerData.GetType();
                if (IDateTriggerData.MapperTable[dataType] is Type dateTriggerType)
                {
                    if (serviceProvider.GetService(dateTriggerType) is not IDateTrigger dateTrigger) return null;
                    await dateTrigger.SetDataAsync(dateTriggerData);
                    return dateTrigger;
                }
                else
                {
                    IEnumerable<IDateTrigger> dateTriggers = serviceProvider.GetServices<IDateTrigger>();
                    IDateTrigger? dateTrigger = dateTriggers.FirstOrDefault(m => m.DataType == dataType);
                    if (dateTrigger is null) return null;
                    await dateTrigger.SetDataAsync(dateTriggerData);
                    IDateTriggerData.MapperTable[dataType] = dateTrigger.GetType();
                    return dateTrigger;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
