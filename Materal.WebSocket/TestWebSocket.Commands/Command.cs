using Materal.WebSocket.Commands;

namespace TestWebSocket.Commands
{
    public class Command : ICommand
    {
        public Command()
        {
        }
        public Command(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; set; }
    }
}
