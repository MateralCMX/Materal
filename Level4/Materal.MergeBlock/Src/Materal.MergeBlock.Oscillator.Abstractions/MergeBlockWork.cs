namespace Materal.MergeBlock.Oscillator.Abstractions
{
    /// <summary>
    /// MergeBlock任务
    /// </summary>
    /// <typeparam name="TWorkData"></typeparam>
    public abstract class MergeBlockWork<TWorkData> : BaseWork<TWorkData>, IWork<TWorkData>
        where TWorkData : class, IWorkData
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public override async Task<string?> ExcuteAsync(TWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            try
            {
                if (OscillatorDataHelper.IsInit(work.Name)) return await InitAsync(workData, jobResults, nowIndex, schedule, scheduleWork, work);
                return await WorkExcuteAsync(workData, jobResults, nowIndex, schedule, scheduleWork, work);
            }
            finally
            {
                OscillatorDataHelper.RemoveInitingKey(work.Name);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        protected virtual Task<string?> InitAsync(TWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work) => Task.FromResult((string?)null);
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        protected virtual Task<string?> WorkExcuteAsync(TWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work) => Task.FromResult((string?)null);
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="workName"></param>
        /// <param name="autoRemove"></param>
        /// <returns></returns>
        protected TData? GetData<TData>(string workName, bool autoRemove = true) => OscillatorDataHelper.GetData<TData>(workName, autoRemove);

    }
}
