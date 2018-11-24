using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLogTest.UI
{
    public class Program
    {
        //private static readonly Logger Logger = LogManager.GetLogger("File");
        public static void Main()
        {
            //var config = new LoggingConfiguration();
            //var logfile = new FileTarget("logfile") { FileName = "file.txt" };
            //var logconsole = new ConsoleTarget("logconsole");
            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            //config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            //LogManager.Configuration = config;
            ILogger logger = LogManager.GetCurrentClassLogger();
            logger.Info("Hello World Info");
            logger.Debug("Hello World Debug");
            Console.ReadKey();
        }
    }
}
