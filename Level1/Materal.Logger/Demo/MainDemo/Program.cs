using Materal.Logger;
using Microsoft.Extensions.Logging;

namespace MainDemo
{
    public partial class Program
    {
        public static void Main()
        {
            Random random = new();
            Guid userID = Guid.NewGuid();
            Console.WriteLine($"UserID={userID}");
            Console.WriteLine("按任意键开始测试");
            Console.ReadKey();
            using IDisposable scope = _logger.BeginScope(new AdvancedScope("TestScope", new Dictionary<string, string>
            {
                ["UserID"] = userID.ToString()
            }));
            while (true)
            {
                LogLevel logLevel = random.Next(0, 6) switch
                {
                    0 => LogLevel.Trace,
                    1 => LogLevel.Debug,
                    2 => LogLevel.Information,
                    3 => LogLevel.Warning,
                    4 => LogLevel.Error,
                    5 => LogLevel.Critical,
                    _ => throw new NotImplementedException()
                };
                _logger.Log(logLevel, $"Hello World!");
                Console.ReadKey();
            }
        }
    }
}