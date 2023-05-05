using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;

namespace Materal.Oscillator.Answers
{
    /// <summary>
    /// 控制台响应
    /// </summary>
    public class ConsoleAnswer : AnswerBase, IAnswer
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsoleAnswer() : base()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public ConsoleAnswer(string message) : base()
        {
            Message = message;
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
        public override Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, IOscillatorJob job)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(Message);
            Console.WriteLine("--------------------------------------------------------");
            return Task.FromResult(true);
        }
    }
}
