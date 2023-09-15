using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.WebAPITest
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        public static Func<Type, Type, bool> DIFilter { get; set; } = DefaultFilter;
        private readonly static string _namespaceString;
        static MateralServiceProviderFactory()
        {
            string startAssemblyName = AppDomain.CurrentDomain.FriendlyName;
            List<string> namespaces = startAssemblyName.Split('.').ToList();
            namespaces.RemoveAt(namespaces.Count - 1);
            _namespaceString = string.Join(".", namespaces);
        }
        /// <summary>
        /// 默认过滤器
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private static bool DefaultFilter(Type interfaceType, Type objectType) 
            => interfaceType.Namespace is not null && interfaceType.Namespace.StartsWith(_namespaceString) || 
            objectType.Namespace is not null && objectType.Namespace.StartsWith(_namespaceString);
        public IServiceProvider CreateBuilder(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IControllerActivatorProvider), typeof(MyControllerActivatorProvider)));
            return services.BuildMateralServiceProvider(DIFilter);
        }

        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder, DIFilter);
        }
    }
}