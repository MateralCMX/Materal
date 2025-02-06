using Materal.Oscillator.Abstractions.PlanTriggers;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 计划触发器数据扩展
    /// </summary>
    public static class PlanTriggerDataExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        /// <summary>
        /// 获取计划触发器
        /// </summary>
        /// <param name="planTriggerData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IPlanTrigger?> GetPlanTriggerAsync(this IPlanTriggerData planTriggerData, IServiceProvider serviceProvider)
        {
            await _semaphore.WaitAsync();
            try
            {
                Type dataType = planTriggerData.GetType();
                if (IPlanTriggerData.MapperTable[dataType] is Type planTriggerType)
                {
                    if (serviceProvider.GetService(planTriggerType) is not IPlanTrigger planTrigger) return null;
                    await planTrigger.SetDataAsync(planTriggerData);
                    return planTrigger;
                }
                else
                {
                    IEnumerable<IPlanTrigger> planTriggers = serviceProvider.GetServices<IPlanTrigger>();
                    IPlanTrigger? planTrigger = planTriggers.FirstOrDefault(m => m.DataType == dataType);
                    if (planTrigger is null) return null;
                    await planTrigger.SetDataAsync(planTriggerData);
                    IPlanTriggerData.MapperTable[dataType] = planTrigger.GetType();
                    return planTrigger;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
