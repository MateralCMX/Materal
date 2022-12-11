using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Answers
{
    public interface IRetryAnswerListener
    {
        /// <summary>
        /// 重试触发
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="nowIndex">当前次数</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        public void RetryTrigger(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval);
        /// <summary>
        /// 重试开始
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="nowIndex">当前次数</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        public void RetryExcute(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval);
        /// <summary>
        /// 重试成功
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="nowIndex">当前次数</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        public void RetrySuccess(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval);
        /// <summary>
        /// 重试失败
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="nowIndex">当前次数</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="retryInterval">重试间隔</param>
        public void RetryFail(Schedule? schedule, ScheduleWorkView scheduleWork, int nowIndex, int retryCount, int retryInterval);
    }
}
