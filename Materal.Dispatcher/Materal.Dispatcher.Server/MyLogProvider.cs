using Quartz.Logging;
using System;
using System.Linq;

namespace Materal.Dispatcher.Server
{
    public class MyLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level == LogLevel.Error && exception != null)
                {
                    Console.WriteLine($"[{DateTime.Now}][{ level}]异常:{exception.Message}");
                }
                else if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{DateTime.Now}][{ level}]{ func()}{string.Join(";", parameters.Select(p => p == null ? " " : p.ToString()))}  自定义日志{name}");
                }
                return true;
            };
        }
        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
