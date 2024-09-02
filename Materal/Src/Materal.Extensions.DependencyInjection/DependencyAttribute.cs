namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 声明依赖注入的行为以便自动注入到容器中，此操作会覆盖已有的依赖声明（如 <see cref="T:Dy.Core.Abstractions.Modularity.ISingletonDependency" />）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DependencyAttribute(params Type[] serviceTypes) : ExposeServicesAttribute(serviceTypes)
    {
        /// <summary>
        /// 如果指定了<see cref="P:Dy.Core.Abstractions.Modularity.DependencyAttribute.Lifetime" /> 属性而目标同时又继承了<see cref="T:Dy.Core.Abstractions.Modularity.ISingletonDependency" />等接口，则会忽略这些接口的限制。
        /// 这在需要覆盖基类的注册行为时会很有用
        /// </summary>
        public virtual ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
        /// <summary>
        /// 注册模式
        /// </summary>
        public ServiceRegisterMode RegisterMode { get; set; } = ServiceRegisterMode.TryAdd;
        /// <summary>
        /// 通过 <see cref="T:System.IServiceProvider" />.GetKeyedService等获取含有键标识的服务
        /// </summary>
        public object? Key { get; set; }
    }
}
