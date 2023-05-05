using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器监听
    /// </summary>
    public interface IOscillatorListener
    {
        #region 调度器
        /// <summary>
        /// 调度器启动
        /// </summary>
        /// <param name="schedule"></param>
        public Task ScheduleStartAsync(Schedule schedule);
        /// <summary>
        /// 调度器准备完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="planTrigger"></param>
        public Task ScheduleReadyAsync(Schedule schedule, IPlanTrigger planTrigger);
        /// <summary>
        /// 调度器停止
        /// </summary>
        /// <param name="schedule"></param>
        public Task ScheduleStopAsync(Schedule schedule);
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="schedule"></param>
        public Task ScheduleExecuteAsync(Schedule schedule);
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="nextRuningTime"></param>
        public Task ScheduleExecutedAsync(Schedule schedule, DateTimeOffset? nextRuningTime = null);
        /// <summary>
        /// 调度器被否决
        /// </summary>
        /// <param name="schedule"></param>
        public Task ScheduleVetoedAsync(Schedule schedule);
        /// <summary>
        /// 调度器报错
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="exception"></param>
        public Task ScheduleErrorAsync(Schedule schedule, Exception exception);
        #endregion
        #region 任务
        /// <summary>
        /// 任务开始执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        public Task WorkExecuteAsync(Schedule schedule, ScheduleWork scheduleWork, Work work);
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="workEvent"></param>
        /// <param name="workResult"></param>
        public Task WorkExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult);
        /// <summary>
        /// 任务成功
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="workEvent"></param>
        /// <param name="workResult"></param>
        public Task WorkSuccessAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult);
        /// <summary>
        /// 任务失败
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="workEvent"></param>
        /// <param name="workResult"></param>
        public Task WorkFailAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string workEvent, string? workResult);
        /// <summary>
        /// 任务出错
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="exception"></param>
        public Task WorkErrorAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Exception exception);
        #endregion
        #region 事件
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="eventValue"></param>
        public Task WorkEventTriggerAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, string eventValue);
        #endregion
        #region 响应
        /// <summary>
        /// 响应执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        public Task AnswerExecuteAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer);
        /// <summary>
        /// 响应执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        public Task AnswerExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer);
        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <param name="canNext">是否执行下一个响应</param>
        public Task AnswerSuccessAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, bool canNext);
        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <param name="answer"></param>
        /// <param name="exception"></param>
        public Task AnswerFailAsync(Schedule schedule, ScheduleWork scheduleWork, Work work, Answer answer, Exception exception);
        #endregion
    }
}
