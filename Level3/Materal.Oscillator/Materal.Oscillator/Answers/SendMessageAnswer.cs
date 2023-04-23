using Materal.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Materal.Oscillator.Services;

namespace Materal.Oscillator.Answers
{
    /// <summary>
    /// 发送消息响应
    /// </summary>
    public class SendMessageAnswer : AnswerBase, IAnswer
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
        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job)
        {
            if (_sendMessageService == null) return true;
            await _sendMessageService.SendMessageAsync(Message, schedule, scheduleWork);
            return true;
        }
    }
}
