using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Repositories
{
    public interface IWorkEventRepository : IOscillatorRepository<WorkEvent>
    {
        /// <summary>
        /// 获得所有任务事件值
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<string[]> GetAllWorkEventValuesAsync(Guid scheduleID);
        /// <summary>
        /// 获得默认任务事件值
        /// </summary>
        /// <returns></returns>
        public string[] GetDefaultWorkEventValues();
        /// <summary>
        /// 获得任务事件值
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<string[]> GetWorkEventValuesAsync(Guid scheduleID);
    }
}
