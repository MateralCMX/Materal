using Materal.Tools.Core;
using Materal.Tools.Core.Helper;
using Materal.Tools.Core.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.Reflection;

namespace Materal.Tools.Command
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        private static readonly IServiceProvider _serviceProvider;
        private static readonly IDisposable _logEvent;
        /// <summary>
        /// 静态构造方法
        /// </summary>
        static Program()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralTools();
            _serviceProvider = services.BuildServiceProvider();
            ILoggerListener loggerListener = _serviceProvider.GetRequiredService<ILoggerListener>();
            _logEvent = loggerListener.Subscribe(OnLog);
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }
        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            _logEvent.Dispose();
            ConsoleQueue.Wait();
        }
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            RootCommand rootCommand = new("Materal工具箱");
            AddSubCommad(rootCommand);
            int result = await rootCommand.InvokeAsync(args);
            return result;
        }
        private static void OnLog(Log log)
        {
            ConsoleColor consoleColor = log.Level switch
            {
                LogLevel.Debug => ConsoleColor.DarkGreen,
                LogLevel.Information => ConsoleColor.Gray,
                LogLevel.Warning => ConsoleColor.DarkYellow,
                LogLevel.Error => ConsoleColor.DarkRed,
                LogLevel.Critical => ConsoleColor.DarkRed,
                _ => ConsoleColor.White
            };
            if (log.Exception is null)
            {
                ConsoleQueue.WriteLine($"[{log.CreateTime:yyyy-MM-dd HH:mm:ss}|{log.Level}]{log.Message}", consoleColor);
            }
            else
            {
                ConsoleQueue.WriteLine($"[{log.CreateTime:yyyy-MM-dd HH:mm:ss}|{log.Level}]{log.Message}\r\n{log.Exception.Message}\r\n{log.Exception.StackTrace}", consoleColor);
            }
        }
        private static void AddSubCommad(RootCommand rootCommand)
        {
            Program program = new();
            MethodInfo[] methodInfos = program.GetType().GetMethods();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                AddSubCommandAttribute? attribute = methodInfo.GetCustomAttribute<AddSubCommandAttribute>();
                if (attribute is null) continue;
                methodInfo.Invoke(program, [rootCommand]);
            }
        }
    }
}
