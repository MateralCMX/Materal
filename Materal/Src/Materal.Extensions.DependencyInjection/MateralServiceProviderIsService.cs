#if NET8_0_OR_GREATER
namespace Materal.Extensions.DependencyInjection
{
    public static partial class HostBuilderExtension
    {
        /// <summary>
        /// 服务提供者是否是服务
        /// </summary>
        public class MateralServiceProviderIsService(IServiceProvider serviceProvider) : IServiceProviderIsService
        {
            /// <summary>
            /// 是否是服务
            /// </summary>
            /// <param name="serviceType"></param>
            /// <returns></returns>
            public bool IsService(Type serviceType)
            {
                object? service = serviceProvider.GetService(serviceType);
                bool result = service is not null;
                return result;
            }
        }
    }
}
#endif