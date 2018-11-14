using System.Threading.Tasks;
using Materal.WebSocket.Commands;

namespace Materal.WebSocket.CommandHandlers
{
    /// <summary>
    /// 命令处理器接口
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task ExcuteAsync(ICommand command);
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="command">命令对象</param>
        void Excute(ICommand command);
    }
}
