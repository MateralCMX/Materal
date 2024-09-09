namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务
    /// </summary>
    public class MateralServices
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static ILogger? _logger;
        /// <summary>
        /// 日志记录器
        /// </summary>
        public static ILogger? Logger { get => _logger ??= _serviceProvider?.GetService<ILogger<MateralServices>>() ?? _services?.GetSingletonInstance<ILogger<MateralServices>>(); set => _logger = value; }
        /// <summary>
        /// 服务容器
        /// </summary>
        private static IServiceCollection? _services;
        /// <summary>
        /// 服务容器
        /// </summary>
        public static IServiceCollection Services { get => _services ?? throw new MateralException("服务容器未实例化"); set => _services = value; }
        /// <summary>
        /// 服务提供者
        /// </summary>
        private static IServiceProvider? _serviceProvider;
        /// <summary>
        /// 服务提供者
        /// </summary>
        public static IServiceProvider ServiceProvider { get => _serviceProvider ?? throw new MateralException("服务容器未实例化"); set => _serviceProvider = value; }
        /// <summary>
        /// 配置
        /// </summary>
        private static IConfiguration? _configuration;
        /// <summary>
        /// 配置
        /// </summary>
        public static IConfiguration? Configuration { get => _configuration ??= _serviceProvider?.GetService<IConfiguration>() ?? _services?.GetSingletonInstance<IConfiguration>(); set => _configuration = value; }
    }
}
