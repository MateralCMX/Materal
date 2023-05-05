using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Abstractions.Answers
{
    /// <summary>
    /// 响应
    /// </summary>
    public interface IAnswer : IOscillatorOperationModel<IAnswer>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public Task InitAsync();
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <param name="job"></param>
        /// <returns>true->继续执行后续响应,false->停止执行后续响应</returns>
        public Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, IOscillatorJob job);
    }
}
