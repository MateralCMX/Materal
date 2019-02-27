namespace TestWebSocket.Commands
{
    public class TestCommand : Command
    {
        public TestCommand() : base("TestCommandHandler")
        {
        }
        public string StringData { get; set; }
    }
}
