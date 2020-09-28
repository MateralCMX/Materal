using System;
using System.Threading.Tasks;
using WebAPP.Common;
using WebAPP.MateralUI;

namespace WebAPP
{
    public class MessageManage
    {
        private readonly MessageService _messageService;

        public MessageManage(MessageService messageService)
        {
            _messageService = messageService;
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        public async Task InfoAsync(string message)
        {
            await _messageService.ShowAsync(message);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        public async Task SuccessAsync(string message)
        {
            await _messageService.SuccessAsync(message);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        public async Task WarningAsync(string message)
        {
            await _messageService.WarningAsync(message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        public async Task ErrorAsync(string message)
        {
            await _messageService.ErrorAsync(message);
            WebAPPConsoleHelper.WriteLine(message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="exception"></param>
        public async Task ErrorAsync(Exception exception)
        {
            await _messageService.ErrorAsync(exception.Message);
            WebAPPConsoleHelper.WriteLine(exception);
        }
    }
}
