using Materal.Oscillator.Abstractions.DR;
using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Models;
using Quartz;

namespace Materal.Oscillator.QuartZExtend
{
    /// <summary>
    /// 作业监听
    /// </summary>
    public class JobListener : IJobListener
    {
        private readonly IOscillatorHost _host;
        private readonly IOscillatorListener? _oscillatorListener;
        private readonly IOscillatorDR? _oscillatorDR;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="host"></param>
        /// <param name="oscillatorListener"></param>
        /// <param name="oscillatorDR"></param>
        public JobListener(IOscillatorHost host, IOscillatorListener ? oscillatorListener = null, IOscillatorDR? oscillatorDR = null)
        {
            _host = host;
            _oscillatorListener = oscillatorListener;
            _oscillatorDR = oscillatorDR;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => nameof(JobListener);
        /// <summary>
        /// 调度器被否决
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            if (context.MergedJobDataMap.ContainsKey(OscillatorJob.ScheduleDataMapKey) && context.MergedJobDataMap[OscillatorJob.ScheduleDataMapKey] is ScheduleFlowModel schedule)
            {
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.ScheduleVetoedAsync(schedule);
                }
            }
        }
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            if (context.MergedJobDataMap.ContainsKey(OscillatorJob.ScheduleDataMapKey) && context.MergedJobDataMap[OscillatorJob.ScheduleDataMapKey] is ScheduleFlowModel schedule)
            {
                if (_oscillatorDR != null)
                {
                    try
                    {
                        if (context.MergedJobDataMap.ContainsKey(OscillatorJob.FlowMapKey) && context.MergedJobDataMap[OscillatorJob.FlowMapKey] is Flow flow)
                        {
                            await _oscillatorDR.ScheduleExecuteAsync(flow);
                        }
                        else
                        {
                            await _oscillatorDR.ScheduleExecuteAsync(schedule);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_oscillatorListener != null)
                        {
                            await _oscillatorListener.ScheduleErrorAsync(schedule, new OscillatorException("容灾组件出错", ex));
                        }
                    }
                }
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.ScheduleExecuteAsync(schedule);
                }
            }
        }
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            if (context.MergedJobDataMap.ContainsKey(OscillatorJob.ScheduleDataMapKey) && context.MergedJobDataMap[OscillatorJob.ScheduleDataMapKey] is ScheduleFlowModel schedule)
            {
                if (context.NextFireTimeUtc == null)
                {
                    await _host.StopAsync(schedule);
                }
                if (_oscillatorDR != null)
                {
                    try
                    {
                        if (context.MergedJobDataMap.ContainsKey(OscillatorJob.FlowMapKey) && context.MergedJobDataMap[OscillatorJob.FlowMapKey] is Flow flow)
                        {
                            await _oscillatorDR.ScheduleExecutedAsync(flow);
                        }
                        else
                        {
                            await _oscillatorDR.ScheduleExecutedAsync(schedule);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (_oscillatorListener != null)
                        {
                            await _oscillatorListener.ScheduleErrorAsync(schedule, new OscillatorException("容灾组件出错", ex));
                        }
                    }
                }
                if (_oscillatorListener != null)
                {
                    await _oscillatorListener.ScheduleExecutedAsync(schedule, context.NextFireTimeUtc);
                }
            }
        }
    }
}
