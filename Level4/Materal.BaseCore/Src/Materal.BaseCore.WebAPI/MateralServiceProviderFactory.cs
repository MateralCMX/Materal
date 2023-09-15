using Materal.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BaseCore.WebAPI
{
    public class MateralServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        public Func<Type, Type, bool> DIFilter { get; set; }
        private readonly static string _namespaceString;
        static MateralServiceProviderFactory()
        {
            string startAssemblyName = AppDomain.CurrentDomain.FriendlyName;
            List<string> namespaces = startAssemblyName.Split('.').ToList();
            namespaces.RemoveAt(namespaces.Count - 1);
            _namespaceString = string.Join(".", namespaces);
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dIFilter"></param>
        public MateralServiceProviderFactory(Func<Type, Type, bool>? dIFilter = null)
        {
            dIFilter ??= DefaultFilter;
            DIFilter = dIFilter;
        }
        /// <summary>
        /// 默认过滤器
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private bool DefaultFilter(Type interfaceType, Type objectType)
        {
            Console.WriteLine(objectType.Namespace);
            if (interfaceType.Namespace is not null && interfaceType.Namespace.StartsWith(_namespaceString)) return true;
            if (objectType.Namespace is not null && objectType.Namespace.StartsWith(_namespaceString)) return true;
            return false;
        }
        public IServiceProvider CreateBuilder(IServiceCollection services) => services.BuildMateralServiceProvider(DIFilter);

        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            if (containerBuilder is MateralServiceProvider) return containerBuilder;
            return new MateralServiceProvider(containerBuilder, DIFilter);
        }
    }
}