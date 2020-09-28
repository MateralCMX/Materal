using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WebAPP.MateralUI
{
    public class NotificationService
    {
        private readonly IJSRuntime _jsRuntime;

        public NotificationService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        public async Task ShowAsync(string message, string title = "提示", int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("notificationManage.ShowAsync", title, message, timer);
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        public async Task InfoAsync(string message, string title = "消息", int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("notificationManage.Info", title, message, timer);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        public async Task SuccessAsync(string message, string title = "成功", int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("notificationManage.Success", title, message, timer);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        public async Task WarningAsync(string message, string title = "警告", int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("notificationManage.Warning", title, message, timer);
        }
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="timer"></param>
        public async Task ErrorAsync(string message, string title = "错误", int timer = 3000)
        {
            await _jsRuntime.InvokeVoidAsync("notificationManage.Error", title, message, timer);
        }
    }
}
