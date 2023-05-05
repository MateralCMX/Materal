using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.PlanTriggers;
using System.Text;

namespace ConsoleDemo
{
    public class OscillatorListenerImpl : IOscillatorListener
    {
        public async Task AnswerExecuteAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work, answer);
            message += $"响应开始执行。";
            ShowMessage(message);
        }
        public async Task AnswerExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work, answer);
            message += $"响应执行完毕";
            ShowMessage(message);
        }
        public async Task AnswerFailAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, Exception exception)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work, answer);
            message += $"响应执行错误：{exception.Message}";
            ShowMessage(message);
        }
        public async Task WorkEventTriggerAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string eventValue)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务事件触发[{eventValue}]";
            ShowMessage(message);
        }
        public async Task AnswerSuccessAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, bool canNext)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work, answer);
            message += $"响应执行成功,{(canNext ? "执行" : "不执行")}下一个响应。";
            ShowMessage(message);
        }
        public async Task ScheduleErrorAsync(Schedule schedule, Exception exception)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器错误：\r\n";
            message += GetExceptionMessage(exception);
            ShowMessage(message);
        }
        public async Task ScheduleExecuteAsync(Schedule schedule)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器执行。";
            ShowMessage(message);
        }
        public async Task ScheduleExecutedAsync(Schedule schedule, DateTimeOffset? nextRuningTime = null)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器执行完毕{(nextRuningTime == null ? "。" : $"下一次执行时间{nextRuningTime:yyyy-MM-dd HH:mm:ss}。")}";
            ShowMessage(message);
        }
        public async Task ScheduleStartAsync(Schedule schedule)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器启动。";
            ShowMessage(message);
        }
        public async Task ScheduleReadyAsync(Schedule schedule, IPlanTrigger planTrigger)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器准备完毕：[{planTrigger.GetDescriptionText()}]。";
            ShowMessage(message);
        }
        public async Task ScheduleStopAsync(Schedule schedule)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器停止。";
            ShowMessage(message);
        }
        public async Task ScheduleVetoedAsync(Schedule schedule)
        {
            string message = await GetMessageTempleteAsync(schedule);
            message += $"调度器被阻止执行。";
            ShowMessage(message);
        }
        public async Task WorkErrorAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Exception exception)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务错误：{exception.Message}";
            ShowMessage(message);
        }
        public async Task WorkExecuteAsync(Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务开始执行。";
            ShowMessage(message);
        }
        public async Task WorkExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务执行完毕，返回事件[{workEvent}],";
            message += $"返回结果[{workResult}],";
            ShowMessage(message);
        }
        public async Task WorkFailAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务执行失败，返回事件[{workEvent}],";
            message += $"返回结果[{workResult}],";
            ShowMessage(message);
        }
        public async Task WorkSuccessAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult)
        {
            string message = await GetMessageTempleteAsync(schedule, scheduleWork, work);
            message += $"任务执行成功，返回事件[{workEvent}],";
            message += $"返回结果[{workResult}],";
            ShowMessage(message);
        }
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        private static void ShowMessage(string message) => Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message.Trim()}");
        /// <summary>
        /// 获得消息模版
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        private static Task<string> GetMessageTempleteAsync(Schedule? schedule, ScheduleWork? scheduleWork = null, Work? work = null, Answer? answer = null)
        {
            StringBuilder message = new();
            message.AppendLine();
            if (schedule != null)
            {
                message.AppendLine($"调度器[{schedule.Name}_{schedule.ID}]");
            }
            if (scheduleWork != null && work != null)
            {
                message.AppendLine($"任务[{work.Name}_{scheduleWork.ID}]");
            }
            if (answer != null)
            {
                message.AppendLine($"响应[{answer.Name}_{answer.WorkEvent}_{answer.ID}]");
            }
            return Task.FromResult(message.ToString());
        }
        /// <summary>
        /// 获得异常消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetExceptionMessage(Exception exception)
        {
            StringBuilder errorMessage = new();
            Exception? nowException = exception;
            while (nowException != null)
            {
                errorMessage.AppendLine(nowException.Message);
                errorMessage.AppendLine(nowException.StackTrace);
                nowException = nowException.InnerException;
            }
            return errorMessage.ToString();
        }
    }
}
