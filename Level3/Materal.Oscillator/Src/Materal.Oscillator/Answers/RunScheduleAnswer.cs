using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Answers
{
    /// <summary>
    /// 运行其他调度器响应
    /// </summary>
    public class RunScheduleAnswer : AnswerBase, IAnswer
    {
        /// <summary>
        /// 目标调度器唯一标识
        /// </summary>
        public Guid ScheduleID { get; set; }
        private readonly IOscillatorHost? _host;
        /// <summary>
        /// 构造方法
        /// </summary>
        public RunScheduleAnswer() : base()
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
            await _host.RunNowAsync(ScheduleID);
            return true;
        }
    }
}
