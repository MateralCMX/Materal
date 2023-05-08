using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 控制台任务
    /// </summary>
    public class ConsoleWork : WorkBase, IWork
    {
        /// <summary>
        /// 显示的消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConsoleWork()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="message"></param>
        public ConsoleWork(string message)
        {
            Message = message;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public override Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(Message);
            Console.WriteLine("--------------------------------------------------------");
            return Task.FromResult((string?)null);
        }
    }
}
