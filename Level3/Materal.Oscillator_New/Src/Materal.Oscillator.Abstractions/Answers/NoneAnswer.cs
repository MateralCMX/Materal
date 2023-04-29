using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Abstractions.Answers
{
    /// <summary>
    /// 无响应
    /// </summary>
    public class NoneAnswer : AnswerBase, IAnswer
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="answer"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public override Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Answer answer, IOscillatorJob job) => Task.FromResult(false);
    }
}
