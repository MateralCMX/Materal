
namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务作用域工厂
    /// </summary>
    public class MateralServiceScopeFactory : IServiceScope, IServiceProvider, IKeyedServiceProvider, IAsyncDisposable, IServiceScopeFactory
    {
        /// <summary>
        /// 默认服务作用域工厂
        /// </summary>
        private IServiceScopeFactory DefaultServiceScopeFactory { get; }
        /// <inheritdoc/>
        public IServiceProvider ServiceProvider { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public MateralServiceScopeFactory(IServiceScopeFactory serviceScopeFactory)
        {
            DefaultServiceScopeFactory = serviceScopeFactory;
            if (serviceScopeFactory is not IServiceProvider serviceProvider) throw new MateralException("服务作用域工厂类型错误");
            if (serviceProvider is MateralServiceProvider)
            {
                ServiceProvider = serviceProvider;
            }
            else
            {
                ServiceProvider = new MateralServiceProvider(serviceProvider);
            }
        }
        /// <summary>
        /// 创建作用域
        /// </summary>
        /// <returns></returns>
        public IServiceScope CreateScope()
        {
            IServiceScope result = DefaultServiceScopeFactory.CreateScope();
            if (result is not IServiceScopeFactory serviceScopeFactory) throw new MateralException("服务作用域类型错误");
            result = new MateralServiceScopeFactory(serviceScopeFactory);
            return result;
        }
        /// <inheritdoc/>
        public object? GetKeyedService(Type serviceType, object? serviceKey)
            => ServiceProvider.GetKeyedServices(serviceType, serviceKey);
        /// <inheritdoc/>
        public object GetRequiredKeyedService(Type serviceType, object? serviceKey)
            => ServiceProvider.GetRequiredKeyedService(serviceType, serviceKey);
        /// <inheritdoc/>
        public object? GetService(Type serviceType)
        {
            object? result = ServiceProvider.GetService(serviceType);
            return result;
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            if (DefaultServiceScopeFactory is not IDisposable disposable) return;
            disposable.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (DefaultServiceScopeFactory is not IAsyncDisposable disposable) return;
            await disposable.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}