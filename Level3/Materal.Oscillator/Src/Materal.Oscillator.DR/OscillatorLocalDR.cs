using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;
using Quartz;
using System.Threading.Tasks.Dataflow;

namespace Materal.Oscillator.DR
{
    public class OscillatorLocalDR : IOscillatorDR
    {
        private readonly IServiceProvider _serviceProvider;
        public OscillatorLocalDR(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task ScheduleExecuteAsync(Schedule schedule)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return Task.CompletedTask;
            JobKey jobKey = OscillatorQuartZManager.GetJobKey(scheduleFlow);
            Flow flow = new()
            {
                JobKey = jobKey.ToString(),
                ScheduleID = scheduleFlow.ID,
                ScheduleData = scheduleFlow.ToJson(),
                AuthenticationCode = scheduleFlow.AuthenticationCode
            };
            _actionBlock.Post(async () =>
            {
                _unitOfWork.RegisterAdd(flow);
                await _unitOfWork.CommitAsync();
            });
            return Task.CompletedTask;
        }

        public Task ScheduleExecuteAsync(Flow flow)
        {
            throw new NotImplementedException();
        }

        public Task ScheduleExecutedAsync(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task ScheduleExecutedAsync(Flow flow)
        {
            throw new NotImplementedException();
        }

        public Task ScheduleStartAsync()
        {
            throw new NotImplementedException();
        }

        public Task WorkExecuteAsync(Schedule schedule, ScheduleWork scheduleWork)
        {
            throw new NotImplementedException();
        }

        public Task WorkExecuteAsync(Flow flow, ScheduleWork scheduleWork)
        {
            throw new NotImplementedException();
        }

        public Task WorkExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, string? workResult)
        {
            throw new NotImplementedException();
        }

        public Task WorkExecutedAsync(Flow flow, ScheduleWork scheduleWork, string? workResult)
        {
            throw new NotImplementedException();
        }
    }
}
