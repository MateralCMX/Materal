using AntDesign;
using System;
using System.Threading.Tasks;
using WebAPP.Common;

namespace WebAPP.AntDesignHelper
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
        public void Info(string message)
        {
            _messageService.Info(message);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task InfoAsync(string message)
        {
            await _messageService.Info(message);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        public void Success(string message)
        {
            _messageService.Success(message);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SuccessAsync(string message)
        {
            await _messageService.Success(message);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            _messageService.Warning(message);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WarningAsync(string message)
        {
            await _messageService.Warning(message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            _messageService.Error(message);
            WebAPPConsoleHelper.WriteLine(message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task ErrorAsync(string message)
        {
            await _messageService.Error(message);
            WebAPPConsoleHelper.WriteLine(message);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="exception"></param>
        public void Error(Exception exception)
        {
            _messageService.Error(exception.Message);
            WebAPPConsoleHelper.WriteLine(exception);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task ErrorAsync(Exception exception)
        {
            await _messageService.Error(exception.Message);
            WebAPPConsoleHelper.WriteLine(exception);
        }
    }
}
