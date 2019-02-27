using Materal.WebSocket.Commands;

namespace TestClient.Commands
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
        public string StringData { get; set; }
        public byte[] ByteArrayData { get; set; }
        public string Message { get; set; }
    }
}
