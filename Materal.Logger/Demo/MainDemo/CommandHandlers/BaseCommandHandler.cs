using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MainDemo.CommandHandlers
{
    public abstract class BaseCommandHandler<T> : ICommandHandler
        where T : BaseCommandHandler<T>
    {
        protected readonly ILogger<T> Logger;
        private readonly IServiceProvider _services;

        protected BaseCommandHandler(IServiceProvider services)
        {
            _services = services;
            Logger = _services.GetService<ILogger<T>>() ?? throw new ApplicationException("获取服务失败");
        }

        protected int WriteLogs(string message, int count = 1)
        {
            ApplicationException innerException = new("这是内部异常的消息");
            ApplicationException exception = new("这是异常的消息", innerException);
            for (int i = 1; i <= count; i++)
            {
                Logger.LogTrace("{message}[{i}]", message, i);
                Logger.LogDebug("{message}[{i}]", message, i);
                Logger.LogInformation("{message}[{i}]", message, i);
                Logger.LogWarning("{message}[{i}]", message, i);
                Logger.LogError(exception, "{message}[{i}]", message, i);
                Logger.LogCritical(exception, "{message}[{i}]", message, i);
            }
            return count * 6;
        }
        public abstract bool Handler();
    }
}
