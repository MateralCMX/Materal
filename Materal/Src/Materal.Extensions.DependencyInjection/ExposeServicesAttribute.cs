namespace Materal.Extensions.DependencyInjection
{
    /// <summary>
    /// 将此实例暴露为指定的类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ExposeServicesAttribute(params Type[] serviceTypes) : Attribute
    {
        /// <summary>
        /// 暴露的服务类型
        /// </summary>
        public Type[] ServiceTypes { get; } = serviceTypes ?? Type.EmptyTypes;
    }
}
