using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务
    /// </summary>
    public abstract class WorkBase : IWork
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="workData"></param>
        /// <returns></returns>
        public virtual IWork Deserialization(string workData) => (IWork)workData.JsonToObject(GetType());
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public virtual IWork Deserialization(Work work) => Deserialization(work.WorkData);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public abstract Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public virtual Task InitAsync() => Task.CompletedTask;
    }
}
