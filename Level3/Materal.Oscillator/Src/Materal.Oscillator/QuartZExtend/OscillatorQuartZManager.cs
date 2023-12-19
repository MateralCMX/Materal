using Materal.Abstractions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.DR;
using Quartz;
using Quartz.Impl.Matchers;
using System.Collections.Specialized;

namespace Materal.Oscillator.QuartZExtend
{
    /// <summary>
    /// QuartZ管理器
    /// </summary>
    public static class OscillatorQuartZManager
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private static IScheduler? _scheduler;
        /// <summary>
        /// 获得作业Key
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static JobKey GetJobKey(Schedule schedule) => new($"{schedule.Name}Job_{schedule.ID}", $"{schedule.Name}_{schedule.ID}");
        /// <summary>
        /// 获得TriggerKey
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static TriggerKey GetTriggerKey(Schedule schedule, Plan plan) => GetTriggerKey(plan.Name, schedule);
        /// <summary>
        /// 获得TriggerKey
        /// </summary>
        /// <param name="planName"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static TriggerKey GetTriggerKey(string planName, Schedule schedule) => new(planName, $"{schedule.Name}_{schedule.ID}");
        /// <summary>
        /// 是否有这个任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static async Task<bool> IsRuningAsync(JobKey jobKey)
        {
            if (_scheduler == null) return false;
            IJobDetail? jobDetail = await _scheduler.GetJobDetail(jobKey);
            return jobDetail != null;
        }
        /// <summary>
        /// 是否有这个任务
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static async Task<bool> IsRuningAsync(Schedule schedule)
        {
            JobKey jobKey = GetJobKey(schedule);
            return await IsRuningAsync(jobKey);
        }
        /// <summary>
        /// 添加新任务
        /// </summary>
        /// <param name="job"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        public static async Task AddNewJobAsync(IJobDetail job, params ITrigger[] triggers)
        {
            await InitQuartZAsync();
            if (_scheduler == null) return;
            await _scheduler.ScheduleJob(job, triggers, false);
        }
        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public static async Task RemoveJobAsync(JobKey jobKey)
        {
            await InitQuartZAsync();
            if (_scheduler == null) return;
            if (!await IsRuningAsync(jobKey)) return;
            await _scheduler.DeleteJob(jobKey);
        }
        /// <summary>
        /// 初始化QuartZ
        /// </summary>
        /// <returns></returns>
        private static async Task InitQuartZAsync()
        {
            if (_scheduler != null) return;
            NameValueCollection properties = new();
            _scheduler = await SchedulerBuilder.Create(properties)
                    .UseDefaultThreadPool(x => x.MaxConcurrency = OscillatorConfig.MaxConcurrency)
                    .BuildScheduler();
            IJobListener? jobListener = MateralServices.GetService<IJobListener>();
            if (jobListener != null)
            {
                _scheduler.ListenerManager.AddJobListener(jobListener, GroupMatcher<JobKey>.AnyGroup());
            }
            IOscillatorDR? _oscillatorDR = MateralServices.GetService<IOscillatorDR>();
            if (_oscillatorDR != null)
            {
                await _oscillatorDR.ScheduleStartAsync();
            }
        }
        /// <summary>
        /// 启动QuartZ
        /// </summary>
        /// <returns></returns>
        public static async Task StartAsync()
        {
            if (_scheduler == null)
            {
                await InitQuartZAsync();
            }
            if (_scheduler == null || _scheduler.IsStarted) return;
            await _scheduler.Start();
        }
        /// <summary>
        /// 停止QuartZ
        /// </summary>
        /// <returns></returns>
        public static async Task StopAsync()
        {
            if (_scheduler == null || _scheduler.IsShutdown) return;
            await _scheduler.Shutdown();
            _scheduler = null;
        }
    }
}
