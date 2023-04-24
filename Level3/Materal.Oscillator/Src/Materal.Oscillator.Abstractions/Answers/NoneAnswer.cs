using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Answers
{
    public class NoneAnswer : AnswerBase, IAnswer
    {
        public override Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job) => Task.FromResult(false);
    }
}
