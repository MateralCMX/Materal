using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    public class ConsoleWork : WorkBase, IWork
    {
        /// <summary>
        /// 显示的消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        public ConsoleWork()
        {
        }
        public ConsoleWork(string message)
        {
            Message = message;
        }

        public override Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWorkView scheduleWork)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(Message);
            Console.WriteLine("--------------------------------------------------------");
            return Task.FromResult((string?)null);
        }
    }
}
