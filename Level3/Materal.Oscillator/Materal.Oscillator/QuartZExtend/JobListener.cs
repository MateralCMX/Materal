using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.DR;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;
using Quartz;

namespace Materal.Oscillator.QuartZExtend
{
    public class JobListener : IJobListener
    {
        private readonly IOscillatorListener? _oscillatorListener;
        private readonly IOscillatorDR? _oscillatorDR;
        private readonly OscillatorService _oscillatorService;

        public JobListener(OscillatorService oscillatorService, IOscillatorListener? oscillatorListener = null, IOscillatorDR? oscillatorDR = null)
        {
            _oscillatorListener = oscillatorListener;
            _oscillatorService = oscillatorService;
            _oscillatorDR = oscillatorDR;
        }

        public string Name => nameof(JobListener);

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

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException, CancellationToken cancellationToken = default)
        {
            if (context.MergedJobDataMap.ContainsKey(OscillatorJob.ScheduleDataMapKey) && context.MergedJobDataMap[OscillatorJob.ScheduleDataMapKey] is ScheduleFlowModel schedule)
            {
                if (context.NextFireTimeUtc == null)
                {
                    await _oscillatorService.StopAsync(schedule);
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
