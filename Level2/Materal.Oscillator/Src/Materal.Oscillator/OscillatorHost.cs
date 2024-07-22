using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Extensions;
using Materal.Oscillator.PlanTriggers;
using System.Collections.Specialized;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器主机
    /// </summary>
    public class OscillatorHost(IOptionsMonitor<OscillatorOptions> options, IEnumerable<IJobListener>? jobListeners = null) : IOscillatorHost
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private IScheduler? _scheduler;
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async Task StartAsync()
        {
            await StopAsync();
            NameValueCollection properties = [];
            _scheduler = await SchedulerBuilder.Create(properties)
                    .UseDefaultThreadPool(x => x.MaxConcurrency = options.CurrentValue.MaxConcurrency)
                    .BuildScheduler();
            if (jobListeners is not null && jobListeners.Any())
            {
                foreach (IJobListener jobListener in jobListeners)
                {
                    _scheduler.ListenerManager.AddJobListener(jobListener, GroupMatcher<JobKey>.AnyGroup());
                }
            }
            await _scheduler.Start();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public async Task StopAsync()
        {
            if (_scheduler is null) return;
            if (_scheduler.IsStarted)
            {
                await _scheduler.Shutdown();
            }
            _scheduler = null;
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillators"></param>
        /// <returns></returns>
        public async Task StartOscillatorAsync(IEnumerable<IOscillator> oscillators)
        {
            foreach (IOscillator oscillator in oscillators)
            {
                await StartOscillatorAsync(oscillator);
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public async Task<bool> StartOscillatorAsync(IOscillator oscillator)
        {
            if (_scheduler is null || !oscillator.Enable) return false;
            List<ITrigger> triggers = [];
            foreach (IPlanTrigger planTrigger in oscillator.Triggers)
            {
                if (!planTrigger.Enable) continue;
                ITrigger? trigger = planTrigger.CreateTrigger(oscillator);
                if (trigger is null) continue;
                if (planTrigger is not NowPlanTrigger)
                {
                    if (trigger.FinalFireTimeUtc is not null && trigger.FinalFireTimeUtc <= DateTime.Now) continue;
                    triggers.Add(trigger);
                }
                else
                {
                    triggers.Add(trigger);
                }
            }
            if (triggers.Count <= 0) return false;
            string jobName = oscillator.WorkData.Name;
            JobKey jobKey = oscillator.GetJobKey(jobName);
            IJobDetail? jobDetail = await _scheduler.GetJobDetail(jobKey);
            int index = 1;
            while (jobDetail is not null)
            {
                jobName = $"{oscillator.WorkData.Name}({index++})";
                jobKey = oscillator.GetJobKey(jobName);
                jobDetail = await _scheduler.GetJobDetail(jobKey);
            }
            jobDetail = oscillator.CreateJobDetail(jobKey);
            await _scheduler.ScheduleJob(jobDetail, triggers, false);
            return true;
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillators"></param>
        /// <returns></returns>
        public async Task StopOscillatorAsync(IEnumerable<IOscillator> oscillators)
        {
            foreach (IOscillator oscillator in oscillators)
            {
                await StopOscillatorAsync(oscillator);
            }
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public async Task<bool> StopOscillatorAsync(IOscillator oscillator)
        {
            if (_scheduler is null) return false;
            JobKey jobKey = oscillator.GetJobKey();
            await _scheduler.DeleteJob(jobKey);
            return true;
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public async Task<bool> CanRuningAsync(IOscillator oscillator)
        {
            JobKey jobKey = oscillator.GetJobKey();
            return await CanRuningAsync(jobKey);
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="jobKey"></param>
        /// <returns></returns>
        public async Task<bool> CanRuningAsync(JobKey jobKey)
        {
            if (_scheduler is null) return false;
            IJobDetail? jobDetail = await _scheduler.GetJobDetail(jobKey);
            return jobDetail is not null;
        }
    }
}
