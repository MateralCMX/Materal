using Materal.Abstractions;
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
        private OscillatorService? _oscillatorService;
        public override async Task InitAsync()
        {
            _oscillatorService = MateralServices.GetService<OscillatorService>();
            await base.InitAsync();
        }
        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job)
        {
            if (_oscillatorService == null) return true;
            await _oscillatorService.RunNowAsync(null, ScheduleID);
            return true;
        }
    }
}
