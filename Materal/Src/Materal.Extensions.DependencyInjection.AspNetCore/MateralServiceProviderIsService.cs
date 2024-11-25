using System.Collections;

namespace Materal.Extensions.DependencyInjection.AspNetCore
{
    /// <summary>
    /// Materal服务提供者工厂
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class MateralServiceProviderIsService(IServiceProvider serviceProvider) : IServiceProviderIsService
    {
        private readonly Hashtable _cache = [];
        /// <inheritdoc/>
        public bool IsService(Type serviceType)
        {
            if (_cache[serviceType] is bool boolResult) return boolResult;
            object? service = serviceProvider.GetService(serviceType);
            bool result = service is not null;
            _cache[serviceType] = result;
            return result;
        }
    }
}
