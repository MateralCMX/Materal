using Materal.Oscillator.Abstractions.Extensions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Extensions;
using System.Collections.Specialized;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器主机
    /// </summary>
    public class OscillatorHost(IOptionsMonitor<OscillatorOptions> options, IServiceProvider serviceProvider, IEnumerable<IOscillatorListener> listeners, IEnumerable<IJobListener> jobListeners) : IOscillatorHost
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
            foreach (IJobListener jobListener in jobListeners)
            {
                _scheduler.ListenerManager.AddJobListener(jobListener);
            }
            await OnOscillatorHostStartBeforAsync(_scheduler);
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
                await OnOscillatorHostStopAfterAsync();
            }
            _scheduler = null;
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillatorDatas"></param>
        /// <returns></returns>
        public async Task StartOscillatorAsync(IEnumerable<IOscillatorData> oscillatorDatas)
        {
            foreach (IOscillatorData oscillator in oscillatorDatas)
            {
                await StartOscillatorAsync(oscillator);
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        public async Task<bool> StartOscillatorAsync(IOscillatorData oscillatorData)
        {
            if (_scheduler is null || !oscillatorData.Enable) return false;
            List<ITrigger> triggers = [];
            foreach (IPlanTriggerData planTriggerData in oscillatorData.PlanTriggers)
            {
                if (!planTriggerData.Enable) continue;
                IPlanTrigger? planTrigger = await planTriggerData.GetPlanTriggerAsync(serviceProvider);
                ITrigger? trigger = planTrigger?.CreateTrigger(oscillatorData);
                if (trigger is null) continue;
                if (planTriggerData is not NowPlanTriggerData)
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
            string jobName = oscillatorData.Work.Name;
            JobKey jobKey = oscillatorData.GetJobKey(jobName);
            IJobDetail? jobDetail = await _scheduler.GetJobDetail(jobKey);
            int index = 1;
            while (jobDetail is not null)
            {
                jobName = $"{oscillatorData.Work.Name}({index++})";
                jobKey = oscillatorData.GetJobKey(jobName);
                jobDetail = await _scheduler.GetJobDetail(jobKey);
            }
            jobDetail = oscillatorData.CreateJobDetail(jobKey);
            await _scheduler.ScheduleJob(jobDetail, triggers, false);
            await OnOscillatorRegisterAsync(oscillatorData);
            return true;
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillatorDatas"></param>
        /// <returns></returns>
        public async Task StopOscillatorAsync(IEnumerable<IOscillatorData> oscillatorDatas)
        {
            foreach (IOscillatorData oscillator in oscillatorDatas)
            {
                await StopOscillatorAsync(oscillator);
            }
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        public async Task<bool> StopOscillatorAsync(IOscillatorData oscillatorData)
        {
            if (_scheduler is null) return false;
            JobKey jobKey = oscillatorData.GetJobKey();
            await _scheduler.DeleteJob(jobKey);
            await OnOscillatorUnRegisterAsync(oscillatorData);
            return true;
        }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        public async Task<bool> CanRuningAsync(IOscillatorData oscillatorData)
        {
            JobKey jobKey = oscillatorData.GetJobKey();
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
        /// <summary>
        /// 调度器主机启动前
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        private async Task OnOscillatorHostStartBeforAsync(IScheduler scheduler)
        {
            foreach (IOscillatorListener listener in listeners)
            {
                await listener.OnOscillatorHostStartBeforAsync(scheduler);
            }
        }
        /// <summary>
        /// 调度器主机停止后
        /// </summary>
        /// <returns></returns>
        private async Task OnOscillatorHostStopAfterAsync()
        {
            foreach (IOscillatorListener listener in listeners)
            {
                await listener.OnOscillatorHostStopAfterAsync();
            }
        }
        /// <summary>
        /// 调度器注册
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        private async Task OnOscillatorRegisterAsync(IOscillatorData oscillatorData)
        {
            foreach (IOscillatorListener listener in listeners)
            {
                await listener.OnOscillatorRegisterAsync(oscillatorData);
            }
        }
        /// <summary>
        /// 调度器反注册
        /// </summary>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        private async Task OnOscillatorUnRegisterAsync(IOscillatorData oscillatorData)
        {
            foreach (IOscillatorListener listener in listeners)
            {
                await listener.OnOscillatorUnRegisterAsync(oscillatorData);
            }
        }
    }
}
