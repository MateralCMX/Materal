using Materal.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Services;

namespace Materal.Oscillator.Works
{
    public class SendMessageWork : WorkBase, IWork
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        private ISendMessageService? _sendMessageService;
        public override async Task InitAsync()
        {
            _sendMessageService = MateralServices.GetService<ISendMessageService>();
            await base.InitAsync();
        }

        public override async Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWorkView scheduleWork)
        {
            if (_sendMessageService == null) throw new OscillatorException("未找到消息服务");
            await _sendMessageService.SendMessageAsync(Message, schedule, scheduleWork);
            return null;
        }
    }
}
