using Microsoft.AspNetCore.SignalR;

namespace RC.Deploy.Application.Hubs
{
    /// <summary>
    /// 控制台消息Hub
    /// </summary>
    public class ConsoleMessageHub : Hub<IConsoleMessageHub>
    {
    }
}
