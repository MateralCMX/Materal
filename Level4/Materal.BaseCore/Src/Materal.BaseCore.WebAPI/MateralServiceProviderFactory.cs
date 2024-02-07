using Materal.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Materal.BaseCore.WebAPI
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        private readonly static string _namespaceString;
        static MateralServiceProviderFactory()
        {
            string startAssemblyName = AppDomain.CurrentDomain.FriendlyName;
            List<string> namespaces = [.. startAssemblyName.Split('.')];
            namespaces.RemoveAt(namespaces.Count - 1);
            _namespaceString = string.Join(".", namespaces);
        }
        public IServiceProvider CreateBuilder(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton(typeof(IControllerActivatorProvider), typeof(MateralBaseCoreControllerActivatorProvider)));
            return services.BuildMateralServiceProvider();
        }
        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder);
        }
    }
}