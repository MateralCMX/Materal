using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务
    /// </summary>
    public interface IWork : IOscillatorOperationModel<IWork>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public Task InitAsync();
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workResult">任务结果集</param>
        /// <param name="nowIndex">当前任务步骤数</param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public Task<string?> ExcuteAsync(List<WorkResultModel> workResult, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work);
    }
}
