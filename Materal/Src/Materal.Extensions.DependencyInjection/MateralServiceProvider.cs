using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务提供者
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class MateralServiceProvider(IServiceProvider serviceProvider) : IServiceProvider, IKeyedServiceProvider, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// 默认服务提供者
        /// </summary>
        public IServiceProvider DefaultServiceProvider { get; } = serviceProvider;
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object? GetService(Type serviceType)
        {
            object? result = DefaultServiceProvider.GetService(serviceType);
            if (serviceType == typeof(IServiceScopeFactory) && result is IServiceScopeFactory serviceScopeFactory)
            {
                result = new MateralServiceScopeFactory(serviceScopeFactory);
                return result;
            }
            if (serviceType == typeof(IServiceProvider) && result is IServiceProvider service && result is not MateralServiceProvider)
            {
                result = new MateralServiceProvider(service);
                return result;
            }
            if (result is null) return result;
            result = BindProperties(result);
            return result;
        }
        /// <inheritdoc/>
        public object? GetKeyedService(Type serviceType, object? serviceKey)
        {
            if (DefaultServiceProvider is not IKeyedServiceProvider keyedServiceProvider) return null;
            object? result = keyedServiceProvider.GetKeyedService(serviceType, serviceKey);
            if (result is null) return result;
            result = BindProperties(result);
            return result;
        }
        /// <inheritdoc/>
        public object GetRequiredKeyedService(Type serviceType, object? serviceKey)
        {
            if (DefaultServiceProvider is not IKeyedServiceProvider keyedServiceProvider) throw new MateralException("获取KeyedService失败");
            object result = keyedServiceProvider.GetRequiredKeyedService(serviceType, serviceKey);
            result = BindProperties(result);
            return result;
        }
        /// <inheritdoc/>
        public void Dispose()
        {
            if (DefaultServiceProvider is not IDisposable disposable) return;
            disposable.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <inheritdoc/>
        public async ValueTask DisposeAsync()
        {
            if (DefaultServiceProvider is not IAsyncDisposable disposable) return;
            await disposable.DisposeAsync();
            GC.SuppressFinalize(this);
        }
        private object BindProperties(object result)
        {
            Type trueType = result.GetType();
            foreach (PropertyInfo propertyInfo in trueType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!propertyInfo.CanWrite || !propertyInfo.HasCustomAttribute<FromServicesAttribute>()) continue;
                object? propertyValue = GetService(propertyInfo.PropertyType);
                propertyInfo.SetValue(result, propertyValue);
            }
            return result;
        }
    }
}