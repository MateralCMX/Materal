using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Services
{
    public interface ISendMessageService
    {
        public Task SendMessageAsync(string message, Schedule schedule, ScheduleWorkView scheduleWork);
    }
}
