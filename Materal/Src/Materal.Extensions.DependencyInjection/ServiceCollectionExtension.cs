using AspectCore.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 服务收集器扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        private static bool _isAddAllAssembly = true;
        /// <summary>
        /// 添加自动服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoService(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                services.AddAutoService(assembly);
            }
            return services;
        }
        /// <summary>
        /// 添加自动服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoService(this IServiceCollection services, Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                services.AddAutoService(type);
            }
            return services;
        }
        /// <summary>
        /// 添加自动服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoService(this IServiceCollection services, Type type)
        {
            if (!type.IsClass || type.IsAbstract || type.IsGenericType) return services;
            DependencyAttribute? dependencyAttribute = type.GetCustomAttribute<DependencyAttribute>();
            ServiceLifetime? lifetime = dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarchy(type);
            if (lifetime is null) return services;
            ServiceRegisterMode serviceRegisterMode = dependencyAttribute?.RegisterMode ?? ServiceRegisterMode.TryAdd;
            IEnumerable<Type> registerTypes = GetRegisterTypes(type);
            foreach (Type registerType in registerTypes)
            {
                ServiceDescriptor descriptor = dependencyAttribute?.Key is null ?
                    ServiceDescriptor.Describe(registerType, type, lifetime.Value)
                    : ServiceDescriptor.DescribeKeyed(registerType, dependencyAttribute.Key, type, lifetime.Value);
                switch (serviceRegisterMode)
                {
                    case ServiceRegisterMode.TryAdd:
                        services.TryAdd(descriptor);
                        break;
                    case ServiceRegisterMode.Add:
                        services.Add(descriptor);
                        break;
                    case ServiceRegisterMode.Replace:
                        services.Replace(descriptor);
                        break;
                }
                StringBuilder logMessage = new();
                if (dependencyAttribute?.Key is not null)
                {
                    logMessage.Append($"注册Key服务[{dependencyAttribute?.Key}]");
                }
                else
                {
                    logMessage.Append("注册服务");
                }
                logMessage.Append($"[{lifetime.Value}][{registerType.FullName}]->[{type}]");
                MateralServices.Logger?.LogDebug(logMessage.ToString());
            }
            _isAddAllAssembly = false;
            return services;
        }
        /// <summary>
        /// 获得容器生命周期
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static ServiceLifetime? GetServiceLifetimeFromClassHierarchy(Type type)
        {
            if (typeof(ITransientDependency).IsAssignableFrom(type)) return ServiceLifetime.Transient;
            if (typeof(ISingletonDependency).IsAssignableFrom(type)) return ServiceLifetime.Singleton;
            if (typeof(IScopedDependency).IsAssignableFrom(type)) return ServiceLifetime.Scoped;
            return null;
        }
        /// <summary>
        /// 获得注册类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<Type> GetRegisterTypes(Type type)
        {
            List<Type> registerTypes = [];
            ExposeServicesAttribute? exposeServicesAttribute = type.GetCustomAttribute<ExposeServicesAttribute>(true);
            if (exposeServicesAttribute is not null && exposeServicesAttribute.ServiceTypes.Length != 0)
            {
                registerTypes.AddRange(exposeServicesAttribute.ServiceTypes);
            }
            Type[] interfaces = type.GetInterfaces();
            Type? registerType = interfaces.FirstOrDefault(m => m.IsGenericType && typeof(IRegisterType<,,,>) == m.GetGenericTypeDefinition());
            registerType ??= interfaces.FirstOrDefault(m => m.IsGenericType && typeof(IRegisterType<,,>) == m.GetGenericTypeDefinition());
            registerType ??= interfaces.FirstOrDefault(m => m.IsGenericType && typeof(IRegisterType<,>) == m.GetGenericTypeDefinition());
            registerType ??= interfaces.FirstOrDefault(m => m.IsGenericType && typeof(IRegisterType<>) == m.GetGenericTypeDefinition());
            if (registerType is null)
            {
                registerTypes.Add(type);
            }
            else
            {
                registerTypes.AddRange(registerType.GetGenericArguments());
            }
            return registerTypes.Distinct();
        }
        /// <summary>
        /// 构建Materal服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider BuildMateralServiceProvider(this IServiceCollection services)
        {
            if (_isAddAllAssembly)
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                services.AddAutoService(assemblies);
            }
            IServiceProvider serviceProvider = services.BuildServiceContextProvider();
            return serviceProvider;
        }
    }
}
