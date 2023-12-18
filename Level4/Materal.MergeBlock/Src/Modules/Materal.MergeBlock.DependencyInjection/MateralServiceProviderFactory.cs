using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.MergeBlock.DependencyInjection
{
    /// <summary>
    /// 依赖注入工厂
    /// </summary>
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        /// <summary>
        /// DI过滤器
        /// </summary>
        public static Func<Type, Type, bool> DIFilter { get; set; } = DefaultFilter;
        private readonly static string _namespaceString;
        static MateralServiceProviderFactory()
        {
            string startAssemblyName = AppDomain.CurrentDomain.FriendlyName;
            List<string> namespaces = [.. startAssemblyName.Split('.')];
            namespaces.RemoveAt(namespaces.Count - 1);
            _namespaceString = string.Join(".", namespaces);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dIFilter"></param>
        public MateralServiceProviderFactory(Func<Type, Type, bool>? dIFilter = null)
        {
            if (dIFilter is not null)
            {
                DIFilter = dIFilter;
            }
        }
        /// <summary>
        /// 默认过滤器
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static bool DefaultFilter(Type interfaceType, Type objectType)
            => interfaceType.Namespace is not null && interfaceType.Namespace.StartsWith(_namespaceString) ||
            objectType.Namespace is not null && objectType.Namespace.StartsWith(_namespaceString);
        /// <summary>
        /// 创建构造器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider CreateBuilder(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IControllerActivatorProvider), typeof(MergeBlockControllerActivatorProvider)));
            return services.BuildMateralServiceProvider(DIFilter);
        }
        /// <summary>
        /// 创建服务提供器
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder, DIFilter);
        }
    }
}