using System.Threading.Tasks;

namespace Materal.WebSocket.Commands
{
    /// <summary>
    /// 命令总线
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="command">命令对象</param>
        void Send(ICommand command);
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendAsync(ICommand command);

    }
}
