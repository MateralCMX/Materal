using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Answers;
using System.Text;

namespace ConsoleDemo
{
    /// <summary>
    /// 重试日志
    /// </summary>
    public class RetryAnswerListenerImpl : IRetryAnswerListener
    {
        public void RetryTrigger(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval)
        {
            string message = GetMessageTemplete(schedule, scheduleWork);
            message += $"[{nowIndex}/{retryCount}]将在 {retryInterval} 秒后进行重试。";
            ShowMessage(message);
        }
        public void RetryExcute(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval)
        {
            string message = GetMessageTemplete(schedule, scheduleWork);
            message += $"[{nowIndex}/{retryCount}]任务重试执行";
            ShowMessage(message);
        }
        public void RetryFail(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval)
        {
            string message = GetMessageTemplete(schedule, scheduleWork);
            message += $"[{nowIndex}/{retryCount}]任务重试失败";
            ShowMessage(message);
        }
        public void RetrySuccess(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval)
        {
            string message = GetMessageTemplete(schedule, scheduleWork);
            message += $"[{nowIndex}/{retryCount}]任务重试成功";
            ShowMessage(message);
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        private static void ShowMessage(string message) => Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message.Trim()}");
        /// <summary>
        /// 获得日志消息模版
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private static string GetMessageTemplete(Schedule? schedule, ScheduleWorkView? scheduleWork = null)
        {
            StringBuilder message = new();
            message.AppendLine();
            if (schedule != null)
            {
                message.AppendLine($"调度器[{schedule.Name}_{schedule.ID}]");
            }
            if (scheduleWork != null)
            {
                message.AppendLine($"任务[{scheduleWork.WorkName}_{scheduleWork.ID}]");
            }
            return message.ToString();
        }
    }
}
