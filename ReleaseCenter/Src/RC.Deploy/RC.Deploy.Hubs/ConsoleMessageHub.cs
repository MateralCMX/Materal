using Microsoft.AspNetCore.SignalR;

namespace RC.Deploy.Hubs
{
    /// <summary>
    /// 控制台消息Hub
    /// </summary>
    public class ConsoleMessageHub : Hub
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message) => await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
