using System.Diagnostics;
using System.Reflection;

namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// Materal服务提供者
    /// </summary>
    public class MateralServiceProvider(IServiceProvider serviceProvider) : IServiceProvider
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object? GetService(Type serviceType)
        {
            object? obj = _serviceProvider.GetService(serviceType);
            if (obj is null) return obj;
            if (obj is IServiceProvider serviceProvider && serviceType == typeof(IServiceProvider))
            {
                if (serviceProvider == _serviceProvider) return this;
                return new MateralServiceProvider(serviceProvider);
            }
            if (obj is IServiceScopeFactory serviceScopeFactory && serviceType == typeof(IServiceScopeFactory)) return new MateralServiceScopeFactory(serviceScopeFactory);
            if (obj is IServiceScope serviceScope && serviceType == typeof(IServiceScope)) return new MateralServiceScope(serviceScope);
            Type objType = obj.GetType();
            foreach (FieldInfo fieldInfo in objType.GetRuntimeFields())
            {
                if (!fieldInfo.HasCustomAttribute<PropertyInjectionAttribute>()) continue;
                object? fieldObj = _serviceProvider.GetService(fieldInfo.FieldType);
                fieldInfo.SetValue(obj, fieldObj);
            }
            foreach (PropertyInfo propertyInfo in objType.GetRuntimeProperties())
            {
                if (!propertyInfo.HasCustomAttribute<PropertyInjectionAttribute>()) continue;
                object? propertyObj = _serviceProvider.GetService(propertyInfo.PropertyType);
                propertyInfo.SetValue(obj, propertyObj);
            }
            return obj;
        }
        //private readonly IServiceProvider _serviceProvider = serviceProvider;
        //private readonly Func<Type, Type, bool> _filter = filter ?? ((serviceType, objType) => true);

        ///// <summary>
        ///// 获得服务
        ///// </summary>
        ///// <param name="serviceType"></param>
        ///// <returns></returns>
        //public object? GetService(Type serviceType)
        //{
        //    object? obj = _serviceProvider.GetService(serviceType);
        //    if (obj is null) return obj;
        //    if (obj is IServiceProvider serviceProvider && serviceType == typeof(IServiceProvider))
        //    {
        //        if (serviceProvider == _serviceProvider) return this;
        //        return new MateralServiceProvider(serviceProvider, _filter);
        //    }
        //    if (obj is IServiceScopeFactory serviceScopeFactory && serviceType == typeof(IServiceScopeFactory)) return new MateralServiceScopeFactory(serviceScopeFactory, _filter);
        //    if (obj is IServiceScope serviceScope && serviceType == typeof(IServiceScope)) return new MateralServiceScope(serviceScope, _filter);
        //    if (!serviceType.IsInterface || !_filter(serviceType, obj.GetType())) return obj;
        //    object result = DecoratorBuilder.BuildDecoratorObject(obj, _serviceProvider);
        //    return result;
        //}
    }
}
