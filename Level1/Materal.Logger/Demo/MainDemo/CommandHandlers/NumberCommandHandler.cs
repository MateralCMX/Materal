using Materal.Common;
using System.Diagnostics;

namespace MainDemo.CommandHandlers
{
    public class NumberCommandHandler : BaseCommandHandler<NumberCommandHandler>
    {
        public NumberCommandHandler(IServiceProvider services) : base(services)
        {
        }
        public override bool Handler()
        {
            ConsoleQueue.WriteLine("请输入写入次数[默认100次]:");
            string? countValue = Console.ReadLine();
            int logCount = Convert.ToInt32(string.IsNullOrWhiteSpace(countValue) ? "100" : countValue);
            ConsoleQueue.WriteLine("请输入作用域[默认为公共作用域]:");
            string? scopeValue = Console.ReadLine();
            int writeCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (string.IsNullOrWhiteSpace(scopeValue))
            {
                writeCount = WriteLogs("大量日志写入", logCount);
            }
            else
            {
                using var scope = Logger.BeginScope(scopeValue);
                writeCount = WriteLogs("大量作用域日志写入", logCount);
            }
            stopwatch.Stop();
            ConsoleQueue.WriteLine($"{writeCount}条数据，耗时{stopwatch.ElapsedMilliseconds / 1000f}s");
            return true;
        }
    }
}
