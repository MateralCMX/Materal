using Materal.Logger.Message;
using Materal.Logger.Models;

namespace Materal.Logger.CommandHandlers
{
    public abstract class BaseCommandHandler<T> : ICommandHander<T>
        where T : ICommand
    {
        public abstract void Handler(MateralLoggerLocalServer server, WebSocketConnectionModel websocket, T command);

        public void Handler(MateralLoggerLocalServer server, WebSocketConnectionModel websocket, ICommand command)
        {
            if (command is T tCommand)
            {
                Handler(server, websocket, tCommand);
            }
        }
    }
}
