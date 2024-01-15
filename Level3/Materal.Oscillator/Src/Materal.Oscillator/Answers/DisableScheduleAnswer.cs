using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Answers
{
    /// <summary>
    /// 禁止调度器
    /// </summary>
    public class DisableScheduleAnswer : AnswerBase, IAnswer
    {
        private readonly IOscillatorHost _host;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DisableScheduleAnswer() : base()
        {
            _host = ServiceProvider.GetRequiredService<IOscillatorHost>();
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, IOscillatorJob job)
        {
            if (_host == null) return true;
            await _host.DisableScheduleAsync(schedule.ID);
            return false;
        }
    }
}
