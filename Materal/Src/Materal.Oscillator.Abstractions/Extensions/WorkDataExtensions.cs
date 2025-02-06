using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 任务数据扩展
    /// </summary>
    public static class WorkDataExtensions
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);
        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task<IWork?> GetWorkAsync(this IWorkData workData, IServiceProvider serviceProvider)
        {
            await _semaphore.WaitAsync();
            try
            {
                Type dataType = workData.GetType();
                if (IWorkData.MapperTable[dataType] is Type workType)
                {
                    if (serviceProvider.GetService(workType) is not IWork work) return null;
                    await work.SetDataAsync(workData);
                    return work;
                }
                else
                {
                    IEnumerable<IWork> works = serviceProvider.GetServices<IWork>();
                    IWork? work = works.FirstOrDefault(m => m.DataType == dataType);
                    if (work is null) return null;
                    await work.SetDataAsync(workData);
                    IWorkData.MapperTable[dataType] = work.GetType();
                    return work;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
