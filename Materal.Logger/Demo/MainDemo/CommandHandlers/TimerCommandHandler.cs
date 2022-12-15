using Materal.Common;

namespace MainDemo.CommandHandlers
{
    public class TimerCommandHandler : BaseCommandHandler<TimerCommandHandler>
    {
        public TimerCommandHandler(IServiceProvider services) : base(services)
        {
        }
        public override bool Handler()
        {
            ConsoleQueue.WriteLine("请输入次数[默认100]:");
            string? countString = Console.ReadLine();
            int count = Convert.ToInt32(string.IsNullOrWhiteSpace(countString) ? "100" : countString);
            ConsoleQueue.WriteLine("请输入间隔(毫秒)[默认1000]:");
            string? intervalString = Console.ReadLine();
            int interval = Convert.ToInt32(string.IsNullOrWhiteSpace(intervalString) ? "1000" : intervalString);
            for (int i = 1; i <= count; i++)
            {
                WriteLogs($"间隔发送的日志{i}");
                Thread.Sleep(interval);
            }
            return true;
        }
    }
}
