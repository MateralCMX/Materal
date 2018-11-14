using Materal.WebSocket.Commands;

namespace TestClient.Commands
{
    public class Command : ICommand
    {
        public Command(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; }
        public string StringData { get; set; }
        public byte[] ByteArrayData { get; set; }
        public string Message { get; set; }
    }
}
