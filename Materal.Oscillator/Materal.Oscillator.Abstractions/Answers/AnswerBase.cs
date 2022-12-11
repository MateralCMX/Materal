using Materal.ConvertHelper;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Abstractions.Answers
{
    public abstract class AnswerBase : IAnswer
    {
        public virtual Task InitAsync() => Task.CompletedTask;
        public virtual IAnswer Deserialization(string answerData) => (IAnswer)answerData.JsonToObject(GetType());
        public abstract Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job);
    }
}
