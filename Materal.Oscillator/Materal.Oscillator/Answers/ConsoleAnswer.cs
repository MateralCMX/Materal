using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Answers
{
    public class ConsoleAnswer : AnswerBase, IAnswer
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        public ConsoleAnswer()
        {
        }
        public ConsoleAnswer(string message)
        {
            Message = message;
        }
        public override Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(Message);
            Console.WriteLine("--------------------------------------------------------");
            return Task.FromResult(true);
        }
    }
}
