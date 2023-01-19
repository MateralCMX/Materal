using Materal.Common;

namespace MainDemo.CommandHandlers
{
    public class TestCommandHandler : BaseCommandHandler<TestCommandHandler>
    {
        public TestCommandHandler(IServiceProvider services) : base(services)
        {
        }
        public override bool Handler()
        {
            while (true)
            {
                ConsoleQueue.WriteLine("请输入要写入的日志内容[输入Exit退出]：");
                string? inputStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputStr)) continue;
                if (inputStr == "Exit") break;
                WriteLogs(inputStr);
            }
            return true;
        }
    }
}
