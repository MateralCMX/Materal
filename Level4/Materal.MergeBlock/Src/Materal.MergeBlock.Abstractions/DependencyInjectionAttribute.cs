namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 依赖注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyInjectionAttribute : Attribute
    {
        /// <summary>
        /// 服务生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Transient;
        /// <summary>
        /// 依赖注入类型
        /// </summary>
        public DependencyInjectionTypeEnum Type { get; set; } = DependencyInjectionTypeEnum.TryAdd;
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type? ServiceType { get; set; }
    }
}
