using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebAPP.MateralUI
{
    public class MessageService
    {
        private readonly IJSRuntime _jsRuntime;

        public MessageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer"></param>
        public async Task ShowAsync(string message, int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("messageManage.ShowAsync", message, timer);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer"></param>
        public async Task SuccessAsync(string message, int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("messageManage.Success", message, timer);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer"></param>
        public async Task WarningAsync(string message, int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("messageManage.Warning", message, timer);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timer"></param>
        public async Task ErrorAsync(string message, int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("messageManage.Error", message, timer);
        }
    }
}
