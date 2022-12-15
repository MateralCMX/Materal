using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    public interface ICommandHander
    {
        public void Handler(MateralLoggerLocalServer server, WebSocketConnectionModel websocket, ICommand command);
    }
    public interface ICommandHander<T> : ICommandHander
        where T : ICommand
    {
        public void Handler(MateralLoggerLocalServer server, WebSocketConnectionModel websocket, T command);
    }
}
