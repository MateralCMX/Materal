using Materal.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    public class MateralLoggerFactory : ILoggerFactory, IDisposable
    {
        private readonly Dictionary<string, ILogger> _loggers = new(); 
        private ILoggerProvider? _provider;
        public MateralLoggerFactory(IServiceProvider services)
        {
            ILoggerProvider? provider = services.GetService<ILoggerProvider>();
            if(provider != null)
            {
                AddProvider(provider);
            }
        }
        public void AddProvider(ILoggerProvider provider)
        {
            _provider = provider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            lock (_loggers)
            {
                if(_provider == null) return new MateralLogger(categoryName);
                if (!_loggers.TryGetValue(categoryName, out var logger))
                {
                    logger = _provider.CreateLogger(categoryName);
                    _loggers[categoryName] = logger;
                }
                return logger;
            }
        }

        public void Dispose()
        {
            _provider?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
